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
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.SiteList
{
    public partial class _Sections_SITELIST_CMS_ListingPage : System.Web.UI.UserControl
    {
        private string sPageType;
        private string sSitePageItemID;

        protected void Page_Load(object sender, EventArgs e)
        {
            sPageType = Request.QueryString["PageType"];
            sSitePageItemID = Request.QueryString["SitePageItemID"];

            if (!Page.IsPostBack)
            {
                this.PopulateTreeView();

                for (int i = 1; i < 11; i++)
                {
                    cboRepeatColumns.Items.Add(new ListItem(i.ToString()));
                }

                for (int i = 0; i < 16; i++)
                {
                    cboPadding.Items.Add(new ListItem(i.ToString()));
                }

                using (SqlDataReader r = SqlHelper.ExecuteReader("SiteList.SELECT_ListingPageProperties",
                    new SqlParameter("@PageType", sPageType),
                    new SqlParameter("@SitePageItemID", int.Parse(sSitePageItemID))))
                {
                    if (r.Read())
                    {
                        txtHeaderText.Text = r["HeaderText"].ToString();
                        try
                        {
                            cboSites.SelectedValue = r["ParentID"].ToString();
                        }
                        catch { }

                        try
                        {
                            cboRepeatColumns.SelectedValue = r["RepeatColumns"].ToString();
                        }
                        catch { }

                        try
                        {
                            cboPadding.SelectedValue = r["CellPadding"].ToString();
                        }
                        catch { }
                        try
                        {
                            chkSortName.Checked = (bool)r["SortByName"];
                        }
                        catch { }
                    }
                }
            }
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            int iParentID = int.Parse(cboSites.SelectedValue);
            string sHeaderText = txtHeaderText.Text.Trim();
            bool bSortByName = chkSortName.Checked;
            int iRepeatColumns = int.Parse(cboRepeatColumns.SelectedValue);
            int iCellPadding = int.Parse(cboPadding.SelectedValue);

            // PERSIST DATA
            SqlHelper.ExecuteNonQuery("SiteList.UPDATE_ListingPageProperties",
                new SqlParameter("@PageType", sPageType),
                new SqlParameter("@SitePageItemID", int.Parse(sSitePageItemID)),
                new SqlParameter("@HeaderText", sHeaderText),
                new SqlParameter("@ParentID", iParentID),
                new SqlParameter("@RepeatColumns", iRepeatColumns),
                new SqlParameter("@CellPadding", iCellPadding),
                new SqlParameter("@SortByName", bSortByName)
            );

            lblStatus.Text = "UPDATE COMPLETE";
        }

        private void PopulateTreeView()
        {
            string sSiteName = WebRegistry.SelectNode("WebAppName").Value; //"Portal";
            cboSites.Items.Clear();

            // COMBO BOX
            ListItem itemRoot = new ListItem(sSiteName, "0");
            cboSites.Items.Add(itemRoot);
            {
                DataSet ds = SqlHelper.ExecuteDataSet("CMS.SELECT_Sites");

                // START RECURSIVE
                LoadRecursiveTree(0, ds.Tables[0], "");
            }
        }

        private void LoadRecursiveTree(int parentId, DataTable table, string sTab)
        {
            sTab += WConstants.TAB;

            DataRow[] rows = table.Select("ParentID=" + parentId.ToString());
            foreach (DataRow row in rows)
            {
                string sSiteName = row["SiteName"].ToString();
                string sSiteID = row["SiteID"].ToString();

                // COMBO BOX
                ListItem item1 = new ListItem(sTab + "\u2022\u00a0" + sSiteName, sSiteID);
                cboSites.Items.Add(item1);

                LoadRecursiveTree((int)row["SiteID"], table, sTab);
            }
        }
    }
}