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

namespace WCMS.WebSystem.WebParts.Download.Controls
{
    public partial class BasicListBase : System.Web.UI.UserControl
    {
        private int _Columns = 1;
        private int _Rows = 5;
        private int? _MaxRecords = null;
        private bool _ForceDownload = false;
        private string _Year = null;

        private int _SitePageItemID;
        private string _PageType;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here

            int? iYear = null;

            if (!string.IsNullOrEmpty(_Year)) iYear = Convert.ToInt32(_Year);

            if (_MaxRecords < 1) _MaxRecords = null;

            using (SqlDataReader r = SqlHelper.ExecuteReader("Download.SELECT_DownloadLocations",
                       new SqlParameter("@SitePageItemID", _SitePageItemID),
                       new SqlParameter("@PageType", _PageType),
                       new SqlParameter("@Year", iYear),
                       new SqlParameter("@MaxRecords", _MaxRecords)
                       ))
            {
                DataList1.RepeatColumns = _Columns;
                DataList1.CellPadding = _Rows;
                DataList1.DataSource = r;
                DataList1.DataBind();
            }

            //Response.Write(string.Format("<h1>{0}</h1>", _Columns));
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
        public int? MaxRecords
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

        public string Year
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

        public int SitePageItemID
        {
            set
            {
                _SitePageItemID = value;
            }

            get
            {
                return _SitePageItemID;
            }
        }

        public string PageType
        {
            set
            {
                _PageType = value;
            }

            get
            {
                return _PageType;
            }
        }
    }
}