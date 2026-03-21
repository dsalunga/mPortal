using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Collections;
using System.Net.Mail;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using WCMS.Common.Utilities;
using WCMS.Web;
using WCMS.Web.Controls;

namespace WCMS.WebSystem.WebParts.Central.Security
{
    public partial class CreateUser : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TabControl1.AddTab("tabBasic", "Basic", true);
                TabControl1.AddTab("tabRoles", "Roles", true);
                TabControl1.AddTab("tabSecurity", "Security", true);
                TabControl1.SelectedIndex = 0;

                string roleName = Request["RoleName"];
                if (!string.IsNullOrEmpty(roleName))
                {
                    FormRoles.RoleNames = roleName;
                }

                MultiView1.SetActiveView(viewProfile);
            }
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            switch (TabControl1.SelectedIndex)
            {
                case 0:
                    {
                        // Basic Profile
                        string userName = FormProfile.UserName;

                        MembershipUser u = Membership.GetUser(userName);
                        if (u != null)
                        {
                            lblMessage.Text = "User Name already taken.";

                            return;
                        }

                        lblMessage.Text = string.Empty;

                        MultiView1.SetActiveView(viewRoles);
                        TabControl1.SelectedIndex = 1;
                        break;
                    }

                case 1:
                    // Security
                    MultiView1.SetActiveView(viewSecurity);
                    TabControl1.SelectedIndex = 2;
                    break;

                case 2:
                    {
                        // Roles
                        //FormRoles.UpdateData();
                        //string errorMessage = FormSecurity.ValidateData();
                        //if (errorMessage != string.Empty)
                        //{
                        //    lblMessage.Text = errorMessage;
                        //}

                        lblMessage.Text = string.Empty;
                        if (UpdateData())
                        {
                            ForwardPage();
                        }
                        break;
                    }
            }
        }

        private bool UpdateData()
        {
            // Create Membership Account
            MembershipCreateStatus status;
            MembershipUser u = Membership.CreateUser(FormProfile.UserName, FormSecurity.NewPassword, FormProfile.EmailAddress,
                FormSecurity.SecurityQuestion, FormSecurity.SecurityAnswer, true, out status);

            if (status == MembershipCreateStatus.Success)
            {
                //// Assign Roles to Membership User
                //Roles.AddUserToRoles(FormProfile.UserName, FormRoles.RoleNames.Split(','));

                //// Create CMS User
                //WCMS.Web.User user = new User(new Guid(u.ProviderUserKey.ToString()), FormProfile.FirstName, FormProfile.MiddleName, FormProfile.LastName, FormProfile.TelephoneNumber,
                //    FormProfile.MobileNumber, FormProfile.FaxNumber, FormProfile.AddressLine1, FormProfile.AddressLine2, FormProfile.AddressLine3, FormProfile.BirthDate, FormProfile.PositionId);

                //// Send Email
                //string sBody = "<span style=\"font-family:Tahoma;font-size:10pt\">Username: {0}<br />Password: {1}<br /><br /><a href=\"{2}/Admin/Login.aspx?Mode=Activate&User={0}&ConfirmCode={3}\">Click here to activate your account</a></span>";
                //sBody = string.Format(sBody, FormProfile.UserName, FormSecurity.NewPassword, SystemSettings.GetSettings("System.WebPath"), u.ProviderUserKey.ToString());

                //MailMessage md = new MailMessage();
                //md.Body = sBody;
                //md.To.Add(new MailAddress(FormProfile.EmailAddress));
                //md.IsBodyHtml = true;
                //md.Subject = "Your " + SystemSettings.GetSettings("System.WebName") + " Account";

                //try
                //{
                //    SmtpClient client = new SmtpClient();
                //    client.Send(md);
                //}
                //catch
                //{
                //    //SystemEvents.Create("Error", "SMTP", ex.Message, null);
                //}
            }
            else
            {
                Response.Write(status.ToString());
                return false;
            }

            return true;
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            switch (TabControl1.SelectedIndex)
            {
                case 0:
                    // Basic Profile
                    ForwardPage();
                    break;

                case 1:
                    // Security
                    MultiView1.SetActiveView(viewProfile);
                    TabControl1.SelectedIndex = 0;
                    break;

                case 2:
                    // Roles
                    // Update
                    MultiView1.SetActiveView(viewRoles);
                    TabControl1.SelectedIndex = 1;
                    break;
            }
        }

        private void ForwardPage()
        {
            string sReturn = Request.QueryString["Return"];

            if (!string.IsNullOrEmpty(sReturn))
            {
                sReturn = HttpUtility.UrlDecode(sReturn);

                Response.Redirect(sReturn, true);
                return;
            }

            Response.Redirect("~/Central/WebSystemHome.aspx", true);
        }
    }
}