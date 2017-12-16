using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.WebParts.Menu;

namespace WCMS.WebSystem.WebParts.Menu
{
    public partial class MenuItemEdit : System.Web.UI.UserControl
    {
        private int menuItemID;
        private int menuID;
        public int SiteId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            menuID = DataHelper.GetId(Request, "MenuId");
            menuItemID = DataHelper.GetId(Request, "MenuItemID");
            SiteId = DataHelper.GetId(Request, WebColumns.SiteId);

            if (!Page.IsPostBack)
            {
                if (menuItemID > 0)
                {
                    MenuItem item = MenuItem.Provider.Get(menuItemID);
                    if (item != null)
                    {
                        this.cboTarget.DataBind();

                        txtCaption.Text = item.Text;
                        txtRank.Text = item.Rank.ToString();
                        chkIsActive.Checked = item.Active == 1;
                        chkCheckPermission.Checked = item.CheckPermission == 1;

                        if (cboTarget.Items.FindByValue(item.Target) != null)
                            cboTarget.SelectedValue = item.Target;

                        if (!string.IsNullOrEmpty(item.NavigateUrl))
                        {
                            txtNavigateURL.Text = item.NavigateUrl;
                        }
                        else if (item.PageId > 0)
                        {
                            var page = item.Page;
                            if (page != null)
                                txtNavigateURL.Text = page.BuildRelativeUrl();
                        }
                    }
                }
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            this.ReturnPage();
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            MenuItem item = null;

            if (menuItemID > 0 && (item = MenuItem.Provider.Get(menuItemID)) != null) { }
            else
            {
                /* INSERT */
                item = new MenuItem();
                item.ParentId = DataHelper.GetId(Request, "ParentID");
                item.MenuId = DataHelper.GetId(Request, "MenuId");
            }

            item.Text = txtCaption.Text.Trim();
            item.Active = chkIsActive.Checked ? 1 : 0;
            item.CheckPermission = chkCheckPermission.Checked ? 1 : 0;

            var pageUrl = txtNavigateURL.Text.Trim();
            var page = WPage.Resolve(pageUrl);
            if (page != null)
            {
                item.NavigateUrl = string.Empty;
                item.PageId = page.Id;
            }
            else
            {
                item.NavigateUrl = pageUrl;
                item.PageId = -1;
            }

            item.Target = cboTarget.SelectedValue;
            item.Rank = DataHelper.GetInt32(txtRank.Text.Trim());

            item.Update();

            this.ReturnPage();
        }

        private void ReturnPage()
        {
            var query = new WQuery(this);
            query.Remove("MenuItemID");
            query.LoadAndRedirect("ConfigMenuItems.ascx");
        }

        public DataSet GetLinkTargets()
        {
            return DataHelper.ToDataSet(WebConstant.Provider.GetList("LinkTargets"));
        }
    }
}