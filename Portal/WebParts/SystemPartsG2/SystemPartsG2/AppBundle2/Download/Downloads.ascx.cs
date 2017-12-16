using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

using WCMS.WebSystem.WebParts.Download.Controls;

namespace WCMS.WebSystem.WebParts.Download
{
    public partial class _Sections_Download_Downloads : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WContext context = new WContext(this);
            int sSiteID = context.Page.SiteId;
            string sControl = "DetailedList.ascx";
            int iColumns = 1;
            int iRows = 5;
            bool forceDownload = true;
            int maxRecords = -1;

            // DEFAULT
            int sPageType = context.ObjectId;
            int sSitePageItemID = context.RecordId;

            using (SqlDataReader r = SqlHelper.ExecuteReader("Download.SELECT_DownloadProperties",
                    new SqlParameter("@PageType", sPageType),
                    new SqlParameter("@SitePageItemID", sSitePageItemID)
            ))
            {
                if (r.Read())
                {
                    sControl = r["InitialControl"].ToString();
                    if (string.IsNullOrEmpty(sControl)) sControl = "DetailedList.ascx";

                    if (r["Columns"] != DBNull.Value) iColumns = Convert.ToInt32(r["Columns"]);
                    if (r["Rows"] != DBNull.Value) iRows = Convert.ToInt32(r["Rows"]);
                    if (r["ForceDownload"] != DBNull.Value) forceDownload = Convert.ToBoolean(r["ForceDownload"]);
                    if (r["MaxRecords"] != DBNull.Value) maxRecords = Convert.ToInt32(r["MaxRecords"]);
                }
            }

            //Response.Write(string.Format("<h1>ThumbColumns = {0}</h1>", iThumbColumns));

            string sControlPath = "Controls/" + sControl;
            //if (File.Exists(MapPath(sControlPath)))
            //{

            Control c = LoadControl(sControlPath);
            switch (sControl)
            {
                //case "BasicList.ascx":
                //    ASP.BasicList list = (ASP.BasicList)c;
                //    list.Columns = iColumns;
                //    list.Rows = iRows;
                //    list.ForceDownload = forceDownload;
                //    list.MaxRecords = maxRecords;
                //    list.PageType = sPageType;
                //    list.SitePageItemID = Convert.ToInt32(sSitePageItemID);
                //    break;

                //case "BasicList_A3.ascx":
                //    ASP.BasicList_A3 list3 = (ASP.BasicList_A3)c;
                //    list3.Columns = iColumns;
                //    list3.Rows = iRows;
                //    list3.ForceDownload = forceDownload;
                //    list3.MaxRecords = maxRecords;
                //    list3.PageType = sPageType;
                //    list3.SitePageItemID = Convert.ToInt32(sSitePageItemID);
                //    break;

                case "GroupByYear.ascx":
                    GroupByYear list2 = (GroupByYear)c;
                    list2.Columns = iColumns;
                    list2.Rows = iRows;
                    list2.ForceDownload = forceDownload;
                    list2.MaxRecords = maxRecords;
                    break;

                case "DetailedList.ascx":
                    DetailedList dl = (DetailedList)c;
                    dl.Columns = iColumns;
                    dl.Rows = iRows;
                    dl.ForceDownload = forceDownload;
                    dl.MaxRecords = maxRecords;
                    dl.PageType = sPageType;
                    dl.SitePageItemID = Convert.ToInt32(sSitePageItemID);
                    break;

                /*
            case "Thumbnails.ascx":
                ThumbnailView tv = (ThumbnailView)c;
                tv.ThumbnailColumns = iThumbColumns;
                tv.ThumbnailRows = iThumbRows;
                break;
                */
            }

            //WebHelper.SetQueryString(c, qs.ToString());
            this.Controls.Add(c);
        }
    }
}