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

namespace WCMS.WebSystem.WebParts.Download.Controls
{
    public partial class GroupByYear : System.Web.UI.UserControl
    {
        private int _Columns = 1;
        private int _Rows = 5;
        private int _MaxRecords = -1;
        private bool _ForceDownload = false;
        private int? _Year = null;

        protected int sPageType;
        protected int iSitePageItemID;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            WContext ctx = new WContext(this);
            sPageType = ctx.ObjectId;
            iSitePageItemID = ctx.RecordId;

            using (SqlDataReader r = SqlHelper.ExecuteReader("Download.SELECT_DownloadLocations",
                       new SqlParameter("@SitePageItemID", iSitePageItemID),
                       new SqlParameter("@PageType", sPageType),
                       new SqlParameter("@GroupByYear", true)
                       ))
            {
                DataList1.RepeatColumns = _Columns;
                DataList1.CellPadding = _Rows;
                DataList1.DataSource = r;
                DataList1.DataBind();
            }

            //Response.Write(string.Format("<h1>{0}, {1}</h1>", iSitePageItemID, sPageType));
        }

        // Repeat Columns
        public int Columns
        {
            set
            {
                _Columns = value;
            }

            get
            {
                return _Columns;
            }
        }

        // Cell Padding
        public int Rows
        {
            set
            {
                _Rows = value;
            }

            get
            {
                return _Rows;
            }
        }

        // Max Display
        public int MaxRecords
        {
            set
            {
                _MaxRecords = value;
            }

            get
            {
                return _MaxRecords;
            }
        }

        // Force Download
        public bool ForceDownload
        {
            set
            {
                _ForceDownload = value;
            }

            get
            {
                return _ForceDownload;
            }
        }

        public int? Year
        {
            set
            {
                _Year = value;
            }

            get
            {
                return _Year;
            }
        }
    }
}