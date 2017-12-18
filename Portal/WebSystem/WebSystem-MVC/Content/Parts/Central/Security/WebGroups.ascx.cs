using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class WebGroupsController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!WebGlobalPolicy.IsUserPermitted(GlobalPolicies.Administration, Permissions.UsersManagement))
                    WQuery.StaticRedirect(WConstants.AbsoluteAccessDeniedPage);
            }
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            if (name != string.Empty)
            {
                var query = new WQuery(this);
                int parentId = query.GetId(WebColumns.ParentId);
                var item = new WebGroup();
                item.Name = name;
                item.ParentId = parentId;
                item.Update();

                GridView1.DataBind();
                txtName.Text = string.Empty;
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string name = e.Keys[0].ToString().ToLower();
            string[] builtin = AccountConstants.BuiltIn;
            foreach (string builtinGroup in builtin)
            {
                if (name == builtinGroup.ToLower())
                {
                    e.Cancel = true;
                    return;
                }
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var query = new WQuery(this);
            int id = DataHelper.GetId(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.Set(WebColumns.GroupId, id);
                    query.Set(WebColumns.ParentId, id);
                    query.Redirect(CentralPages.WebGroupHome);
                    break;

                case "Custom_Delete":
                    if (id > 0)
                    {
                        WebGroup.Delete(id);
                        GridView1.DataBind();
                    }
                    break;

                case "View_ChildNodes":
                    query.Set(WebColumns.ParentId, id);
                    query.Set(WebColumns.GroupId, id);
                    query.Redirect();
                    break;

                case "View_Users":
                    query.Set(WebColumns.GroupId, id);
                    query.Set(WebColumns.ParentId, id);
                    query.Redirect(CentralPages.WebGroupUsers);
                    break;

                case "Toggle_Approval":
                    var group = WebGroup.Get(id);
                    if (group != null)
                    {
                        group.RequireApproval = !group.RequireApproval;
                        group.Update();
                        GridView1.DataBind();
                    }
                    break;
            }
        }

        protected void cmdUp_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            int parentId = query.GetId(WebColumns.ParentId);
            if (parentId > 0)
            {
                query.Set(WebColumns.ParentId, WebGroup.Get(parentId).ParentId);
                query.Redirect();
            }
        }

        public DataSet Get(int parentId)
        {
            WebUser owner = null;
            var isAdmin = WSession.Current.IsAdministrator;

            var query = new WQuery(true);
            query.BasePath = CentralPages.WebGroupHome;

            return DataHelper.ToDataSet(
                from i in WebGroup.Provider.GetList(parentId)
                where (isAdmin || (i.Id != SystemGroups.ADMINS_GROUP_ID))
                select new
                {
                    i.Id,
                    i.Name,
                    i.OwnerId,
                    Owner = (owner = i.Owner) != null ? owner.FirstAndLastName : string.Empty,
                    i.RequireApproval,
                    NameUrl = query.Set(WebColumns.ParentId, i.Id).Set(WebColumns.GroupId, i.Id).ToString(),
                    UserCount = i.Users.Count()
                }
            );
        }

    }
}