using System;
using System.Text;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Reflection;
using System.IO;

using WCMS.WebSystem.Controls;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework.Utilities;
using WCMS.Framework;
using WCMS.Framework.Net;
using WCMS.Framework.Core;
using WCMS.Framework.Core.Shared;
using WCMS.Framework.Security;

using WCMS.WebSystem.Agent;
using WCMS.WebSystem.Apps.Integration.ExtApp;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.ExternalMemberWS;

namespace WCMS.WebSystem.Apps.Integration.Account
{
    public partial class RegistrationV2 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblStatus.Visible = false;
            lblAlert.Visible = false;

            if (!IsPostBack)
            {
                var context = new WContext(this);
                var element = context.ParameterizedObject;
                var set = context.GetParameterSet();

                var loginUrl = ParameterizedWebObject.GetValue(AccountConstants.LoginUrl, element, set);
                var disableLocalRegistration = DataUtil.GetBool(ParameterizedWebObject.GetValue(AccountConstants.DisableLocalRegistration, element, set), false);
                var enableExternalAccounts = DataUtil.GetBool(ParameterizedWebObject.GetValue(AccountConstants.EnableExternalAccounts, element, set), false);

                var loginInfoObj = Session[AccountConstants.LoginInfoSessionKey];
                if (disableLocalRegistration && loginInfoObj == null)
                {
                    MultiView1.SetActiveView(viewRegisterDisabled);
                    return;
                }

                cboCountries.DataSource = Country.GetList();
                cboCountries.DataBind();

                var photoRequired = DataUtil.GetBool(element.GetParameterValue("PhotoRequired"), false);
                if (photoRequired)
                    panelSkip.Visible = false;

                bool hasTerms = false;
                var terms = element.GetParameterValue("TermsAndAgreementContent");
                if (!string.IsNullOrEmpty(terms))
                {
                    hasTerms = true;
                    panelTerms.InnerHtml = terms;
                    hHasTerms.Value = "1";

                    var termsCancelUrl = element.GetParameterValue("TermsCancelUrl");
                    if (!string.IsNullOrEmpty(termsCancelUrl))
                        linkDisagreeTerms.HRef = termsCancelUrl;
                }

                if (enableExternalAccounts)
                {
                    if (loginInfoObj != null)
                    {
                        var loginInfo = (KeyValuePair<string, string>)loginInfoObj;
                        var oneAppName = element.GetParameterValue(ExtApp.ExtAppConstants.PARAM_APP_ONE_NAME);

                        var userInfo = ExtAppProvider.GetUserInfo(loginInfo.Key, context);
                        if (userInfo != null)
                        {
                            // Check username and email if existing
                            var user = WebUser.GetByEmail(userInfo.Email);
                            if (user == null)
                                user = WebUser.Get(userInfo.UserName);

                            // Check Group ID if in use
                            var link = MemberLink.Provider.Get(userInfo.ExternalId);
                            if (user != null || link != null)
                                DisplayAlertMessage("NOTE: Your existing Portal account will be linked to your Integration Ext account.");

                            userInfo.Password = loginInfo.Value;
                            Session[IntegrationAppSessions.EXT_USER_INFO_KEY] = userInfo;

                            hLinkExternalRegister.Value = "1";
                            MultiView1.SetActiveView(viewONEFirstTime);
                        }
                        else
                        {
                            MultiView1.SetActiveView(viewRegisterDisabled);
                        }
                        return;
                    }
                }

                var info = Session[IntegrationAppSessions.MemberProfile] as MemberProfileModel;
                if (info != null)
                {
                    // No existing account
                    txtRegisterEmail.Text = info.Email;
                    txtMembershipDate.Text = info.DateOfMembership.ToString("yyyy-MM-dd");
                    txtExternalID.Text = info.ExternalId;
                    //if (!string.IsNullOrEmpty(info.ExternalId))
                    //{
                    //    txtUserName.Text = info.ExternalId;
                    //}
                    //else
                    //{
                    //    txtUserName.Text = info.Email;
                    //}

                    MultiView1.SetActiveView(viewEnterInfo);
                    return;
                }

                var open = context.GetOpen();
                if (!string.IsNullOrEmpty(open))
                {
                    context.RemoveOpen();
                    context.Redirect();
                    return;
                }

                if (hasTerms)
                    MultiView1.SetActiveView(viewTerms);
                else
                    MultiView1.SetActiveView(viewEnterEmail);
            }
        }

        protected void cmdEmailNext_Click(object sender, EventArgs e)
        {
            string email = txtEnterEmail.Text.Trim();
            if (!string.IsNullOrEmpty(email) && Validator.IsRegexMatch(email, RegexPresets.Email))
            {
                var user = WebUser.GetByEmail(email);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        // Existing Portal User
                        txtPortalEmail.Text = user.Email;
                        MultiView1.SetActiveView(viewPortalUser);
                    }
                    else
                    {
                        // Existing Portal User but not activated.
                        DisplayErrorMessage("This account already exists and is not activated, please contact ADDCIT.");
                    }
                }
                else
                {
                    // First time email
                    txtRegisterEmail.Text = email;
                    //txtUserName.Text = email;
                    MultiView1.SetActiveView(viewEnterInfo);
                }
            }
            else
            {
                // Invalid E-mail entered
                DisplayErrorMessage("Please enter a valid email address.");
            }
        }

        protected void cmdPortalFinish_Click(object sender, EventArgs e)
        {
            // TODO: Implement code for Portal-registered users

            string email = txtEnterEmail.Text.Trim();
            if (!string.IsNullOrEmpty(email) && Validator.IsRegexMatch(email, RegexPresets.Email))
            {
                var user = WebUser.GetByEmail(email);
                if (user != null && user.IsActive)
                {
                    string password = txtPassword.Text;

                    user = AccountHelper.ValidateLogin(user.UserName, password);
                    if (user != null && user.IsActive)
                    {
                        var link = MemberLink.Provider.GetByUserId(user.Id);
                        if (link != null)
                        {
                            var context = new WContext(this);
                            var paramSet = context.GetParameterSet();

                            string groups = paramSet.GetParameterValue("GroupsToAdd");
                            user.AddToGroups(groups, RecordStatus.Inactive);

                            // Send E-mail to User
                            if (!SendEmailToUser(user, link, paramSet))
                                return;

                            // Send E-mail to Approver
                            if (!SendEmailToAdmin(user, link, paramSet))
                                return;

                            MultiView1.SetActiveView(viewFinish);

                            return;
                        }
                    }
                }
            }

            DisplayErrorMessage("Invalid account or incorrect password.");
        }

        protected void cmdRegister_Click(object sender, EventArgs e)
        {
            Register(true);
        }

        protected void cmdInfoNext_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(viewPhotoUploader);
        }

        protected void cmdUploadSkip_Click(object sender, EventArgs e)
        {
            Register();
        }

        protected void cmdUpload_Click(object sender, EventArgs e)
        {
            UploadPhoto();
        }

        protected void cmdReUpload_Click(object sender, EventArgs e)
        {
            MultiViewPhotoUpload.SetActiveView(viewUpload);
        }

        protected void cmdAgree_Click(object sender, EventArgs e)
        {
            // From Terms view
            bool isExternalRegister = DataUtil.GetBool(hLinkExternalRegister.Value, false);
            if (isExternalRegister)
                BeginONERegistration();
            else
                MultiView1.SetActiveView(viewEnterEmail);
        }

        protected void cmdONESetupContinue_ServerClick(object sender, EventArgs e)
        {
            // Skip Terms view if no terms content.
            bool hasTerms = DataUtil.GetBool(hHasTerms.Value, false);
            if (hasTerms)
                MultiView1.SetActiveView(viewTerms);
            else
                BeginONERegistration();
        }

        protected void cmdONESetupCancel_ServerClick(object sender, EventArgs e)
        {
            Session[AccountConstants.LoginInfoSessionKey] = null;

            var context = new WContext(this);
            var element = context.GetParameterSet();
            var loginUrl = element.GetParameterValue(AccountConstants.LoginUrl);

            context.Redirect(loginUrl);
        }

        #region Local Methods

        private void DisplayErrorMessage(string message)
        {
            lblStatus.InnerHtml = message;
            lblStatus.Visible = true;
        }

        private void DisplayAlertMessage(string message)
        {
            lblAlert.InnerHtml = message;
            lblAlert.Visible = true;
        }

        private void BeginONERegistration()
        {
            var loginInfoObj = Session[AccountConstants.LoginInfoSessionKey];
            if (loginInfoObj != null)
            {
                var userInfo = Session[IntegrationAppSessions.EXT_USER_INFO_KEY] as ExtAppUserInfo;
                if (userInfo != null)
                {
                    var isAccountDirty = false;
                    MemberLink link = null;
                    WebUser user = null;
                    do
                    {
                        user = null;
                        link = MemberLink.Provider.Get(userInfo.ExternalId);
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

                    // Check username and email if existing, not linked to Member
                    if (user == null)
                    {
                        user = WebUser.GetByEmail(userInfo.Email);
                        if (user == null)
                            user = WebUser.Get(userInfo.UserName);
                    }

                    // Check Group ID if in use
                    if (user != null || link != null)
                    {
                        if (user != null) // 1 or both not null
                        {
                            if (!user.UserName.Equals(userInfo.UserName, StringComparison.InvariantCultureIgnoreCase))
                                user.UserName = userInfo.UserName;

                            if (user.Status == AccountStatus.PENDING)
                                user.IsActive = true;

                            user.ProviderId = AccountConstants.DefaultExternalProvider;
                            user.Update();
                        }
                        //else // both null, seems impossible to reach
                        //{
                        //    user = new WebUser();
                        //    user.FirstName = userInfo.FirstName;
                        //    user.LastName = userInfo.LastName;
                        //    user.Email = userInfo.Email;
                        //    user.UserName = userInfo.UserName;
                        //    user.IsActive = true;
                        //    user.ProviderId = AccountConstants.DefaultExternalProvider;
                        //    user.Update();
                        //    if (link != null)
                        //    {
                        //        link.UserId = user.Id;
                        //        link.Update();
                        //    }
                        //}

                        if (link == null)
                        {
                            link = MemberLink.Provider.GetByUserId(user.Id);
                            if (link == null)
                            {
                                link = new MemberLink();
                                link.UserId = user.Id;
                            }
                            link.ExternalIdNo = userInfo.ExternalId;
                            link.Update();
                        }

                        MultiView1.SetActiveView(viewONELinkFinish);
                        return;
                    }

                    //DisplayErrorMessage("Your Group ID is already in use, please contact ADDCIT to resolve this issue.");
                    //DisplayErrorMessage("Your ONE Email is already in use, please contact ADDCIT to resolve this issue.");
                    //DisplayErrorMessage("Your ONE User Name is already in use, please contact ADDCIT to resolve this issue.");

                    // Begin ONE registration/linking
                    MultiView1.SetActiveView(viewEnterInfo);

                    //txtUserName.Text = userInfo.UserName;
                    //txtUserName.ReadOnly = true;

                    txtFirstName.Text = userInfo.FirstName;
                    txtLastName.Text = userInfo.LastName;

                    txtRegisterEmail.Text = userInfo.Email;
                    txtRegisterEmail.ReadOnly = true;

                    txtExternalID.Text = userInfo.ExternalId;
                    txtExternalID.ReadOnly = true;

                    //userInfo.Password = loginInfo.Value;
                    //Session[ONEConstants.EXT_USER_INFO_KEY] = userInfo;
                }
            }
        }

        private void UploadPhoto()
        {
            var context = new WContext(this);
            var element = context.Element;

            var photoPath = WConfig.UserPhotoPath; //site.GetParameterValue("WCMS.UserPhotoPath", "/Content/Assets/User-Photos");
            var photoSize = DataUtil.GetInt32(element.GetParameterValue("PhotoSize"), 600);

            if (photoUpload.HasFile)
            {
                var fileName = photoUpload.PostedFile.FileName;
                if (ImageUtil.IsValidImage(fileName))
                {
                    var userId = (int)DateTime.UtcNow.TimeOfDay.TotalMinutes;
                    hExtension.Value = Path.GetExtension(fileName);
                    hUserId.Value = userId.ToString();
                    imagePreview.ImageUrl = AccountHelper.UploadPhotoForPreview(userId, photoUpload, photoSize);
                    MultiViewPhotoUpload.SetActiveView(viewPreview);
                }
                else
                {
                    DisplayErrorMessage("The file you uploaded is not a valid image file.");
                }
            }
        }

        private void Register(bool hasPhoto = false)
        {
            var email = txtRegisterEmail.Text.Trim();
            var externalId = txtExternalID.Text.Trim();
            //var userName = txtUserName.Text.Trim();

            ExtAppUserInfo userInfo = null;
            bool isExternalRegister = DataUtil.GetBool(hLinkExternalRegister.Value, false);
            if (isExternalRegister)
            {
                var loginInfoObj = Session[AccountConstants.LoginInfoSessionKey];
                if (loginInfoObj != null)
                {
                    var loginInfo = (KeyValuePair<string, string>)loginInfoObj;
                    userInfo = Session[IntegrationAppSessions.EXT_USER_INFO_KEY] as ExtAppUserInfo;
                    if (userInfo != null)
                    {
                        email = userInfo.Email;
                        externalId = userInfo.ExternalId;
                        //userName = userInfo.UserName;
                    }
                    else
                        isExternalRegister = false;
                }
                else
                    isExternalRegister = false;
            }

            // Check email and username if already in use.

            var user = WebUser.Get(externalId);
            if (user != null)
            {
                DisplayErrorMessage("Your Group ID is already taken.");
                return;
            }

            var link = MemberLink.Provider.Get(externalId);
            if (link != null)
            {
                DisplayErrorMessage("Group ID already in use by another user, please contact ADDCIT.");
                return;
            }

            //var code = new OtpCodeGenerator();
            var context = new WContext(this);
            var paramSet = context.GetParameterSet();

            user = new WebUser();
            user.UserName = !string.IsNullOrEmpty(externalId) ? externalId : email;
            user.FirstName = txtFirstName.Text.Trim();
            user.LastName = txtLastName.Text.Trim();
            user.Email = email;
            user.MobileNumber = txtMobileNumber.Text.Trim();
            user.Gender = char.Parse(cboGender.SelectedValue);

            if (isExternalRegister)
            {
                //user.Password = userInfo.Password;
                user.Password = SecurityHelper.CreatePasswordHash(userInfo.Password, SecurityHelper.SALT);
                user.IsActive = true;
                user.ProviderId = AccountConstants.DefaultExternalProvider;
            }
            else
            {
                user.Password = OtpCodeGenerator.Generate();
                user.PasswordExpiryDate = WConstants.ExpiredPasswordDate;
            }

            user.Update();


            #region Photo Upload

            if (hasPhoto)
            {
                var element = context.Element;
                var photoPathUrl = WConfig.UserPhotoPath; //site.GetParameterValue("WCMS.UserPhotoPath", "/Content/Assets/User-Photos");
                var thumbSize = DataUtil.GetInt32(element.GetParameterValue("ThumbSize"), 200);
                var ext = hExtension.Value;

                AccountHelper.FinalizePhotoUpload(user, DataUtil.GetId(hUserId.Value), ext, thumbSize);
            }

            #endregion

            string groups = paramSet.GetParameterValue("GroupsToAdd");
            user.AddToGroups(groups);

            // TODO: Check existing memberId on External

            var countryCode = DataUtil.GetInt32(cboCountries.SelectedValue);

            link = new MemberLink();
            link.Approved = MemberAccountStatus.Approved;
            link.UserId = user.Id;
            link.ExternalIdNo = externalId;
            link.MembershipDate = DataUtil.GetDateTime(txtMembershipDate.Text.Trim());
            link.Locale = txtLocale.Text.Trim();
            link.LocaleCountryCode = countryCode;
            link.IsPrivate = true;
            link.Update();

            var address = user.NewAddress(AddressTags.Home);
            address.CountryCode = countryCode;
            address.Update();

            Session[AccountConstants.LoginInfoSessionKey] = null;
            Session[IntegrationAppSessions.EXT_USER_INFO_KEY] = null;
            Session[IntegrationAppSessions.MemberProfile] = null;

            if (!isExternalRegister)
            {
                // Send E-mail to User
                if (!SendEmailToUser(user, link, paramSet))
                    return;

                // Send E-mail to Approver
                if (!SendEmailToAdmin(user, link, paramSet))
                    return;

                MultiView1.SetActiveView(viewFinish);
            }
            else
            {
                // Check external External
                if (link.MembershipDate <= WConstants.DateTimeMinValue)
                {
                    var client = new MemberSoapClient(false);
                    var member = client.GetProfile(externalId, link.MembershipDate);
                    if (member != null)
                    {
                        // Add to SG group
                        string sgGroup = paramSet.GetParameterValue("SingaporeGroup");
                        if (!string.IsNullOrEmpty(sgGroup))
                            user.AddToGroup(sgGroup); // Parameterized this
                    }
                }

                MultiView1.SetActiveView(viewONEFinish);
            }
        }

        private bool SendEmailToAdmin(WebUser user, MemberLink link, ParameterizedWebObject paramSet)
        {
            var emailTemplate = FileHelper.ReadFile(MapPath(paramSet.GetParameterValue("AccountCreatedEmailToAdmin")));
            var accountReviwers = paramSet.GetParameterValue("AccountReviewer");
            var baseAddress = paramSet.GetParameterValue("BaseAddress");
            var emailSubjectTemplate = paramSet.GetParameterValue("AccountCreatedEmailToAdminSubject", "Music Ministry Portal: New Account Approval Request");

            var approvalUrl = paramSet.GetParameterValue("ApprovalUrl");
            if (approvalUrl.StartsWith("/"))
                approvalUrl = WebUtil.CombineAddress(string.IsNullOrEmpty(baseAddress) ? WConfig.BaseAddress : baseAddress, approvalUrl);

            var country = link.LocaleCountry;
            var userPhotoUrl = link.GetPhotoPathIfNull();

            var provider = new NamedValueProvider();
            provider.Add("CHURCH_ID_NO", link.ExternalIdNo);
            provider.Add("MEMBERSHIP_DATE", link.MembershipDate.ToString("dd-MMM-yyyy"));
            provider.Add("BASE_ADDRESS", baseAddress);
            //provider.Add("PHOTO_URL", link.GetPhotoPathIfNull("200x200"));
            provider.Add("PHOTO_URL", WebUtil.BuildAddress(baseAddress, string.IsNullOrEmpty(userPhotoUrl) ? WConstants.NoPhotoThumb : userPhotoUrl));
            provider.Add("FIRST_NAME", user.FirstName);
            provider.Add("LAST_NAME", user.LastName);
            provider.Add("MEMBER_NAME", AccountHelper.GetPrefixedName(user, true));
            provider.Add("EMAIL", user.Email);
            provider.Add("USER_NAME", user.UserName);
            provider.Add("MOBILE", user.MobileNumber);
            provider.Add("LOCALE", link.Locale);
            provider.Add("COUNTRY", country != null ? country.CountryName : "");
            provider.Add("APPROVAL_URL", approvalUrl);

            var emailContent = Substituter.Substitute(emailTemplate, provider);
            var emailSubject = Substituter.Substitute(emailSubjectTemplate, provider);
            var recipients = AccountHelper.CollectEmailString(accountReviwers);
            recipients.AddRange(AccountHelper.CollectMobileNumbers(accountReviwers));
            var to = DataUtil.ToDelimitedString(recipients, ';');

            var message = WebMessageQueue.Create(emailContent, string.Empty, MessageSendVia.Email, to, emailSubject, null);
            message.EnableMonitor = false;
            message.Update();
            AgentHelper.ExecuteTask(MessageProcessorTask.TASK_NAME);
            return true;
        }

        private bool SendEmailToUser(WebUser user, MemberLink link, ParameterizedWebObject paramSet)
        {
            var emailTemplate = FileHelper.ReadFile(MapPath(paramSet.GetParameterValue("AccountCreatedEmailToUser")));
            var baseAddress = paramSet.GetParameterValue("BaseAddress");
            var emailSubjectTemplate = paramSet.GetParameterValue("AccountCreatedEmailToUserSubject", "Music Ministry Portal: Your New Account Is Pending Approval");

            /*
            var loginUrl = paramSet.GetParameterValue("LoginUrl");
            if (loginUrl.StartsWith("/"))
                loginUrl = WebHelper.CombineAddress(string.IsNullOrEmpty(baseAddress) ? WConfig.BaseAddress : baseAddress, loginUrl);
            */

            var country = link.LocaleCountry;
            var userPhotoUrl = link.GetPhotoPathIfNull();

            var provider = new NamedValueProvider();
            provider.Add("CHURCH_ID_NO", link.ExternalIdNo);
            provider.Add("MEMBERSHIP_DATE", link.MembershipDate.ToString("dd-MMM-yyyy"));
            provider.Add("BASE_ADDRESS", baseAddress);
            //provider.Add("PHOTO_URL", member.GetPhotoPath("200x200"));
            provider.Add("PHOTO_URL", WebUtil.BuildAddress(baseAddress, string.IsNullOrEmpty(userPhotoUrl) ? WConstants.NoPhotoThumb : userPhotoUrl));
            provider.Add("FIRST_NAME", user.FirstName);
            provider.Add("LAST_NAME", user.LastName);
            provider.Add("MOBILE", user.MobileNumber);
            provider.Add("EMAIL", user.Email);
            provider.Add("USER_NAME", user.UserName);
            //provider.Add("PASSWORD", user.Password);
            provider.Add("MEMBER_NAME", AccountHelper.GetPrefixedName(user, true));
            provider.Add("LOCALE", link.Locale);
            provider.Add("COUNTRY", country != null ? country.CountryName : "");
            //provider.Add("URL", loginUrl);

            var emailContent = Substituter.Substitute(emailTemplate, provider);
            var emailSubject = Substituter.Substitute(emailSubjectTemplate, provider);

            var message = WebMessageQueue.Create(emailContent, string.Empty, MessageSendVia.Email, user.Email, emailSubject, null);
            message.EnableMonitor = false;
            message.Update();
            AgentHelper.ExecuteTask(MessageProcessorTask.TASK_NAME);
            return true;
        }

        #endregion
    }
}