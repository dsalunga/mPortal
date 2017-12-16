using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.WebSystem.Controls;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Article.Controls
{
    public partial class AdminTabControl : System.Web.UI.UserControl
    {
        private int _permissionId = -2;

        protected TabControl TabControl1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //var permissionId = PermissionId;

                var query = new WQuery(this);

                var siteId = query.GetId(WebColumns.SiteId);
                var pageId = query.GetId(WebColumns.PageId);
                var inEditMode = siteId > 0 && pageId > 0;
                var loadKey = query.Get(WConstants.Load);
                var hasLoadKey = !string.IsNullOrEmpty(loadKey);
                var defaultView = query.GetInt32("DefaultView", 0);

                var q = query.Clone();

                q.Remove(WConstants.Load);
                q.Remove("DefaultView");

                // Setup Tabs
                if (inEditMode)
                    TabControl1.AddTab("tabArticles", "Selected Articles", q.BuildQuery(CentralPages.LoaderMain));

                //if (permissionId == Permissions.ManageInstance)
                {
                    if (inEditMode)
                    {
                        q.Set("DefaultView", 1);

                        TabControl1.AddTab("tabConfig", "Configuration", q.BuildQuery(CentralPages.LoaderMain));
                    }

                    if (!hasLoadKey)
                        query.Set(WConstants.Load, "AdminPublication.ascx");

                    TabControl1.AddTab("tabArticleManager", "Article Manager", query.BuildQuery());
                }

                if (hasLoadKey || !inEditMode)
                    TabControl1.SelectedTab = "tabArticleManager";
                else if (defaultView == 1)
                    TabControl1.SelectedTab = "tabConfig";
                else
                    TabControl1.SelectedTab = "tabArticles";
            }
        }

        public TabControl TabControl { get { return TabControl1; } }

        public int PermissionId
        {
            get
            {
                if (_permissionId == -2)
                    _permissionId = WHelper.GetUserMgmtPermission();

                return _permissionId;
            }
        }

        //protected void tabMain_SelectedTabChanged(object oSender, TabEventArgs args)
        //{
        //    switch (args.TabName)
        //    {
        //        case "tabArticles":
        //            mtvInstance.SetActiveView(viwContent);
        //            break;

        //        case "tabConfig":
        //            mtvInstance.SetActiveView(viwSettings);
        //            LoadTemplate();
        //            break;
        //    }
        //}
    }
}