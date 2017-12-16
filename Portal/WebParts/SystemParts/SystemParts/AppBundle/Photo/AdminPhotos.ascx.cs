using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

using WCMS.WebSystem.ViewModel;
using WCMS.WebSystem.WebParts.Photo;
using System.Collections.Generic;

namespace WCMS.WebSystem.WebParts.Photo
{
    /// <summary>
    ///		Summary description for CONTENTCMS_Gallery.
    /// </summary>
    public partial class AdminPhotosView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlSites.Items.AddRange(WebSiteViewModel.GenerateListItem(-1).ToArray());

                MultiView1.SetActiveView(viewGrid);

                btnImageURL.Attributes.Add("onclick", "Upload('" + txtImageURL.ClientID + "','" + PhotoConstants.TempPath + "','&FileOnly=true'); return false;");

                cmdUploadCollection.Attributes.Add("onclick", "Upload('" + txtPhotoCollection.ClientID + "','" + PhotoConstants.TempPath + "','&FileOnly=true'); return false;");

                //btnDelete.Attributes.Add("onclick", "");

                cboAlbum.DataBind();
                cboAlbumEdit.DataBind();

                // SELECT CATEGORY IF THERE IS
                string categoryID = Request["CategoryId"];
                if (!string.IsNullOrEmpty(categoryID))
                {
                    cboAlbum.SelectedValue = categoryID;
                }
                else
                {
                    cmdRegenerate.Enabled = false;
                    cmdBatchUpload.Enabled = false;
                }
            }
        }

        protected void btnNew_Click(object sender, System.EventArgs e)
        {
            MultiView1.SetActiveView(viewDetails);

            litID.Text = string.Empty;
            txtCaption.Text = string.Empty;
            txtImageURL.Text = string.Empty;
            litDateCreated.Text = DateTime.Now.ToString();

            if (!string.IsNullOrEmpty(cboAlbum.SelectedValue))
                WebHelper.SetCboValue(cboAlbum, cboAlbum.SelectedValue);
        }

        protected void btnCancel_Click(object sender, System.EventArgs e)
        {
            MultiView1.SetActiveView(viewGrid);
        }

        protected void btnUpdate_Click(object sender, System.EventArgs e)
        {
            int galleryId = -1;
            string imageFile = txtImageURL.Text.Trim();
            string albumFolder = "";
            int categoryId = int.Parse(cboAlbumEdit.SelectedValue);
            int photoHeight = PhotoConstants.DefaultPhotoHeight;
            int photoWidth = PhotoConstants.DefaultPhotoWidth;

            if (categoryId > 0)
            {
                var album = Album.Provider.Get(categoryId);
                if (album != null)
                {
                    if (album.PhotoHeight > 0)
                        photoHeight = album.PhotoHeight;

                    if (album.PhotoWidth > 0)
                        photoWidth = album.PhotoWidth;

                    albumFolder = album.FolderName;
                }

                string galleryPath = PhotoConstants.GalleryPath;
                if (!string.IsNullOrEmpty(albumFolder))
                    galleryPath += albumFolder + "/";

                string absImagePath = MapPath(galleryPath + imageFile);

                // Move file
                if (!File.Exists(MapPath(PhotoConstants.TempPath + imageFile)))
                {
                    if (!File.Exists(absImagePath))
                    {
                        lblNotify.Text = "Image does not exist.";
                        return;
                    }
                    else
                    {
                        // File already in the right place, no need to do anything
                    }
                }
                else
                {
                    // Move image from temp to gallery path
                    //if (!File.Exists(absImagePath))
                    // Overwrite

                    if (!File.Exists(absImagePath))
                        File.Delete(absImagePath);

                    File.Move(MapPath(PhotoConstants.TempPath + imageFile), absImagePath);
                }

                if (!ImageUtil.GenerateThumbnail(absImagePath, MapPath(galleryPath + PhotoConstants.ResizedPrefix + imageFile), photoWidth, photoHeight))
                {
                    lblNotify.Text = "Error creating thumbnail.";
                    return;
                }

                if (!string.IsNullOrEmpty(litID.Text))
                    galleryId = int.Parse(litID.Text);

                AlbumPhoto item = galleryId > 0 ? AlbumPhoto.Provider.Get(galleryId) : new AlbumPhoto();

                item.Caption = txtCaption.Text.Trim();
                item.PhotoName = imageFile;
                item.SiteId = DataHelper.GetId(ddlSites.SelectedValue);
                item.AlbumId = categoryId;
                item.IsActive = chkIsActive.Checked;
                item.Update();

                album.DateModified = DateTime.Now;
                album.Update();

                MultiView1.SetActiveView(viewGrid);
                GridView1.DataBind();
            }
        }

        protected void btnDelete_Click(object sender, System.EventArgs e)
        {
            string checkedItems = Request.Form["chkChecked"];
            var list = DataHelper.ParseCommaSeparatedIdList(checkedItems);
            if (list.Count > 0)
            {
                foreach (var item in list)
                    AlbumPhoto.Delete(item);

                GridView1.DataBind();
            }

            //if (!string.IsNullOrEmpty(strForm))
            //{
            //    SqlHelper.ExecuteNonQuery(CommandType.Text, "DELETE Gallery WHERE GalleryID IN(" + strForm + ");");

            //}
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var id = DataHelper.GetId(e.CommandArgument);
            switch (e.CommandName)
            {
                case "edit_item":
                    var item = AlbumPhoto.Provider.Get(id);
                    if (item != null)
                    {
                        litID.Text = item.Id.ToString();
                        txtCaption.Text = item.Caption;
                        txtImageURL.Text = item.PhotoName;
                        litDateCreated.Text = item.DateCreated.ToString();

                        WebHelper.SetCboValue(ddlSites, item.SiteId);
                        WebHelper.SetCboValue(cboAlbumEdit, item.AlbumId);

                        chkIsActive.Checked = item.IsActive;

                        MultiView1.SetActiveView(viewDetails);
                        lblNotify.Text = string.Empty;
                    }
                    break;
            }
        }

        public DataSet Select(int albumId)
        {
            var album = Album.Provider.Get(albumId);
            return DataHelper.ToDataSet(
                from i in AlbumPhoto.Provider.GetList(albumId)
                select new
                {
                    i.Id,
                    i.Caption,
                    i.PhotoName,
                    i.DateCreated,
                    i.IsActive,
                    ThumbUrl = i.RelativePhotoThumbPath,
                    AlbumTitle = album.Title
                }
            );
        }

        public IEnumerable<Album> GetAlbums()
        {
            return Album.Provider.GetList();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int categoryId = DataHelper.GetId(cboAlbum.SelectedValue);
            var query = new WQuery(this);
            if (categoryId > 0)
                query.Set("CategoryId", categoryId);
            else
                query.Remove("CategoryId");
            query.Redirect();
        }

        protected void cmdRegenerate_Click(object sender, EventArgs e)
        {
            int categoryId = DataHelper.GetId(cboAlbum.SelectedValue);
            if (categoryId > 0)
            {
                string galleryPath = PhotoConstants.GalleryPath;
                int photoHeight = PhotoConstants.DefaultPhotoHeight;
                int photoWidth = PhotoConstants.DefaultPhotoWidth;

                var album = Album.Provider.Get(categoryId);
                if (album != null)
                {
                    if (album.PhotoHeight > 0)
                        photoHeight = album.PhotoHeight;

                    if (album.PhotoWidth > 0)
                        photoWidth = album.PhotoWidth;

                    if (!string.IsNullOrEmpty(album.FolderName))
                        galleryPath += album.FolderName + "/";
                }

                /*
                using (SqlDataReader r = SqlHelper.ExecuteReader("GalleryCategory_Get",
                        new SqlParameter("@CategoryID", categoryId)))
                {
                    if (r.Read())
                    {
                        var h = DataHelper.GetId(r, "PhotoHeight");
                        if (h > 0)
                            photoHeight = h;

                        var w = DataHelper.GetId(r, "PhotoWidth");
                        if (w > 0)
                            photoWidth = w;

                        string albumFolder = DataHelper.Get(r, "FolderName");
                        if (!string.IsNullOrEmpty(albumFolder))
                            galleryPath += albumFolder + "/";
                    }
                }
                */

                lblNotify.Text = "";

                var photos = AlbumPhoto.Provider.GetList(categoryId);
                if (photos.Count() > 0)
                {
                    foreach (var photo in photos)
                    {
                        string absImagePath = MapPath(galleryPath + photo.PhotoName);
                        if (File.Exists(absImagePath))
                        {
                            if (!ImageUtil.GenerateThumbnail(absImagePath, MapPath(galleryPath + PhotoConstants.ResizedPrefix + photo.PhotoName), photoWidth, photoHeight))
                                lblNotify.Text += string.Format("Error creating thumbnail: {0}<br/>", photo.PhotoName);
                            else
                                lblNotify.Text += string.Format("Thumbnail for {0} created<br/>", photo.PhotoName);
                        }
                        else
                        {
                            lblNotify.Text += string.Format("Image file doesn't exist: {0}<br/>", photo.PhotoName);
                        }
                    }
                }

                /*
                using (SqlDataReader r = SqlHelper.ExecuteReader("Gallery_Get",
                     new SqlParameter("@CategoryID", categoryId)))
                {
                    while (r.Read())
                    {
                        string imageFile = r["ImageURL"].ToString();
                        string absImagePath = MapPath(galleryPath + imageFile);

                        if (File.Exists(absImagePath))
                        {
                            if (!ImageUtil.GenerateThumbnail(absImagePath, MapPath(galleryPath + PhotoConstants.ResizedPrefix + imageFile), photoWidth, photoHeight))
                                lblNotify.Text += "Error creating thumbnail: " + imageFile + "<br/>";
                            else
                                lblNotify.Text += "Thumbnail for " + imageFile + " created<br/>";
                        }
                        else
                        {
                            lblNotify.Text += "Image file doesn't exist: " + imageFile + "<br/>";
                        }
                    }

                    lblNotify.Text += "Thumbnail recreation done!<br/>";
                }
                */
            }
        }

        protected void cmdBatchUpload_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(viewBatchUpload);

            int categoryId = DataHelper.GetId(cboAlbum.SelectedValue);
            if (categoryId > 0)
            {
                Album item = Album.Provider.Get(categoryId);
                if (item != null)
                    lblAlbumName.InnerHtml = item.Title;
            }
        }

        protected void cmdProcessCollection_Click(object sender, EventArgs e)
        {
            int categoryId = DataHelper.GetId(cboAlbum.SelectedValue);
            Album album = Album.Provider.Get(categoryId);

            string archiveName = txtPhotoCollection.Text.Trim();
            if (!string.IsNullOrEmpty(archiveName) && album != null)
            {
                string sourcePath = MapPath(PhotoConstants.TempPath + archiveName);
                string destPath = string.Empty;
                if (File.Exists(sourcePath))
                {
                    for (int i = 1; i < 100; i++)
                    {
                        string destPathTemp = MapPath(PhotoConstants.TempPath + i + "/");
                        if (!Directory.Exists(destPathTemp))
                        {
                            Directory.CreateDirectory(destPathTemp);
                            destPath = destPathTemp;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(destPath))
                    {
                        lblBatchUploadStatus.Text += "Extracting archive " + archiveName + " to TEMP folder...";
                        Compression.Extract(sourcePath, destPath, true, true);
                        lblBatchUploadStatus.Text += " done!<br/>";

                        var photos = album.Photos;

                        var absAlbumFolder = album.AbsoluteAlbumFolder;
                        if (!Directory.Exists(absAlbumFolder))
                            Directory.CreateDirectory(absAlbumFolder);

                        Action<string> CreatePhotoEntryRecursive = null;
                        CreatePhotoEntryRecursive = (currDestPath) =>
                        {
                            if (currDestPath.EndsWith("__MACOSX", StringComparison.InvariantCultureIgnoreCase))
                                return;

                            var files = Directory.GetFiles(currDestPath);
                            foreach (var file in files)
                            {
                                if (ImageUtil.IsValidImage(file))
                                {
                                    try
                                    {
                                        var fileName = Path.GetFileName(file);
                                        lblBatchUploadStatus.Text += string.Format("Creating entry for {0} done.<br/>", fileName);

                                        string destFile = absAlbumFolder + fileName;
                                        AlbumPhoto item = null;

                                        if (File.Exists(destFile))
                                        {
                                            item = photos.FirstOrDefault(i => i.PhotoName.ToLower() == fileName.ToLower());
                                            File.Delete(destFile);
                                        }

                                        if (item == null)
                                        {
                                            item = new AlbumPhoto();
                                            item.PhotoName = fileName;
                                            item.AlbumId = album.Id;
                                            item.Caption = Path.GetFileNameWithoutExtension(fileName);
                                            item.Update();
                                        }

                                        File.Move(file, destFile);
                                        item.RecreateThumbnail();
                                    }
                                    catch (Exception ex)
                                    {
                                        LogHelper.WriteLog(ex);
                                    }
                                }

                                File.Delete(file);
                            }

                            var folders = Directory.GetDirectories(currDestPath);
                            foreach (var folder in folders)
                            {
                                CreatePhotoEntryRecursive(folder);

                                Directory.Delete(folder, true);
                            }
                        };

                        CreatePhotoEntryRecursive(destPath);

                        Directory.Delete(destPath);
                        File.Delete(sourcePath);

                        album.DateModified = DateTime.Now;
                        album.Update();


                        lblBatchUploadStatus.Text += "Batch upload completed successfully!";
                    }
                }
            }

            txtPhotoCollection.Enabled = false;
            cmdProcessCollection.Enabled = false;
            cmdUploadCollection.Enabled = false;
            cmdBatchUploadDone.Visible = true;

            panelBatchActions.Visible = false;
        }

        protected void cmdBatchUploadDone_Click(object sender, EventArgs e)
        {
            txtPhotoCollection.Enabled = true;
            txtPhotoCollection.Text = string.Empty;

            cmdProcessCollection.Enabled = true;
            cmdUploadCollection.Enabled = true;
            cmdBatchUploadDone.Visible = false;
            lblBatchUploadStatus.Text = string.Empty;

            MultiView1.SetActiveView(viewGrid);
            GridView1.DataBind();
        }

        protected void cmdBatchUploadCancel_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(viewGrid);
        }
    }
}