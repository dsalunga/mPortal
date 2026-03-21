namespace WCMS.WebSystem.WebParts.Ads
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    using WCMS.Common.Utilities;

    /// <summary>
    ///		Summary description for CMS_Ad.
    /// </summary>
    public partial class CMS_Ad : System.Web.UI.UserControl
    {
        //private SqlData db;
        //private SqlCommand cmd;
        //private SqlDataReader r;
        private string SID, SSID, IID, s_LOCATION;
        private int i_ID;


        #region Web Form designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grdAds.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdAds_PageIndexChanged);

        }
        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            IID = Request.QueryString["_i"];
            SSID = Request.QueryString["SiteSectionID"];
            SID = Request.QueryString["SiteID"];

            //db = new SqlData();

            if (SSID == null || SSID == string.Empty)
            {
                Response.Redirect(".", false);
            }

            switch (SSID)
            {
                case "HOME":
                    i_ID = int.Parse(IID);
                    s_LOCATION = "H";
                    break;
                case "CONTENT":
                    i_ID = int.Parse(IID);
                    s_LOCATION = "L";
                    break;
                default:
                    i_ID = int.Parse(SSID);
                    s_LOCATION = "S";
                    break;
            }

            if (!Page.IsPostBack)
            {
                cmdDelete.Attributes.Add("onclick", "return confirm('Delete Selected Items?');");

                // OTHER DATA (COMBO, ETC)
                this.LoadEtcData();

                // GRID DATA
                LoadData();
            }
        }

        private void LoadEtcData()
        {
            // LOAD ARTICLE TYPES
            cboTypes.Items.Add(new ListItem("[ All Categories ]", "-1"));
            using (SqlDataReader r = SqlHelper.ExecuteReader("ADS_SELECT_AdCategories_USED"))
            {
                while (r.Read())
                {
                    string s_ACID = r["AdCategoryID"].ToString();
                    string sName = r["Name"].ToString();
                    cboTypes.Items.Add(new ListItem(sName, s_ACID));
                }
            }
            cboTypes.SelectedIndex = 0;

            // LOAD WRITERS
            cboWriters.Items.Add(new ListItem("[ All Writers ]", "-1"));
            using (SqlDataReader r = SqlHelper.ExecuteReader("ADS_SELECT_Users_AD_Writer"))
            {
                while (r.Read())
                {
                    string sUserID = r["UserID"].ToString();
                    string sName = r["FullName"].ToString();
                    cboWriters.Items.Add(new ListItem(sName, sUserID));
                }
            }
            cboWriters.SelectedIndex = 0;
        }

        private void LoadData(int iPageIndex)
        {
            using (SqlDataReader r = SqlHelper.ExecuteReader("ADS_SELECT_Ads_SITE_CMS",
                new SqlParameter("@LocationItemID", i_ID),
                new SqlParameter("@LocationType", s_LOCATION)))
            {
                grd.DataSource = r;
                grd.DataBind();
            }

            // SHOW ALL UNINSERTED
            DataSet ds = SqlHelper.ExecuteDataSet("ADS_SELECT_Ads_FILTER",
                new SqlParameter("@UserID", int.Parse(cboWriters.SelectedValue)),
                new SqlParameter("@AdCategoryID", int.Parse(cboTypes.SelectedValue)),
                new SqlParameter("@LocationItemID", i_ID),
                new SqlParameter("@LocationType", s_LOCATION));
            if (iPageIndex != -1)
            {
                grdAds.CurrentPageIndex = iPageIndex;
            }
            grdAds.DataSource = ds.Tables[0].DefaultView;
            grdAds.DataBind();
        }

        private void LoadData()
        {
            this.LoadData(-1);
        }

        private void dtgArticles_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            string strSiteArticleID = e.Item.Cells[0].Text;
        }

        private void dtgAllContents_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            string strArticleID = e.Item.Cells[0].Text;
        }

        protected void btnInsert_Click(object sender, System.EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];
            if (sChecked != null && sChecked != string.Empty)
            {
                SqlHelper.ExecuteNonQuery("ADS_INSERT_SiteAds",
                new SqlParameter("@LocationItemID", i_ID),
                new SqlParameter("@LocationType", s_LOCATION),
                new SqlParameter("@AdID", int.Parse(sChecked)),
                new SqlParameter("@SiteID", int.Parse(SID)));
                this.LoadData(0);
            }
        }

        protected void cmdView_Click(object sender, System.EventArgs e)
        {
            this.LoadData(0);
        }

        private void grdAds_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.LoadData(e.NewPageIndex);
        }
    }
}
