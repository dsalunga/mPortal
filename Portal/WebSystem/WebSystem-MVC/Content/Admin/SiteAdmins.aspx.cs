using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using des.Common.Utilities;
using des.Framework;

namespace des.Web.cmsadmin
{
    /// <summary>
    /// Summary description for Users.
    /// </summary>
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
        }

        protected void cmdAdd_Click(object sender, System.EventArgs e)
        {
            //Response.Redirect("CreateUser.aspx?RoleName=Site Owners");

            QSParser qs = new QSParser(this);
            qs["Return"] = "ManageUsers.aspx";
            qs["Page"] = "Accounts/CreateUser";
            qs["RoleName"] = "Site Owners,ROG Members";
            qs["CSCCID"] = "76";

            qs[WebColumns.ParentId] = "00000000-0000-0000-0000-000000000001";
            Response.Redirect("/Admin/?" + qs, true);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string sUID = e.CommandArgument.ToString();
            QSParser qs = new QSParser(this);
            qs["RoleName"] = "Site Owners";

            switch (e.CommandName)
            {
                case "edit_item":
                    if (Roles.IsUserInRole(sUID, "ROG Members"))
                    {
                        Response.Redirect("/Admin/?Return=ManageUsers.aspx&Page=Accounts/User&CSCCID=76&UserName=" + e.CommandArgument, true);
                    }
                    else
                    {
                        qs["UserName"] = sUID;
                        qs["Return"] = "SiteAdmins.aspx";
                        Response.Redirect("UserProfile.aspx?" + qs, true);
                    }
                    
                    break;
                case "permission":
                    qs[WebColumns.UserId] = sUID;
                    Response.Redirect("SiteAdminRights.aspx?" + qs, true);
                    break;
                case "content":
                    qs[WebColumns.UserId] = sUID;
                    Response.Redirect("ContentRights.aspx?" + qs, true);
                    break;
                case "Custom_Delete":
                    MembershipUser user = Membership.GetUser(new Guid(sUID));
                    Membership.DeleteUser(user.UserName, true);
                    GridView1.DataBind();
                    break;
            }
        }
    }
}
