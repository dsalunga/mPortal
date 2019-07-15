using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Net;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.Agent;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.ExternalMemberWS;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.Ext;

namespace WCMS.WebSystem.Apps.Integration.Account
{
    public partial class ForgotPasswordV2 : System.Web.UI.UserControl
    {
        private const string LoginUrl = "LoginUrl";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var context = new WContext(this);
                var element = context.Element;

                var session = WSession.Current;
                if (session.IsLoggedIn)
                {
                    var url = WHelper.GetRedirectUrl(session.User, context, element);
                    WQuery.StaticRedirect(url, false);
                    return;
                }

                var loginUrl = element.GetParameterValue(LoginUrl);
                if (!string.IsNullOrEmpty(loginUrl))
                {
                    var query = context.Query.Clone();
                    query.BasePath = loginUrl;
                    linkLogin.HRef = loginUrl;

                    hLoginUrl.Value = loginUrl;
                }

                var username = context.Get("Username");
                if (!string.IsNullOrEmpty(username))
                {
                    if (Validator.IsRegexMatch(username, RegexPresets.Email))
                    {
                        txtEmail.Text = username;
                    }
                    else
                    {
                        txtExternalId.Text = username;
                    }
                }
            }
        }

        private void DisplayMessage(string message)
        {
            lblMessage.Text = message;
            panelAlert.Visible = true;
        }

        protected void cmdRetrieve_Click(object sender, EventArgs e)
        {
            bool validated = false;
            int recoverStatus = AccountRecoverStatus.NULL;
            var externalId = txtExternalId.Text.Trim();
            var userNameOrEmail = txtEmail.Text.Trim();
            var dateOfMembershipString = txtDateOfMembership.Text.Trim();
            var hasExternalId = !string.IsNullOrEmpty(externalId) && !chkNoExternalId.Checked;
            var dateOfMembership = DataUtil.GetDateTime(dateOfMembershipString, WConstants.DateTimeMinValue);

            if (!Page.IsValid)
            {
                DisplayMessage("Incorrect Verification Code. Please enter the correct code.");
                return;
            }

            if (string.IsNullOrEmpty(userNameOrEmail))
            {
                DisplayMessage("Enter your email.");
                return;
            }

            if (string.IsNullOrEmpty(dateOfMembershipString))
            {
                DisplayMessage("Enter your Date of Membership.");
                return;
            }

            if (string.IsNullOrEmpty(externalId) && !chkNoExternalId.Checked)
            {
                DisplayMessage("Enter your Group ID. If you don't know your Group ID, tick the checkbox.");
                return;
            }

            if (!hasExternalId && !string.IsNullOrEmpty(externalId))
                externalId = string.Empty;

            MemberProfileModel profile = null;
            WebUser user = null;
            MemberLink link = null;
            if (hasExternalId && (link = MemberLink.Provider.Get(externalId)) != null)
            {
                // Has Link & ExternalID
                user = link.User;
                if (user != null)
                {
                    // Check UserName or Email
                    if ((user.UserName.Equals(userNameOrEmail, StringComparison.InvariantCultureIgnoreCase)
                        || user.Email.Equals(userNameOrEmail, StringComparison.InvariantCultureIgnoreCase)
                        || user.EmailId.Equals(userNameOrEmail, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        // Verified
                        validated = true;
                    }
                }
            }
            else
            {
                user = WebUser.GetByEmailOrUsername(userNameOrEmail);
                if (user == null && hasExternalId)
                    user = WebUser.Get(externalId);

                if (user != null)
                {
                    link = MemberLink.Provider.GetByUserId(user.Id);
                    if (link != null)
                    {
                        // Check Date of Membership
                        if (!validated && dateOfMembership != WConstants.DateTimeMinValue && link.MembershipDate.Date == dateOfMembership.Date)
                            validated = true;
                    }
                }
            }

            if (link != null)
            {
                if (!link.IsApproved || !user.IsActive)
                {
                    recoverStatus = AccountRecoverStatus.InformAdmin;
                }
                else
                {
                    // ALL ACTIVE

                    //if (user.Groups.Count() == 0)
                    //{
                    //    DisplayMessage("There is a problem in your account's permission, please contact administrator.");
                    //    // permission issue
                    //    return;
                    //}

                    // Assuming user went here only because he does't know the password.
                    if (validated)
                    {
                        if (user.ProviderId == AccountConstants.DefaultExternalProvider)
                        {
                            recoverStatus = AccountRecoverStatus.ResetExt;
                        }
                        else
                        {
                            recoverStatus = AccountRecoverStatus.ResetPortal;
                        }
                    }
                    else
                    {
                        // Existing account, invalid input
                    }
                }
            }
            else if (user != null)
            {
                //if (!user.IsActive)
                recoverStatus = AccountRecoverStatus.InformAdmin;
            }
            else
            {

                if (!string.IsNullOrEmpty(externalId))
                {
                    // Check external ONE
                    var info = ExtProvider.GetUserInfo(externalId);
                    if (info != null)
                    {
                        recoverStatus = AccountRecoverStatus.NewUserWithONE;
                    }
                    else if (dateOfMembership <= WConstants.DateTimeMinValue) // Check external External
                    {
                        var client = new MemberSoapClient(false);
                        var member = client.GetProfile(externalId, dateOfMembership);
                        if (member != null)
                        {
                            recoverStatus = AccountRecoverStatus.RegisterSG;
                        }
                    }
                }

                if (recoverStatus == AccountRecoverStatus.NULL)
                {
                    recoverStatus = AccountRecoverStatus.RegisterNew;
                }
            }

            //var context = new WContext(this);
            //var element = context.ParameterizedObject;
            //var parameterSetName = element.GetParameterValue("MessageParameterSet");
            //WebParameterSet parameterSet = !string.IsNullOrEmpty(parameterSetName) ? WebParameterSet.Get(parameterSetName) : null;

            var query = new WQuery(this);
            switch (recoverStatus)
            {
                case AccountRecoverStatus.InformAdmin:
                    hUserId.Value = user.Id.ToString();
                    hCID.Value = externalId;
                    hDOB.Value = dateOfMembershipString;
                    lblEmailInformAdmin.InnerHtml = user.Email;
                    mvForgotPassword.SetActiveView(viewInformAdmin);
                    break;

                case AccountRecoverStatus.ResetExt:
                    // ONE Account
                    // Show option to reset via ONE / reset via their email and disconnect from ONE
                    //string ONENote = ParameterizedWebObject.GetValue("CannotResetONEAccountNote", element, parameterSet);
                    //if (string.IsNullOrEmpty(ONENote))
                    //    ONENote = "Cannot perform Password Reset on your ONE account. Please perform the Password Reset via the ONE website.";
                    //mvForgotPassword.SetActiveView(viewPageNote);
                    //panelPageNote.InnerHtml = ONENote;
                    mvForgotPassword.SetActiveView(viewONEReset);
                    break;

                case AccountRecoverStatus.ResetPortal:
                    hUserId.Value = user.Id.ToString();
                    lblEmail.InnerHtml = user.Email;
                    mvForgotPassword.SetActiveView(viewPortalReset);
                    break;

                case AccountRecoverStatus.NewUserWithONE:
                    // Redirect to Login
                    // Verify via email before continuing
                    hEmail.Value = userNameOrEmail;
                    hCID.Value = externalId;
                    hDOB.Value = dateOfMembershipString;
                    mvForgotPassword.SetActiveView(viewExistingONEConfirm);
                    break;

                case AccountRecoverStatus.RegisterSG:
                    // Redirect to SG Register
                    profile = new MemberProfileModel();
                    profile.ExternalId = externalId;
                    profile.DateOfMembership = dateOfMembership;
                    Session[IntegrationAppSessions.MemberProfile] = profile;
                    query.SetOpen("SG");
                    query.Redirect();
                    break;

                case AccountRecoverStatus.RegisterNew:
                    profile = new MemberProfileModel();
                    profile.Email = userNameOrEmail;
                    profile.ExternalId = externalId;
                    profile.DateOfMembership = dateOfMembership;
                    Session[IntegrationAppSessions.MemberProfile] = profile;
                    query.SetOpen("Register");
                    query.Redirect();
                    break;

                default:
                    DisplayMessage("One of your information is not correct. Would you like to get help?&nbsp;<a href='' class='btn btn-danger btn-xs'>Get Help</a>");
                    break;
            }

            //if (!validated)
            //    DisplayMessage("An account with the credentials you provided does not exist.");
        }

        protected void cmdInformAdmin_Click(object sender, EventArgs e)
        {
            var userId = DataUtil.GetId(hUserId.Value);
            var externalId = hCID.Value;
            var context = new WContext(this);
            var element = context.Element;
            var parameterSetName = element.GetParameterValue("ForReviewParameterSet");
            WebParameterSet parameterSet = !string.IsNullOrEmpty(parameterSetName) ? WebParameterSet.Get(parameterSetName) : new WebParameterSet();
            if (parameterSet != null)
            {
                // If did not provide email, ask whether they still have access to the email
                // If no access, ask for new email, put new email in approval Q by admin / 2 member approvals, take them to contact page
                var user = WebUser.Get(userId);
                var comments = txtComments.InnerHtml;
                var successTemplate = parameterSet.GetParameterValue("ToAdminSuccessTemplate", "Your request has been sent for review. We will contact you in 3 to 5 working days via {ContactInfo}.");
                var admins = parameterSet.GetParameterValue("Admins");
                var emailTemplatePath = parameterSet.GetParameterValue("ToAdminEmailTemplatePath", "~/Content/Parts/Integration/Assets/templates/GetAccessReviewToAdmin.htm");
                string content = FileHelper.ReadFile(MapPath(emailTemplatePath));

                var provider = new NamedValueProvider();
                provider.Add("Member", string.Format("{0}, #{1}", user.Email, user.Id));
                provider.Add("ExternalID", externalId);
                provider.Add("DOB", hDOB.Value);
                provider.Add("Comments", comments ?? "");

                content = Substituter.Substitute(content, provider);

                var msg = new WebMessageQueue();
                msg.To = admins + ";" + string.Format("{0};{1}", user.Email, user.Email2);
                msg.SendVia = MessageSendVia.Email;
                msg.ToOrBcc = MessageToOrBccStatus.Bcc;
                msg.EmailSubject = "Integration Portal: Get Access - Account Review";
                msg.EmailMessage = content;
                msg.Update();

                try
                {
                    AgentHelper.ExecuteTask(MessageProcessorTask.TASK_NAME);
                }
                catch (Exception ex)
                {
                    DisplayMessage("There was an error sending the message to the reviewer. However, it was placed on queue by the messaging server and will be sent on the next schedule.");
                    LogHelper.WriteLog(ex);
                }

                provider = new NamedValueProvider();
                provider.Add("ContactInfo", !string.IsNullOrEmpty(comments) ? user.Email + " or via the contact you provided" : "your email " + user.Email);
                panelPageNote.InnerHtml = Substituter.Substitute(successTemplate, provider);
                mvForgotPassword.SetActiveView(viewPageNote);

                var q = new WQuery(linkPageNoteContact.HRef);
                q.Set("Email", user.Email);
                q.Set("Subject", "Integration Portal: Account recovery");
                if (!string.IsNullOrEmpty(externalId)) q.Set("Message", "Group ID: " + externalId);
                linkPageNoteContact.HRef = q.BuildQuery();
            }
            else
            {
                panelPageNote.InnerHtml = "Invalid email parameters, please contact the support team.";
                mvForgotPassword.SetActiveView(viewPageNote);
            }
        }

        protected void cmdPerformReset_Click(object sender, EventArgs e)
        {
            var userId = DataUtil.GetId(hUserId.Value);
            var context = new WContext(this);
            var element = context.ParameterizedObject;
            var parameterSetName = element.GetParameterValue("ResetPasswordParameterSet");
            WebParameterSet parameterSet = !string.IsNullOrEmpty(parameterSetName) ? WebParameterSet.Get(parameterSetName) : null;
            if (parameterSet != null)
            {
                // If did not provide email, ask whether they still have access to the email
                // If no access, ask for new email, put new email in approval Q by admin / 2 member approvals, take them to contact page
                var user = WebUser.Get(userId);
                var codeGen = new OtpCodeGenerator(user.Id);
                int sendVia = DataUtil.GetInt32(parameterSet.GetParameterValue("Send-Via", "2")); // Default is Both
                var smsTemplate = parameterSet.GetParameterValue("SMS-Template");
                var emailTemplate = parameterSet.GetParameterValue("Email-Template");
                var emailSubjectTemplate = parameterSet.GetParameterValue("EmailSubject-Template");
                var successTemplate = parameterSet.GetParameterValue("Success-Template");

                var emailTemplatePath = parameterSet.GetParameterValue("EmailTemplatePath");
                var msg = new WebMessageQueue();
                msg.To = string.Format("{0};{1}", user.Email, user.MobileNumber);
                msg.SendVia = sendVia;
                msg.ToOrBcc = MessageToOrBccStatus.ToGroup;

                var baseAddress = parameterSet.GetParameterValue("BaseAddress");
                if (string.IsNullOrEmpty(baseAddress))
                    baseAddress = WConfig.BaseAddress;

                var loginUrl = element.GetParameterValue(LoginUrl);
                if (string.IsNullOrEmpty(loginUrl))
                    loginUrl = parameterSet.GetParameterValue(LoginUrl);
                if (loginUrl.StartsWith("/"))
                    loginUrl = WebHelper.CombineAddress(string.IsNullOrEmpty(baseAddress) ? WConfig.BaseAddress : baseAddress, loginUrl);

                var provider = new NamedValueProvider();
                if ((!string.IsNullOrEmpty(emailTemplate) || !string.IsNullOrEmpty(emailTemplatePath)) && !string.IsNullOrEmpty(emailSubjectTemplate)
                    && (msg.SendVia == MessageSendVia.Email || msg.SendVia == MessageSendVia.EmailAndSms))
                {
                    // Format email content here
                    string emailSubject = Substituter.Substitute(emailSubjectTemplate, provider);
                    provider.Add("MemberFirstName", AccountHelper.GetPrefixedName(user, true));
                    provider.Add("Username", user.UserName);
                    provider.Add("Password", codeGen.OtpCode);
                    provider.Add("BaseAddress", baseAddress.TrimEnd('/'));
                    provider.Add("Title", emailSubject);
                    provider.Add("LoginUrl", loginUrl);

                    if (!string.IsNullOrEmpty(emailTemplatePath))
                    {
                        // Template From Path
                        emailTemplate = FileHelper.ReadFile(MapPath(emailTemplatePath));
                        var message = Substituter.Substitute(emailTemplate, provider);
                        msg.EmailMessage = WebMailMessage.FixPaths(message);
                        msg.EmailSubject = emailSubject;
                    }
                    else
                    {
                        string emailBody = Substituter.Substitute(emailTemplate, provider);
                        // Prepare Email
                        provider.Add("Title", emailSubject);
                        provider.Add("Content", emailBody);

                        string emailPath = "~/Content/Parts/Messaging/Templates/EmailMessage.htm";
                        string message = FileHelper.ReadFile(MapPath(emailPath));
                        message = Substituter.Substitute(message, provider);
                        // Format relative paths to absolute
                        msg.EmailMessage = WebMailMessage.FixPaths(message);
                        msg.EmailSubject = WebMailMessage.PrefixSubject(emailSubject);
                    }
                }

                if (!string.IsNullOrEmpty(smsTemplate) && (msg.SendVia == MessageSendVia.Sms || msg.SendVia == MessageSendVia.EmailAndSms))
                {
                    // Prepare SMS
                    provider = new NamedValueProvider();
                    provider.Add("MemberFirstName", AccountHelper.GetPrefixedName(user, true));
                    provider.Add("Username", user.UserName);
                    provider.Add("Password", codeGen.OtpCode);
                    msg.SmsMessage = Substituter.Substitute(smsTemplate, provider);
                }
                msg.Update();

                try
                {
                    AgentHelper.ExecuteTask(MessageProcessorTask.TASK_NAME);
                    user.Password = codeGen.OtpCode;
                    user.PasswordExpiryDate = WConstants.ExpiredPasswordDate;
                    user.Update(false);
                }
                catch (Exception ex)
                {
                    DisplayMessage("There was an error sending the password reset message. However, it was placed on queue by the messaging server and will be sent on the next schedule.");
                    LogHelper.WriteLog(ex);
                }

                provider = new NamedValueProvider();
                provider.Add("Email", user.Email);
                provider.Add("Mobile", string.IsNullOrEmpty(user.MobileNumber) ? "NOT APPLICABLE" : user.MobileNumber);
                provider.Add(LoginUrl, loginUrl);
                panelPageNote.InnerHtml = Substituter.Substitute(successTemplate, provider);
                mvForgotPassword.SetActiveView(viewPageNote);
            }
            else
            {
                // Template name not found.
                DisplayMessage("There was a problem setting-up the password reset message.");
                return;
            }
        }

        protected void cmdGoToLogin_Click(object sender, EventArgs e)
        {
            var externalId = hCID.Value.Trim();
            var query = new WQuery(this);
            query.Set("Username", externalId);
            query.Redirect(hLoginUrl.Value);
        }

        protected void cmdRegisterNewAccount_Click(object sender, EventArgs e)
        {
            var externalId = hCID.Value.Trim();
            var userNameOrEmail = hEmail.Value.Trim();
            var dateOfMembershipString = hDOB.Value.Trim();
            var dateOfMembership = DataUtil.GetDateTime(dateOfMembershipString, WConstants.DateTimeMinValue);

            var profile = new MemberProfileModel();
            profile.Email = userNameOrEmail;
            profile.ExternalId = externalId;

            profile.DateOfMembership = dateOfMembership;
            Session[IntegrationAppSessions.MemberProfile] = profile;

            var query = new WQuery(this);
            query.SetOpen("Register");
            query.Redirect();
        }
    }
}