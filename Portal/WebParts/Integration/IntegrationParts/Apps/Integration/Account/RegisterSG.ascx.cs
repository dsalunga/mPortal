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
using WCMS.Framework.Core;
using WCMS.Framework.Net;

using WCMS.WebSystem.Agent;
using WCMS.WebSystem.Apps.Integration.ExternalMemberWS;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.Integration.Account
{
    public partial class Registration : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var context = new WContext(this);
                var element = context.Element;
                var noPhotoPath = WebRegistry.SelectNodeValue(MemberConstants.NoPhotoPathKey);
                var loginUrl = element.GetParameterValue("LoginUrl");
                if (!string.IsNullOrEmpty(loginUrl))
                    linkLogin.HRef = loginUrl;

                var localeFilter = element.GetParameterValue("LocaleFilter");
                if (!string.IsNullOrEmpty(localeFilter))
                    hLocaleFilter.Value = localeFilter;

                imgPhoto.ImageUrl = noPhotoPath;
                Session.Remove("Member");

                if (context.Get("NoExternal") == "1")
                {
                    lblTitle.InnerHtml += " - No External Account";

                    // Hide unnecessary items

                    rowLocale.Visible = true;
                    rowContact.Visible = true;
                    rowAddress.Visible = true;

                    // Show/enable required items
                    btnRegister.Enabled = true;

                    txtFirstName.ReadOnly = false;
                    txtLastName.ReadOnly = false;
                    txtEmail.ReadOnly = false;
                    txtMobileNumber.ReadOnly = false;

                    txtMiddleName.Visible = true;

                    EnableNameAndAccountInfo(true);
                    EnableRetrieve(false);

                    // Validation
                    //txtMembershipDate.ValidationGroup = "register";
                    //rfvMembershipDate.ValidationGroup = "register";

                    // With External help link
                    context.Remove("NoExternal");
                    linkWithExternal.HRef = context.BuildQuery();
                    linkNoExternal.Visible = false;
                }
                else
                {
                    // No External help link
                    context.Set("NoExternal", "1");
                    linkNoExternal.HRef = context.BuildQuery();
                    linkWithExternal.Visible = false;

                    rfvEmail.ErrorMessage = "Registration cannot continue without your email. Please contact the ADDCIT team or the Locale Secretary.";

                    panelMemberInfo.Visible = false;
                    panelMemberInfoHr.Visible = false;

                    var profile = Session[IntegrationAppSessions.MemberProfile] as MemberProfileModel;
                    if (profile != null)
                    {
                        //txtExternalIDNo.Text = 
                        RetrieveAccount(profile.ExternalId, profile.DateOfMembership);
                    }
                }
            }
        }

        protected void btnRetrieve_Click(object sender, EventArgs e)
        {
            RetrieveAccount();
        }

        private void RetrieveAccount(string externalId = null, DateTime? dob = null)
        {
            var externalIdNo = string.IsNullOrEmpty(externalId) ? txtExternalIDNo.Text.Trim() : externalId;
            var membershipDate = dob == null ? DataUtil.GetDateTime(txtMembershipDate.Text.Trim()) : dob.Value;

            if (membershipDate != (new DateTime(0)) && !string.IsNullOrEmpty(externalIdNo))
            {
                var isAccountDirty = false;
                MemberLink link = null;
                WebUser user = null;
                do
                {
                    user = null;
                    link = MemberLink.Provider.Get(externalIdNo);
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

                // Make sure records are clean
                if (link != null && (link.Approved != MemberAccountStatus.Approved || !user.IsActive))
                    link = null;

                if (link == null)
                {
                    var client = new MemberSoapClient(false);
                    var member = client.GetProfile(externalIdNo, membershipDate);
                    if (member != null)
                    {
                        bool validated = true;
                        var localeFilter = hLocaleFilter.Value;

                        if (!string.IsNullOrEmpty(localeFilter))
                        {
                            var slashIdx = localeFilter.IndexOf('/');
                            if (slashIdx > 0)
                            {
                                var localeId = DataUtil.GetId(localeFilter.Substring(0, slashIdx));
                                var localeName = localeFilter.Substring(slashIdx + 1);

                                if (localeId > 0 && !string.IsNullOrEmpty(localeName))
                                {
                                    var status = client.GetMembershipStatus(member.MemberID);
                                    if (status == null || status.LocaleID != localeId)
                                    {
                                        InvalidateRegInfo(string.Format("Sorry, only External-registered members in {0} are allowed to register in the Portal.<br/>For assistance, kindly contact the ADDCIT team or the Locale Secretary.", localeName));
                                        validated = false;
                                    }
                                }
                            }
                        }

                        if (validated)
                        {
                            Session["Member"] = member;

                            txtFirstName.Text = member.FirstName;
                            txtLastName.Text = member.LastName;
                            txtEmail.Text = member.Email;
                            txtMobileNumber.Text = member.Mobile;

                            var photoPath = member.GetPhotoPath("200x200");
                            if (!string.IsNullOrEmpty(photoPath))
                                imgPhoto.ImageUrl = photoPath;

                            //txtUserName.Text = member.GetSuggestedUserName(); // externalIdNo.ToLower();

                            EnableLookUpFields(false);
                            EnableNameAndAccountInfo(true);

                            btnRegister.Enabled = true;
                            panelMemberInfo.Visible = true;
                            panelMemberInfoHr.Visible = true;

                            member = null;
                        }
                    }
                    else
                    {
                        InvalidateRegInfo("Your Group profile does not exist in Integration External System.<br/>For assistance, kindly contact the ADDCIT team or the Locale Secretary <br/>or use the registration for non-External users by clicking the link next to Group ID field.");
                    }
                }
                else
                {
                    InvalidateRegInfo("The Group ID you entered is already in use.<br/>Please use the Forgot Password feature to reset your password. <br/>Please contact the ADDCIT team or the Locale Secretary for any issues.");
                }
            }
            else
            {
                InvalidateRegInfo("Invalid Membership Date format. Please enter a valid Membership Date in format YYYY-MM-DD.<br/>For assistance, kindly contact the ADDCIT team or the Locale Secretary.");
            }
        }

        private void EnableRetrieve(bool enable)
        {
            imgPhoto.Visible = enable;
            btnRetrieve.Visible = enable;
            rfvExternalIDNo.Enabled = enable;
        }

        private void EnableNameAndAccountInfo(bool enable)
        {
            txtFirstName.Enabled = enable;
            txtLastName.Enabled = enable;
            txtEmail.Enabled = enable;
            txtMobileNumber.Enabled = enable;

            //txtUserName.Enabled = enable;
            //txtPwd01.Enabled = enable;
            //txtPwd02.Enabled = enable;
        }

        private void EnableLookUpFields(bool value)
        {
            txtExternalIDNo.ReadOnly = !value;
            txtMembershipDate.ReadOnly = !value;
        }

        private void InvalidateRegInfo(string msg)
        {
            lblMsg1.Text = msg;
            mpeMsg1.Show();

            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtMobileNumber.Text = "";

            EnableNameAndAccountInfo(false);

            imgPhoto.ImageUrl = WebRegistry.SelectNodeValue(MemberConstants.NoPhotoPathKey);

            btnRegister.Enabled = false;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            Member member = null;

            var membershipDate = DataUtil.GetDateTime(txtMembershipDate.Text.Trim());
            if (membershipDate != (new DateTime(0)))
            {
                if (context.Get("NoExternal") != "1" && (member = Session["Member"] as Member) != null)
                {
                    if (!string.IsNullOrWhiteSpace(member.Email))
                    {
                        // With External account registration

                        var link = MemberLink.Provider.Get(member.EvalExternalId);
                        WebUser user = null;

                        // Make sure records are clean
                        if (link != null && ((user = link.User) == null || link.Approved != MemberAccountStatus.Approved)) // || !user.IsActive))
                        {
                            link.Delete();
                            link = null;
                        }

                        if (link == null)
                        {
                            // Insert to Member Table in CMS
                            try
                            {
                                var paramSet = GetRegistrationParameterSet(context);

                                user = SetupWebUser(member, true);
                                if (user != null)
                                {
                                    // Successful registration
                                    SetupMemberLink(member, membershipDate, user);
                                    SendAccountCreatedAlert(paramSet, member, membershipDate, user, true);

                                    hRedirect.Value = "1";

                                    lblMsg1.Text = "Registration Successful!<br/><br/>Your Temporary Password has been sent to your email and mobile phone.";
                                    mpeMsg1.Show();
                                }
                                else
                                {
                                    // UserName already exits
                                    lblMsg1.Text = "The specified Username already exists, please specify a different one.";
                                    mpeMsg1.Show();
                                }
                            }
                            catch (Exception ex)
                            {
                                // Unknown error
                                lblMsg1.Text = "Registration failed. Please contact the ADDCIT Team or the Locale Secretary.<br/>" + ex;
                                mpeMsg1.Show();
                            }
                        }
                        else
                        {
                            // Group ID already in use
                            lblMsg1.Text = "The Group ID you entered is already in use.<br/>Please use the Forgot Password feature to reset your password. <br/>Please contact the ADDCIT team or the Locale Secretary for any issues.";
                            mpeMsg1.Show();
                        }
                    }
                    else
                    {
                        // Email is empty
                        lblMsg1.Text = "Registration cannot continue without a valid email. <br/>Please contact the ADDCIT team or the Locale Secretary.";
                        mpeMsg1.Show();
                    }
                }
                else if (context.Get("NoExternal") == "1")
                {
                    // Non-External registration
                    try
                    {
                        string externalIdNo = txtExternalIDNo.Text.Trim();
                        MemberLink link = string.IsNullOrEmpty(externalIdNo) ? null : MemberLink.Provider.Get(externalIdNo);
                        WebUser user = null;

                        // Make sure records are clean
                        if (link != null && ((user = link.User) == null || link.Approved != MemberAccountStatus.Approved)) // || !user.IsActive))
                        {
                            link.Delete();
                            link = null;
                        }

                        if (link == null)
                        {
                            bool searchExternal = WebRegistry.SelectNode("/System/Integration-External/Search-External").ValueBool;
                            if (searchExternal && !string.IsNullOrEmpty(externalIdNo) && (member = GetMemberProfile(externalIdNo, membershipDate)) != null)
                            {
                                lblMsg1.Text = "An existing External account matching your Group ID is found, <br/>please use the Registration for registered External users.";
                                mpeMsg1.Show();
                            }
                            else
                            {
                                var email = txtEmail.Text.Trim();
                                if (!string.IsNullOrEmpty(email) && Validator.IsRegexMatch(email, RegexPresets.Email))
                                {
                                    string noPhotoUrl = WebUtil.CombineAddress(WConfig.BaseAddress, WebRegistry.SelectNodeValue(MemberConstants.NoPhotoPathKey));

                                    member = new Member();
                                    member.MembershipDate = DataUtil.GetDateTime(txtMembershipDate.Text.Trim());
                                    member.ExternalIDNo = txtExternalIDNo.Text.Trim();
                                    member.FirstName = txtFirstName.Text.Trim();
                                    member.MiddleName = txtMiddleName.Text.Trim();
                                    member.LastName = txtLastName.Text.Trim();
                                    member.Mobile = txtMobileNumber.Text.Trim();
                                    member.Phone = member.Mobile;
                                    member.Email = email;
                                    member.PhotoPath = noPhotoUrl;
                                    member.NickName = "";

                                    var paramSet = GetRegistrationParameterSet(context);

                                    user = SetupWebUser(member, false);
                                    if (user != null)
                                    {
                                        // Create MemberLink and send verification e-mail
                                        SetupMemberLink(member, membershipDate, user);
                                        SendAccountCreatedAlert(paramSet, member, membershipDate, user, false);

                                        // Send confirmation link to Admin/Secretary/Manager
                                        SendApprovalAlertToAdmin(paramSet, member, membershipDate, user);

                                        hRedirect.Value = "1";

                                        lblMsg1.Text = "Registration Successful!<br/><br/>Your Temporary Password has been sent to your email and mobile phone. <br/>However, your new account is currently PENDING APPROVAL by the Locale Secretary. <br/>Please wait for an SMS or email confirmation that your account has been approved <br/>before you can log-in using your Temporary Password.";
                                        mpeMsg1.Show();
                                    }
                                    else
                                    {
                                        // UserName taken

                                        lblMsg1.Text = "The specified Username already exists, please enter a different one. <br/>You may also try the Forgot Password feature to reset your password <br/>if you already have an existing account.";
                                        mpeMsg1.Show();
                                    }
                                }
                                else
                                {
                                    // Invalid email
                                    lblMsg1.Text = "Registration cannot continue without a valid email address. <br/>Please contact the ADDCIT team or the Locale Secretary for any issues.";
                                    mpeMsg1.Show();
                                }
                            }
                        }
                        else
                        {
                            // Group ID already in use
                            lblMsg1.Text = "The Group ID is already in use. Please use the Forgot Password feature to retrieve your password. <br/>Please contact the ADDCIT team or the Locale Secretary for any issues.";
                            mpeMsg1.Show();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Unkown error
                        lblMsg1.Text = string.Format("Registration failed. <br/>Error: {0}.<br/>", ex);
                        mpeMsg1.Show();
                    }
                }
                else
                {
                    // Unknown registration mode

                    lblMsg1.Text = "Registration failed. <br/>Could not determine registration mode. <br/>Please contact the ADDCIT team.<br/>";
                    mpeMsg1.Show();
                }
            }
            else
            {
                // Invalid Membership Date

                lblMsg1.Text = "Registration failed. Invalid Date of Membership.<br/>";
                mpeMsg1.Show();
            }
        }

        private Member GetMemberProfile(string externalIdNo, DateTime membershipDate)
        {
            try
            {
                MemberSoapClient memWS = new MemberSoapClient(false);
                return memWS.GetProfile(externalIdNo, membershipDate);
            }
            catch (Exception) { }

            return null;
        }

        private WebParameterSet GetRegistrationParameterSet(WContext context)
        {
            var paramSetName = context.Element.GetParameterValue("Registration-ParameterSet");
            WebParameterSet paramSet = !string.IsNullOrEmpty(paramSetName) ? WebParameterSet.Get(paramSetName) : null;

            return paramSet;
        }

        private bool SendApprovalAlertToAdmin(WebParameterSet paramSet, Member member, DateTime membershipDate, WebUser user)
        {
            string emailTemplate = paramSet.GetParameterValue("NewRegistrationAlertToAdmin-Email");
            string smsTemplate = paramSet.GetParameterValue("NewRegistrationAlertToAdmin-SMS");
            var accountReviwers = paramSet.GetParameterValue("AccountReviewer");

            var loginUrl = paramSet.GetParameterValue("LoginUrl");
            if (loginUrl.StartsWith("/"))
                loginUrl = WebUtil.CombineAddress(WConfig.BaseAddress, loginUrl);

            NamedValueProvider provider = new NamedValueProvider();
            provider.Add("CHURCH_ID_NO", member.EvalExternalId);
            provider.Add("MEMBERSHIP_DATE", membershipDate.ToString("dd-MMM-yyyy"));
            provider.Add("PHOTO_URL", member.GetPhotoPath("200x200"));
            provider.Add("FIRST_NAME", user.FirstName);
            provider.Add("LAST_NAME", user.LastName);
            provider.Add("EMAIL", user.Email);
            provider.Add("USER_NAME", user.UserName);
            provider.Add("MOBILE", user.MobileNumber);
            provider.Add("HOME_ADDRESS", txtAddress.Text.Trim());
            provider.Add("LOCALE", txtLocale.Text.Trim());
            provider.Add("URL", loginUrl);

            var emailContent = Substituter.Substitute(emailTemplate, provider);
            var emailSubject = WebMailMessage.PrefixSubject("New Account Approval Request");
            var smsContent = Substituter.Substitute(smsTemplate, provider);
            int sendVia = DataUtil.GetInt32(paramSet.GetParameterValue("Send-Via", "2")); // Default is Both

            var recipients = AccountHelper.CollectEmailString(accountReviwers);
            recipients.AddRange(AccountHelper.CollectMobileNumbers(accountReviwers));

            var to = DataUtil.ToDelimitedString(recipients, ';');

            WebMessageQueue msg = WebMessageQueue.Create(emailContent, smsContent, sendVia, to, emailSubject, null);
            msg.Update();

            try
            {
                AgentHelper.ExecuteTask(MessageProcessorTask.TASK_NAME);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);

                return false;
            }

            return true;
        }

        private string SendAccountCreatedAlert(WebParameterSet paramSet, Member member, DateTime membershipDate, WebUser user, bool withExternal)
        {
            string emailTemplate = paramSet.GetParameterValue(withExternal ? "AccountCreatedAlert-Email" : "AccountCreatedNonExternalAlert-Email");
            string smsTemplate = paramSet.GetParameterValue(withExternal ? "AccountCreatedAlert-SMS" : "AccountCreatedNonExternalAlert-SMS");

            var loginUrl = paramSet.GetParameterValue("LoginUrl");
            if (loginUrl.StartsWith("/"))
                loginUrl = WebUtil.CombineAddress(WConfig.BaseAddress, loginUrl);

            NamedValueProvider provider = new NamedValueProvider();
            provider.Add("CHURCH_ID_NO", member.ExternalIDNo);
            provider.Add("TEMPORARY_ID_NO", member.TemporaryIDNo);
            provider.Add("MEMBERSHIP_DATE", membershipDate.ToString("dd-MMM-yyyy"));
            provider.Add("PHOTO_URL", member.GetPhotoPath("200x200"));
            provider.Add("FIRST_NAME", user.FirstName);
            provider.Add("LAST_NAME", user.LastName);
            provider.Add("MOBILE", user.MobileNumber);
            provider.Add("EMAIL", user.Email);
            provider.Add("USER_NAME", user.UserName);
            provider.Add("PASSWORD", user.Password);
            provider.Add("MEMBER_NAME", AccountHelper.GetPrefixedName(user, true));
            provider.Add("URL", loginUrl);

            var emailContent = Substituter.Substitute(emailTemplate, provider);
            var emailSubject = WebMailMessage.PrefixSubject(withExternal ? "Your New Account" : "Your New Account Is Pending Approval");
            var smsMessage = Substituter.Substitute(smsTemplate, provider);

            var sendVia = DataUtil.GetInt32(paramSet.GetParameterValue("Send-Via", "2")); // Default is Both
            var to = string.Format("{0};{1}", user.Email, user.MobileNumber);

            WebMessageQueue.Create(emailContent, smsMessage, sendVia, to, emailSubject, null).Update();

            try
            {
                AgentHelper.ExecuteTask(MessageProcessorTask.TASK_NAME);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return "There was an error sending your Temporary Password. However, <br/>it was placed on queue by the messaging server and will be sent on the next schedule.";
            }

            return null;
        }

        private MemberLink SetupMemberLink(Member member, DateTime membershipDate, WebUser user)
        {
            MemberLink link = null;

            if (member != null && member.MemberID > 0)
                link = MemberLink.Provider.GetByMemberId((int)member.MemberID);

            if (link == null)
                link = new MemberLink();

            link.Approved = MemberAccountStatus.Approved;
            link.UserId = user.Id;
            link.MemberId = (int)member.MemberID;
            link.ExternalIdNo = member.EvalExternalId;
            link.Nickname = member.NickName;
            link.PhotoPath = member.PhotoPath;
            link.MembershipDate = membershipDate;
            link.LastUpdate = DateTime.Now;
            link.Locale = txtLocale.Text.Trim();

            //memLink.HomeAddressLine1 = txtAddress.Text.Trim();
            //memLink.MobileNumber = mem.Mobile;
            //memLink.HomePhone = mem.Phone;

            // Already covered by SetupWebUser
            //user.MobileNumber = mem.Mobile;
            //user.TelephoneNumber = mem.Phone;
            //user.Update();

            if (link.MemberId > 0)
            {
                var client = MemberSoapClient.GetNewClientInstance();
                link.TryPopulateGroupsFromExt(client);
                link.TryPopulateHomeAddressFromExt(client);
                link.TryPopulateProfileFromExt(client);
                link.TryStatusFromExt(client);
            }

            link.Update();
            return link;
        }

        private WebUser SetupWebUser(Member member, bool activate)
        {
            //var userName = txtUserName.Text.Trim();
            var user = WebUser.Get(member.EvalExternalId);
            if (user == null)
            {
                var otpCode = new OtpCodeGenerator(-1);
                user = new WebUser();
                user.UserName = member.EvalExternalId; // txtUserName.Text.Trim();
                user.Password = otpCode.OtpCode;
                user.FirstName = member.FirstName;
                user.MiddleName = member.MiddleName;
                user.LastName = member.LastName;
                user.Email = member.Email; //txtEmail.Text.Trim();
                user.MobileNumber = member.Mobile; //txtMobileNumber.Text.Trim();
                user.TelephoneNumber = string.IsNullOrEmpty(member.Phone) ? member.Mobile : member.Phone;
                user.Gender = string.IsNullOrEmpty(member.Gender) ? GenderTypes.Unspecified : Convert.ToChar(member.Gender);
                user.IsActive = activate;
                user.PasswordExpiryDate = WConstants.ExpiredPasswordDate;
                if (!string.IsNullOrEmpty(member.PhotoPath))
                    user.PhotoPath = member.PhotoPath;
                user.Update();

                // Add appropriate GROUPS to user
                if (activate)
                {
                    string groups = WebRegistry.SelectNodeValue(MemberConstants.GroupsToAddPath);
                    user.AddToGroups(groups);
                }

                // Setup HOME address
                var address = txtAddress.Text.Trim();
                var homeAddress = user.GetAddress(AddressTags.Home);
                if (homeAddress != null)
                {
                    if (!string.IsNullOrEmpty(homeAddress.AddressLine1))
                        homeAddress.AddressLine1 = address;
                }
                else
                {
                    homeAddress = user.NewAddress(AddressTags.Home);
                    homeAddress.AddressLine1 = address;
                }

                homeAddress.Update();

                return user;
            }
            else
            {
                // Account already exists
            }

            return null;
        }

        protected void cmdMsgOK_Click(object sender, EventArgs e)
        {
            if (hRedirect.Value == "1")
            {
                var context = new WContext(this);
                string loginRedirect = context.Element.GetParameterValue(MemberConstants.LoginUrlKey);

                if (!string.IsNullOrEmpty(loginRedirect))
                    context.Redirect(loginRedirect);
                else
                    context.Redirect();
            }
        }
    }
}