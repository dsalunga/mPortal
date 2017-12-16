using System;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.WebParts.Menu
{
    public partial class AdminMenu08 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cboSites.Items.AddRange(WebSiteViewModel.GenerateListItem(-1).ToArray());

                var siteId = DataHelper.GetId(Request, WebColumns.SiteId);
                if (siteId > 0)
                {
                    WebHelper.SetCboValue(cboSites, siteId);
                    cboSites.Visible = false;

                    ObjectDataSource1.SelectParameters["siteId"].DefaultValue = siteId.ToString();
                }

                GridView1.DataBind();
            }
        }

        protected void cmdNew_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            query.LoadAndRedirect("AdminMenuEdit.ascx");
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            string sChecked = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(sChecked))
            {
                var ids = DataHelper.ParseCommaSeparatedIdList(sChecked);
                if (ids.Count > 0)
                {
                    foreach (var id in ids)
                    {
                        var menu = MenuEntity.Provider.Get(id);
                        if (menu != null)
                            menu.Delete();
                    }

                    GridView1.DataBind();
                }
            }
        }

        public DataSet GetList(int siteId)
        {
            WSite site = null;
            var query = new WQuery(true);
            query.Set(WConstants.Load, "ConfigMenuItems.ascx");

            return DataHelper.ToDataSet(
                from item in MenuEntity.Provider.GetList()
                where siteId == -2 || item.SiteId == siteId
                select new
                {
                    item.Id,
                    item.Name,
                    item.DateCreated,
                    item.IsActive,
                    SiteName = (item.SiteId > 0 ? site = WSite.Get(item.SiteId) : null) != null ? site.Name : string.Empty,
                    NameUrl = query.Set("MenuId", item.Id).BuildQuery()
                }
            );
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();

            var query = new WQuery(this);
            query.Set("MenuId", id);

            switch (e.CommandName)
            {
                case "edit_item":
                    query.LoadAndRedirect("AdminMenuEdit.ascx");
                    break;

                case "menu_items":
                    query.LoadAndRedirect("ConfigMenuItems.ascx");
                    break;
            }
        }

        protected void cboSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }
    }
}