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
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.WebParts.Profile
{
    public partial class ForgotPassword : System.Web.UI.UserControl
    {
        private const string LoginUrl = "LoginUrl";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var context = new WContext(this);
                var element = context.Element;

                // Forgot Password
                var loginUrl = element.GetParameterValue(LoginUrl);
                if (!string.IsNullOrEmpty(loginUrl))
                {
                    var query = context.Query.Clone();
                    query.BasePath = loginUrl;

                    linkLogin.HRef = loginUrl;
                }
            }
        }

        protected void cmdRetrieve_Click(object sender, EventArgs e)
        {
            bool validated = false;

            var externalId = txtExternalId.Text.Trim();
            if (!string.IsNullOrEmpty(externalId))
            {
                var userNameOrEmail = txtEmail.Text.Trim();
                var dateOfMembershipString = txtDateOfMembership.Text.Trim();

                if (string.IsNullOrEmpty(userNameOrEmail) && string.IsNullOrEmpty(dateOfMembershipString))
                {
                    lblMessage.Text = "Please enter either your Date Of Membership or E-mail.";
                    return;
                }

                var isAccountDirty = false;
                MemberLink link = null;
                WebUser user = null;
                do
                {
                    link = MemberLink.Provider.Get(externalId);
                    if (link != null)
                    {
                        user = link.User;
                        if (user == null)
                        {
                            // Perform clean-up
                            link.Delete();
                            isAccountDirty = true;
                            continue;
                        }
                    }
                    isAccountDirty = false;
                } while (isAccountDirty);

                if (link != null)
                {
                    if (!link.IsApproved)
                    {
                        lblMessage.Text = "You Integration Account is not activated, please use the contact form to contact the Portal admin.";
                        return;
                    }

                    if (!user.IsActive)
                    {
                        lblMessage.Text = "You Account is not activated, please use the contact form to contact the Portal admin.";
                        return;
                    }

                    DateTime dateOfMembership = DataUtil.GetDateTime(dateOfMembershipString, WConstants.DateTimeMinValue);

                    // Check UserName or Email
                    if (!string.IsNullOrEmpty(userNameOrEmail) && (user.UserName.Equals(userNameOrEmail, StringComparison.InvariantCultureIgnoreCase)
                        || user.Email.Equals(userNameOrEmail, StringComparison.InvariantCultureIgnoreCase)
                        || user.EmailId.Equals(userNameOrEmail, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        validated = true;
                    }

                    // Check Date of Membership
                    if (!validated && dateOfMembership != WConstants.DateTimeMinValue && link.MembershipDate.Date == dateOfMembership.Date)
                        validated = true;

                    if (validated)
                    {
                        var context = new WContext(this);
                        var element = context.ParameterizedObject;
                        var parameterSetName = element.GetParameterValue("MessageParameterSet");
                        var parameterSet = !string.IsNullOrEmpty(parameterSetName) ? WebParameterSet.Get(parameterSetName) : context.GetParameterSet();
                        if (user.ProviderId == AccountConstants.DefaultExternalProvider)
                        {
                            // ONE Account
                            string ONENote = ParameterizedWebObject.GetValue("CannotResetONEAccountNote", element, parameterSet);
                            if (string.IsNullOrEmpty(ONENote))
                                ONENote = "Cannot perform Password Reset on your ONE account. Please perform the Password Reset via the ONE website.";

                            mvForgotPassword.SetActiveView(viewPageNote);
                            panelPageNote.InnerHtml = ONENote;
                        }
                        else if (parameterSet != null)
                        {
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
                            msg.EnableMonitor = false;

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
                                lblMessage.Text = "There was an error sending the password reset message. However, it was placed on queue by the messaging server and will be sent on the next schedule.";
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
                            lblMessage.Text = "There was problem setting-up the password reset message.";
                        }
                    }
                }
            }

            if (!validated)
                lblMessage.Text = "An account with the credentials you provided does not exist.";
        }
    }
}