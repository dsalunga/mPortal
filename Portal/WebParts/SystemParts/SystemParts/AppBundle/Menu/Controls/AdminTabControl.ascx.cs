using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

using WCMS.WebSystem.Controls;

using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Menu.Controls
{
    public partial class AdminTabControl : System.Web.UI.UserControl
    {
        protected TabControl TabControl1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var query = new WQuery(this);
                var siteId = query.GetId(WebColumns.SiteId);
                var pageId = query.GetId(WebColumns.PageId);
                var elementId = query.GetId(WebColumns.PageElementId);
                var loadKey = query.Get(WConstants.Load);
                var hasLoadKey = !string.IsNullOrEmpty(loadKey);

                if (siteId > 0 && (pageId > 0 || elementId > 0))
                {
                    var q = query.Clone();
                    q.Remove(WConstants.Load);
                    q.Remove("MenuId");
                    q.Remove("ParentId");
                    q.Remove("MenuItemId");

                    TabControl1.AddTab("tabGeneral", "Configuration", q.BuildQuery(CentralPages.LoaderMain));
                }
                else
                {
                    //this.Visible = false;
                }

                if (siteId > 0)
                    TabControl1.ThemeName = "green";

                if (!hasLoadKey)
                    query.Set(WConstants.Load, "AdminMenu.ascx");

                TabControl1.AddTab("tabMenuManager", "Menu Manager", query.BuildQuery());

                if (hasLoadKey && TabControl1.Tabs.Count == 2)
                    TabControl1.SelectedIndex = 1;
            }
        }

        protected void TabControl1_SelectedTabChanged(object oSender, TabEventArgs args)
        {
            switch (args.TabName)
            {
                case "tabGeneral":
                    TabControl1.SelectedIndex = 0;
                    break;
            }
        }
    }
}