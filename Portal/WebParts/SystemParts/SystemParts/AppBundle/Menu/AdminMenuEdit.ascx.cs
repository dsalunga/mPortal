using System;
using System.Data;
using System.Data.SqlClient;
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
    public partial class _Sections_MENU_CCMS_Menu_09 : System.Web.UI.UserControl
    {
        private int menuId;

        protected void Page_Load(object sender, EventArgs e)
        {
            menuId = DataHelper.GetId(Request, "MenuId");

            if (!Page.IsPostBack)
            {
                cboSites.Items.AddRange(WebSiteViewModel.GenerateListItem(-1).ToArray());

                MenuEntity item = null;
                if (menuId > 0)
                {
                    item = MenuEntity.Provider.Get(menuId);
                    if (item != null)
                    {
                        txtCaption.Text = item.Name;
                        chkIsActive.Checked = item.IsActive == 1;

                        cboSites.DataBind();
                        WebHelper.SetCboValue(cboSites, item.SiteId);
                    }
                }

                var siteId = DataHelper.GetId(Request, WebColumns.SiteId);
                if (siteId > 0)
                {
                    if (cboSites.SelectedIndex == 0)
                        WebHelper.SetCboValue(cboSites, siteId);

                    cboSites.Enabled = false;
                }
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            this.ReturnPage();
        }
        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            string name = txtCaption.Text.Trim();
            int siteId = DataHelper.GetId(cboSites.SelectedValue);

            MenuEntity item = null;
            if (menuId > 0 && (item = MenuEntity.Provider.Get(menuId)) != null) { }
            else
            {
                item = new MenuEntity();
                item.UserId = WSession.Current.UserId;
            }

            item.Name = name;
            item.SiteId = siteId;
            item.IsActive = chkIsActive.Checked ? 1 : 0;
            item.Update();

            this.ReturnPage();
        }

        private void ReturnPage()
        {
            var query = new WQuery(this);
            query.Remove("MenuId");
            query.LoadAndRedirect("AdminMenu.ascx");
        }
    }
}