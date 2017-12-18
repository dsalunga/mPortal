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
    public partial class WebRoleTab : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var query = new QueryParser(this);
                int id = query.GetId(WebColumns.RoleId);
                WebRole item = null;
                if (id > 0 && (item = WebRole.Get(id)) != null)
                    linkHeader.InnerHtml = item.Name;

                TabControl1.AddTab("tabGeneral", "General", query.BuildQuery(CentralPages.WebRoleHome), CentralPages.WebRoleHome);
                //TabControl1.AddTab("tabUsers", "Users", qs.BuildQuery(CentralPages.WebRoleUsers), "WebRoleUsers.aspx");

                TabControl1.SelectTab(query.BasePath);
            }
        }
    }
}