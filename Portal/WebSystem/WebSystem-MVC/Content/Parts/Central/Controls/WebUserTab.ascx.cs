using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.WebSystem.Controls;
using WCMS.Framework;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Central.Controls
{
    public partial class WebUserTab : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var query = new WQuery(this);
                int userId = query.GetId(WebColumns.UserId);
                WebUser user = null;
                if (userId > 0 && (user = WebUser.Get(userId)) != null)
                    lblHeader.InnerHtml = string.Format("{0} ({1})", user.FirstAndLastName, user.UserName);

                var open = query.GetOpen();
                query.RemoveOpen();

                TabControl1.AddTab("tabGeneral", "General", query.BuildQuery(CentralPages.WebUserHome), CentralPages.WebUserHome);
                TabControl1.AddTab("tabGroups", "Groups", query.BuildQuery(CentralPages.WebUserGroups), CentralPages.WebUserGroups);
                TabControl1.AddTab("tabAddresses", "Addresses", query.BuildQuery(CentralPages.WebAddresses), CentralPages.WebAddresses);

                query.SetOpen("User-Activities");
                TabControl1.AddTab("tabActivities", "Activities", query.BuildQuery(CentralPages.WebUserHome), "User-Activities");
                //TabControl1.AddTab("tabRoles", "Roles", CentralPages.WebUserRoles);
                //TabControl1.AddTab("tabPermissions", "Permissions", CentralPages.WebUserPermissions);

                TabControl1.SelectTab(string.IsNullOrEmpty(open) ? query.BasePath : open);
            }
        }
    }
}