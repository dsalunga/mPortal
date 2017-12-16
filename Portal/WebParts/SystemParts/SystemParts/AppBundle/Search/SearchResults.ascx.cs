using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Search
{
    /// <summary>
    ///		Summary description for GenericSearchResult.
    /// </summary>
    public partial class GenericSearchResult : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                WContext context = new WContext(this);
                string sSearch = context["Query"];

                //litSearch.Text = sSearch;

                if (!string.IsNullOrEmpty(sSearch))
                {
                    txtSearch.Text = sSearch;
                    int sSiteID = context.Page.SiteId;

                    if (!string.IsNullOrEmpty(context["Search"]))
                    {
                        Control rbl = this.FindControl("RadioButtonList1");
                        if (rbl != null)
                        {
                            ((RadioButtonList)rbl).SelectedIndex = 1;
                            sSiteID = -1;
                        }
                    }

                    this.BeginSearch(sSearch, sSiteID);
                }
            }
        }

        private void BeginSearch(string sSearch, int sSiteID)
        {
            SqlDataSource1.SelectParameters["SiteID"].DefaultValue = sSiteID.ToString();
            SqlDataSource1.SelectParameters["Search"].DefaultValue = sSearch;
            GridView1.DataBind();
        }

        protected void cmdFind_Click(object sender, EventArgs e)
        {
            WContext context = new WContext(this);
            string sSearch = txtSearch.Text.Trim();

            if (!string.IsNullOrEmpty(sSearch))
            {
                //txtSearch.Text = sSearch;
                int sSiteID = context.Page.SiteId;

                Control rbl = this.FindControl("RadioButtonList1");
                if (rbl != null)
                    if (((RadioButtonList)rbl).Items[1].Selected)
                        sSiteID = -1;

                this.BeginSearch(sSearch, sSiteID);
            }
        }
    }
}