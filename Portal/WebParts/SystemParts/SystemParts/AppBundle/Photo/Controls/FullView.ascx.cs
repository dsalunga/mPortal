using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.WebSystem.WebParts.Photo;

namespace WCMS.WebSystem.WebParts.Photo.Controls
{
    /// <summary>
    ///		Summary description for FoundationFull.
    /// </summary>
    public partial class FullView : System.Web.UI.UserControl
    {
        protected string AlbumsLink;
        protected string sThumbnailLink;
        protected string sFullImage;
        protected string sImageCaption;
        protected string AlbumFolder;
        public string MaxPhotoWidth = "700";
        public int ThumbnailCount = 5;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            WContext context = new WContext(this);
            int photoId = context.GetId("PID");
            int albumId = context.GetId("CID");

            if (photoId < 1 || albumId < 1)
            {
                Response.Redirect(context.BuildQuery(), false);
            }
            else
            {
                List<SqlParameter> parms = new List<SqlParameter>();

                Album album = Album.Provider.Get(albumId);

                lCategory.Text = album.Title;
                AlbumFolder = album.RelativeAlbumFolder;

                DataSet ds = SqlHelper.ExecuteDataSet("GalleryPicture_GetFull",
                    new SqlParameter("@CategoryID", albumId));

                int count = ds.Tables[0].Rows.Count;

                // CURRENT PAGE
                int currentPage = context.GetInt32("CP");
                if (currentPage < 0)
                    currentPage = 0;

                // ROW COUNT
                int rowCount = 1; //(int)Math.Ceiling(iCount/5.0);

                // PAGE COUNT
                int pageCount = (int)Math.Ceiling(count / (double)(rowCount * ThumbnailCount));

                // DISPLAY CURRENT IMAGE
                DataRow rowImage = photoId > 0 ? ds.Tables[0].Select("GalleryID=" + photoId)[0] : ds.Tables[0].Rows[0];
                sFullImage = album.RelativeAlbumFolder + rowImage["ImageURL"].ToString();
                sImageCaption = rowImage["Caption"].ToString();

                //GENERATE THUMBNAILS
                #region GENERATE THUMBNAILS

                // INIT QS
                string[] sTemplate = lThumbs.Text.Trim().Split('$');
                string sTr = sTemplate[0];
                string sTdImg = sTemplate[1];
                string sThumbs = string.Empty;

                for (int iRow = currentPage * rowCount; (iRow < (currentPage * rowCount + rowCount)) && (iRow < pageCount * rowCount); iRow++)
                {
                    sThumbs += sTr;
                    for (int iCol = iRow * ThumbnailCount; (iCol < (iRow * ThumbnailCount + ThumbnailCount)) && (iCol < count); iCol++)
                    {
                        // START HERE
                        DataRow row = ds.Tables[0].Rows[iCol];
                        string thumbNail = album.RelativeAlbumFolder + PhotoConstants.ResizedPrefix + row["ImageURL"];
                        context["PID"] = row["GalleryID"].ToString();

                        // GENERATE PICTURE LINK
                        string sSrc = string.Format("javascript:LoadImage(\"{0}\",\"{1}\");", row["ImageURL"], row["Caption"]);

                        //sThumbs += sTdImg.Replace("[LINK]", ".?" + qs.ToString()).Replace("[SRC]", row["Thumbnail"].ToString()).Replace("[TITLE]", row["Caption"].ToString());

                        sThumbs += sTdImg
                            .Replace("[LINK]", sSrc)
                            .Replace("[SRC]", thumbNail)
                            .Replace("[TITLE]", row["Caption"].ToString())
                            .Replace("[HEIGHT]", album.PhotoHeight.ToString());
                    }
                    sThumbs += "</tr>";
                }

                lThumbs.Visible = true;
                lThumbs.Text = sThumbs;

                #endregion

                // GENERATE PAGING
                #region GENERATE PAGING

                // INIT QS

                if (pageCount > 1)
                {
                    string[] sPagingTemplate = lNums.Text.Split('$');
                    string sCurrentPage = sPagingTemplate[0];
                    string sNormalPage = sPagingTemplate[1];
                    string sPages = string.Empty;

                    for (int iPage = 0; iPage < pageCount; iPage++)
                    {
                        if (iPage == currentPage)
                        {
                            sPages += sCurrentPage.Replace("[#]", (iPage + 1).ToString());
                        }
                        else
                        {
                            context["CP"] = iPage.ToString();
                            sPages += sNormalPage.Replace("[#]", (iPage + 1).ToString()).Replace("[URL]", context.BuildQuery());
                        }
                    }

                    lNums.Visible = true;
                    lNums.Text = sPages;

                    // PREV PAGE
                    if (currentPage > 0)
                    {
                        context["CP"] = (currentPage - 1).ToString();

                        lPrev.Visible = true;
                        lPrev.Text = lPrev.Text.Replace("[URL]", context.BuildQuery());
                    }

                    // NEXT PAGE
                    if (currentPage < (pageCount - 1))
                    {
                        context["CP"] = (currentPage + 1).ToString();

                        lNext.Visible = true;
                        lNext.Text = lNext.Text.Replace("[URL]", context.BuildQuery());
                    }
                }

                #endregion

                // MAIN GALLERY LINK
                context.Remove("CP");
                context.Remove("PID");
                sThumbnailLink = context.BuildQuery();

                context.Remove("CID");
                AlbumsLink = context.BuildQuery();
            }
        }
    }
}
