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
    public partial class ThumbnailView : System.Web.UI.UserControl
    {
        protected string PhotoHeight = "75";

        protected string mainLink;
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
                // PRODUCT LINE TITLE
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
                }

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
            int currentPage;
            if (!string.IsNullOrEmpty(context.Get("CP")))
                currentPage = int.Parse(context.Get("CP"));
            else
                currentPage = 0;

            if (currentPage < 0) currentPage = 0;

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

            string[] sTemplate = lThumbs.Text.Trim().Split('$');
            string sTr = sTemplate[0];
            string sTdImg = sTemplate[1];
            string sThumbs = string.Empty;

            for (int rowIndex = currentPage * rowCount; (rowIndex < (currentPage * rowCount + rowCount)) && (rowIndex < pageCount * rowCount); rowIndex++)
            {
                sThumbs += sTr;
                for (int col = rowIndex * colCount; (col < (rowIndex * colCount + colCount)) && (col < count); col++)
                {
                    // START HERE
                    DataRow row = ds.Tables[0].Rows[col];
                    string sGalleryID = row["GalleryID"].ToString();
                    context["PID"] = sGalleryID;

                    string _tmp = sTdImg
                        .Replace("[LINK]", context.BuildQuery())
                        .Replace("[SRC]", albumFolder + PhotoConstants.ResizedPrefix + row["ImageURL"])
                        .Replace("[TITLE]", row["Caption"].ToString());

                    sThumbs += _tmp;

                    //.Replace("[HEIGHT]", PhotoHeight);
                    //_tmp = _tmp.Replace("[CLICK]", "window.open('/Content/Parts/Photo/Preview.aspx?Album=" + folder + "&PID=" + sGalleryID + "', 'windowPreview', 'resizable=1,scrollbars=1,width=630,height=550'); return false;");
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
                context.Remove("PID");

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
                lNums.Text = "Pages:&nbsp;" + sPages;

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
            context.Remove("CID");
            mainLink = context.BuildQuery();
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
