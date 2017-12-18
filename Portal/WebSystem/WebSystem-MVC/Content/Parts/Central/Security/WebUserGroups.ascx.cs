using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Utilities;

namespace WCMS.WebSystem.WebParts.Central.Security
{
    public partial class WebUserGroups : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var manageUser = WebGlobalPolicy.IsUserPermitted(GlobalPolicies.Administration, Permissions.UsersManagement);
                if (!manageUser)
                    WQuery.StaticRedirect(WConstants.AbsoluteAccessDeniedPage);

                var isAdmin = WSession.Current.IsAdministrator;

                int id = DataHelper.GetId(Request, WebColumns.UserId);
                if (!isAdmin && (!manageUser || WSession.Current.UserId == -1 || WSession.Current.UserId == id))
                {
                    rowControls.Visible = false;
                    GridView1.Columns[0].Visible = false;
                }
                //else if (!isAdmin && (user = id > 0 ? WebUser.Get(id) : null) != null && user.IsAdministrator())
                //{
                //    QueryParser.StaticRedirect(WConstants.AbsoluteAccessDeniedPage);
                //}
            }
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            var query = new QueryParser(this);
            bool success = false;
            int userId = query.GetId(WebColumns.UserId);
            var user = WebUser.Get(userId);
            var admin = WSession.Current.IsAdministrator;

            if (user != null)
            {
                string groupNames = txtId.Text.Trim();

                var groups = AccountHelper.CollectGroups(groupNames);
                if (groups.Count > 0)
                {
                    foreach (var group in groups)
                    {
                        if (!user.IsMemberOf(group.Id) && (admin || group.Id != SystemGroups.ADMINS_GROUP_ID || (WebGlobalPolicy.IsUserPermitted(GlobalPolicies.Administration, Permissions.FullControl))))
                        {
                            user.AddToGroup(group.Id);
                            success = true;
                        }
                    }
                }

                if (success)
                {
                    txtId.Text = string.Empty;
                    GridView1.DataBind();
                }
            }
        }

        public DataSet Select(int userId)
        {
            WebGroup g = null;

            var items = from u in WebUserGroup.GetByUserId(userId, -1)
                        where (g = u.Group) != null
                        select new
                        {
                            g.Id,
                            g.Name,
                            u.DateJoined,
                            u.Active
                        };

            return DataHelper.ToDataSet(items);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var query = new QueryParser(this);
            int groupId = DataHelper.GetId(e.CommandArgument);
            int userId = query.GetId(WebColumns.UserId);

            switch (e.CommandName)
            {
                case "Custom_Delete":
                    WebUserGroup.Delete(userId, groupId);
                    GridView1.DataBind();
                    break;

                case "ToggleActive":
                    var userGroup = WebUserGroup.Get(groupId, userId);
                    if (userGroup != null)
                    {
                        userGroup.IsActive = !userGroup.IsActive;
                        userGroup.Update();
                        GridView1.DataBind();
                    }
                    break;
            }
        }
    }
}