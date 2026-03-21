using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.LessonReviewer.Core;

namespace WCMS.LessonReviewer.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var session = Context.Session[MakeUpServiceSession.SessionKey] as MakeUpServiceSession;
                if (session != null)
                {
                    if (session.LoggedIn)
                    {
                        Response.Redirect("~/Admin/Manage.aspx", true);
                    }
                    else
                    {
                        cmdCancel.Text = "Log Out";
                    }
                }
            }
        }

        protected void cmdLogin_Click(object sender, EventArgs e)
        {
            var adminUserName = ConfigManager.Get("AdminUserName");
            var adminPassword = ConfigManager.Get("AdminPassword");

            var userName = txtUserName.Text.Trim();
            var password = txtPassword.Text;

            if (adminUserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase) && adminPassword == password)
            {
                MakeUpServiceSession session = new MakeUpServiceSession();

                if (chkBypassSeek.Checked)
                    session.OverrideSeek = true;

                if (chkBypass.Checked)
                {
                    session.IntranetMode = true;
                    session.LoggedIn = false;
                    Session[MakeUpServiceSession.SessionKey] = session;

                    Response.Redirect("~/Default.aspx", true);
                }
                else
                {
                    session.LoggedIn = true;
                    Session[MakeUpServiceSession.SessionKey] = session;

                    Response.Redirect("~/Admin/Manage.aspx", true);
                }
            }
            else
            {
                lblMsg.Text = "Invalid username or password.";
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            var session = Context.Session[MakeUpServiceSession.SessionKey] as MakeUpServiceSession;
            if (session != null)
            {
                Session[MakeUpServiceSession.SessionKey] = null;
                Response.Redirect("~/Admin/Login.aspx", true);
            }
            else
            {
                Response.Redirect("~/Default.aspx", true);
            }
        }
    }
}