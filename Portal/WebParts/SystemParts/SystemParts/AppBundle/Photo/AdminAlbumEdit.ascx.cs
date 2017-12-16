using System;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

using WCMS.Common.Utilities;
using WCMS.WebSystem.WebParts.Photo;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Photo
{
    /// <summary>
    ///		Summary description for CONTENTCMS_Category.
    /// </summary>
    public partial class AdminAlbumEdit : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnImageURL.Attributes.Add("onclick", string.Format("Upload('{0}','{1}','&FileOnly=true'); return false;",
                    txtImageURL.ClientID, PhotoConstants.GalleryPath));
                txtTitle.Attributes["onblur"] = "if(WCMS.Dom.Get('" + txtFolderName.ClientID + "').value==''){WCMS.Dom.Get('" + txtFolderName.ClientID + "').value=GenerateFolderName(this.value);}";

                var id = DataHelper.GetId(Request, "CategoryId");
                var item = id > 0 ? Album.Provider.Get(id) : null;
                if (item != null)
                {
                    txtTitle.Text = item.Title;
                    txtImageURL.Text = item.ImageFile;
                    txtFolderName.Text = item.FolderName;
                    txtWidth.Text = item.Width.ToString();
                    txtPhotoHeight.Text = item.PhotoHeight.ToString();
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ReturnToGrid();
        }

        private void ReturnToGrid()
        {
            var query = new WQuery(this);
            query.Remove("CategoryId");
            query.SetLoad("AdminAlbum");
            query.Redirect();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = DataHelper.GetId(Request, "CategoryId");
            string title = txtTitle.Text.Trim();
            string imageFile = txtImageURL.Text.Trim();
            int width = DataHelper.GetId(txtWidth.Text.Trim());
            int photoHeight = DataHelper.GetId(txtPhotoHeight.Text.Trim());
            string folderName = txtFolderName.Text.Trim();
            string sourceImage = MapPath(PhotoConstants.GalleryPath + imageFile);
            string destImage = MapPath(PhotoConstants.GalleryPath + "Resized." + imageFile);

            if (!File.Exists(sourceImage))
            {
                lblNotify.Text = "Album image does not exist.";
                return;
            }

            if (id <= 0)
            {
                // Check for duplicate
                var d = Album.Provider.Get(title);
                if (d != null)
                {
                    lblNotify.Text = "The specified Album Title already exists!";
                    return;
                }
            }

            if (width > 0)
            {
                if (!ImageUtil.GenerateThumbnailW(sourceImage, destImage, width))
                {
                    lblNotify.Text = "Error creating thumbnail.";
                    return;
                }
            }
            else
            {
                File.Copy(sourceImage, destImage, true);
            }
            

            var item = id > 0 ? Album.Provider.Get(id) : new Album();
            if (item == null)
            {
                lblNotify.Text = "Album being edited does not exist!";
                return;
            }

            item.Title = title;
            item.ImageFile = imageFile;
            item.Width = width;
            item.PhotoHeight = photoHeight;
            item.FolderName = folderName;
            item.DateModified = DateTime.Now;
            item.Update();

            if (!string.IsNullOrEmpty(folderName))
            {
                string absAlbumFolder = MapPath(PhotoConstants.GalleryPath + folderName + "/");
                if (!Directory.Exists(absAlbumFolder))
                    Directory.CreateDirectory(absAlbumFolder);
            }

            ReturnToGrid();
        }
    }
}