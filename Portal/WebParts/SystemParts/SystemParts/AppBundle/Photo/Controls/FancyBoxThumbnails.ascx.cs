namespace WCMS.WebSystem.WebParts.Photo.Controls
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    using WCMS.Common.Utilities;
    using WCMS.Framework;

    using WCMS.WebSystem.WebParts.Photo;

    /// <summary>
    ///		Summary description for WebUserControl1.
    /// </summary>
    public partial class FancyBoxThumbnails : System.Web.UI.UserControl
    {
        protected string PhotoHeight = "75";

        protected string sMainLink;
        private int _ThumbnailColumns = 4;
        private int _ThumbnailRows = 5;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            WContext context = new WContext(this.Parent);
            SqlParameter[] parms;

            int categoryId = context.GetId("CID");
            string albumFolder = PhotoConstants.GalleryPath;
            string folder = "";

            if (categoryId > 0)
            {
                var album = Album.Provider.Get(categoryId);
                if (album != null)
                {
                    lCategory.Text = album.Title;
                    PhotoHeight = album.PhotoHeight.ToString();

                    folder = album.FolderName;
                    if (!string.IsNullOrEmpty(folder))
                        albumFolder += folder + "/";
                }

                /*
                using (var r = SqlHelper.ExecuteReader("GalleryCategory_Get",
                        new SqlParameter("@CategoryID", categoryId)))
                {
                    if (r.Read())
                    {
                        lCategory.Text = r["Title"].ToString();
                        PhotoHeight = r["PhotoHeight"].ToString();

                        folder = DataHelper.Get(r, "FolderName");
                        if (!string.IsNullOrEmpty(folder))
                            albumFolder += folder + "/";
                    }
                }*/

                // FILTER BY PRODUCT LINE
                parms = new SqlParameter[]
                {
                    new SqlParameter("@RecordId", context.RecordId),
                    new SqlParameter("@ObjectId", context.ObjectId),
                    new SqlParameter("@CategoryID", categoryId)
                };
            }
            else
            {
                parms = new SqlParameter[]
                {
                    new SqlParameter("@RecordId", context.RecordId),
                    new SqlParameter("@ObjectId", context.ObjectId)
                };
            }

            DataSet ds = SqlHelper.ExecuteDataSet("GalleryPicture_Get", parms);
            int count = ds.Tables[0].Rows.Count;

            // CURRENT PAGE
            int iCurrentPage;
            if (!string.IsNullOrEmpty(context["CP"]))
                iCurrentPage = int.Parse(context["CP"]);
            else
                iCurrentPage = 0;
            if (iCurrentPage < 0) iCurrentPage = 0;

            // COL COUNT
            int colCount = _ThumbnailColumns;

            // ROW COUNT
            int rowCount = _ThumbnailRows; //(int)Math.Ceiling(iCount/5.0);

            // PAGE COUNT
            int pageCount = (int)Math.Ceiling(count / (double)(rowCount * colCount));

            //GENERATE THUMBNAILS
            #region GENERATE THUMBNAILS

            // INIT QS
            context.Remove("CP");

            string[] template = lThumbs.Text.Trim().Split('$');
            string sTr = template[0];
            string sTdImg = template[1];
            string thumbs = string.Empty;

            for (int iRow = iCurrentPage * rowCount; (iRow < (iCurrentPage * rowCount + rowCount)) && (iRow < pageCount * rowCount); iRow++)
            {
                thumbs += sTr;
                for (int iCol = iRow * colCount; (iCol < (iRow * colCount + colCount)) && (iCol < count); iCol++)
                {
                    // START HERE
                    DataRow row = ds.Tables[0].Rows[iCol];
                    string albumId = row["GalleryID"].ToString();
                    context["PID"] = albumId;

                    string _tmp = sTdImg
                        .Replace("[LINK]", albumFolder + row["ImageURL"].ToString()) //ctx.BuildQuery())
                        .Replace("[SRC]", albumFolder + PhotoConstants.ResizedPrefix + row["ImageURL"])
                        .Replace("[TITLE]", row["Caption"].ToString());

                    thumbs += _tmp;

                    //.Replace("[HEIGHT]", PhotoHeight);
                    //_tmp = _tmp.Replace("[CLICK]", "window.open('/Content/Parts/Photo/Preview.aspx?Album=" + folder + "&PID=" + sGalleryID + "', 'windowPreview', 'resizable=1,scrollbars=1,width=630,height=550'); return false;");
                }
                thumbs += "</tr>";
            }

            lThumbs.Visible = true;
            lThumbs.Text = thumbs;

            #endregion

            // GENERATE PAGING
            #region GENERATE PAGING

            // INIT QS

            if (pageCount > 1)
            {
                //qs["P"] = "2";
                context.Remove("PID");

                string[] pagingTemplate = lNums.Text.Split('$');
                string currentPage = pagingTemplate[0];
                string sNormalPage = pagingTemplate[1];
                string pages = string.Empty;

                for (int iPage = 0; iPage < pageCount; iPage++)
                {
                    if (iPage == iCurrentPage)
                    {
                        pages += currentPage.Replace("[#]", (iPage + 1).ToString());
                    }
                    else
                    {
                        context["CP"] = iPage.ToString();
                        pages += sNormalPage.Replace("[#]", (iPage + 1).ToString()).Replace("[URL]", context.BuildQuery());
                    }
                }

                lNums.Visible = true;
                lNums.Text = "Pages:&nbsp;" + pages;

                // PREV PAGE
                if (iCurrentPage > 0)
                {
                    context["CP"] = (iCurrentPage - 1).ToString();

                    lPrev.Visible = true;
                    lPrev.Text = lPrev.Text.Replace("[URL]", context.BuildQuery());
                }

                // NEXT PAGE
                if (iCurrentPage < (pageCount - 1))
                {
                    context["CP"] = (iCurrentPage + 1).ToString();

                    lNext.Visible = true;
                    lNext.Text = lNext.Text.Replace("[URL]", context.BuildQuery());
                }
            }

            #endregion

            // MAIN GALLERY LINK
            context.Remove("CP");
            context.Remove("PID");
            context.Remove("CID");
            //qs["P"] = "1";
            sMainLink = context.BuildQuery();
        }

        public int ThumbnailColumns
        {
            set { _ThumbnailColumns = value; }
            get { return _ThumbnailColumns; }
        }

        public int ThumbnailRows
        {
            set { _ThumbnailRows = value; }
            get { return _ThumbnailRows; }
        }
    }
}
