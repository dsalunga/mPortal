using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class WebRolesController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();

            if (name != string.Empty)
            {
                WebRole role = new WebRole();
                role.Name = name;
                role.Update();

                GridView1.DataBind();

                txtName.Text = string.Empty;
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string name = e.Keys[0].ToString().ToLower();

            string[] builtinRoles = AccountConstants.BuiltIn;
            foreach (string builtinRole in builtinRoles)
            {
                if (name == builtinRole.ToLower())
                {
                    e.Cancel = true;
                    return;
                }
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            QueryParser query = new QueryParser(this);
            int id = DataHelper.GetId(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.Set(WebColumns.RoleId, id.ToString());
                    query.Redirect(CentralPages.WebRoleHome);
                    break;

                //case "Manage_Permissions":
                //    {
                //        qs[WebColumns.RoleId] = e.CommandArgument.ToString();
                //        qs.Redirect("~/Central/WebPermissions.aspx");

                //        break;
                //    }

                case "Custom_Delete":
                    {
                        int groupId = DataHelper.GetId(e.CommandArgument);
                        if (groupId > 0)
                        {
                            WebRole.Delete(groupId);
                            GridView1.DataBind();
                        }
                        break;
                    }

                case "View_Users":
                    {
                        query[WebColumns.RoleId] = e.CommandArgument.ToString();
                        query.Redirect(CentralPages.WebUsers);
                        break;
                    }
            }
        }

        public DataSet Get()
        {
            return DataHelper.ToDataSet(WebRole.GetList());
        }
    }
}