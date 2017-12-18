using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

using WCMS.Framework.Utilities;
using WCMS.Framework;
using WCMS.Framework.Security;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Common
{
    public partial class LoginController : System.Web.UI.UserControl
    {
        /* 
         * PARAMETERS:
         * - ForgotPasswordUrl
         * - FirstLoginUrl
         * - SignOutRedirect
         * - LoginUrl
         * - SignUpUrl
         * - UpdatePasswordUrl
         * - EnableRememberMe
         * - RememberMeText
         * - EnableOTP
         * - EnableSMS
         * - MembershipCheck
         * - UpdatePasswordUrl
         * - EnableExternalAccounts
         * - LoginHomeUrl
         * - PreLoginNote
         */

        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
            lblAlert.Visible = false;

            if (!IsPostBack)
            {
                var context = new WContext(this);
                var element = context.Element;
                var forgotPasswordUrl = element.GetParameterValue(AccountConstants.ForgotPasswordUrl);
                if (linkForgot != null)
                {
                    if (!string.IsNullOrEmpty(forgotPasswordUrl))
                        linkForgot.HRef = forgotPasswordUrl;
                }

                string mode = context.Get(AccountConstants.ModeKey);
                if (mode == AccountConstants.ModeLogOff)
                {
                    string sessionId = context.Get(WConstants.SessionId);
                    if (!string.IsNullOrEmpty(sessionId))
                    {
                        var session = WSession.UserSessions.Sessions.FirstOrDefault(i => i.SessionId.ToString()
                                .Equals(sessionId, StringComparison.InvariantCultureIgnoreCase));
                        if (session != null && session.UserId > 0) // Make sure user has a valid active session
                        {
                            // Removing all checks for now since this will create log off issue when there are multiple sessions, 
                            // in different browsers or devices.

                            //var request = Context.Request;
                            //if (/*request.UserAgent == session.UserAgent &&*/ request.UserHostAddress == session.IPAddress)
                            //{
                            WSession.LogOff(session.UserId);
                            //}
                        }
                    }

                    // Log-Off
                    WSession.LogOff();
                    // WSession.ClearLoginCookie(Context);

                    var signOutRedirect = Request[WQuery.SourceKey];
                    if (!string.IsNullOrEmpty(signOutRedirect))
                    {
                        WQuery.StaticRedirect(signOutRedirect);
                    }
                    else
                    {
                        signOutRedirect = element.GetParameterValue(AccountConstants.SignOutRedirect, null);
                        if (!string.IsNullOrEmpty(signOutRedirect))
                            WQuery.StaticRedirect(signOutRedirect, false);
                        else if (!string.IsNullOrEmpty(signOutRedirect = context.Get(AccountConstants.SignOutRedirect)))
                            WQuery.StaticRedirect(signOutRedirect, false);
                        else
                            WQuery.StaticBaseRedirect("/");
                    }
                }
                else if (mode == AccountConstants.ModeForgot)
                {
                    // Forgot Password
                    var loginUrl = element.GetParameterValue(AccountConstants.LoginUrl);
                    if (!string.IsNullOrEmpty(loginUrl))
                    {
                        var query = context.Query.Clone();
                        query.BasePath = loginUrl;
                        linkLogin.HRef = query.BuildQuery();
                        linkLogin2.HRef = query.BuildQuery();
                        linkLogin3.HRef = query.BuildQuery();
                    }

                    if (!string.IsNullOrEmpty(forgotPasswordUrl))
                        context.BasePath = forgotPasswordUrl;
                    else
                        context.Set(AccountConstants.ModeKey, AccountConstants.ModeForgot);

                    linkForgot.HRef = context.BuildQuery();
                    MultiView1.SetActiveView(viewForgot);
                }
                else if (mode == AccountConstants.ModeActivate)
                {
                    // Activate Account
                    string sUsername = context.Get("User");
                    string sGuid = context.Get("ConfirmCode");

                    MultiView1.SetActiveView(viewActivation);
                }
                else if (mode == AccountConstants.OtpVerify)
                {
                    // Verify OTP Code
                    var otpCache = Session[AccountConstants.OtpCacheKey] as OtpCache;
                    if (otpCache != null)
                    {
                        lblOtpType.InnerHtml = otpCache.OtpType == 0 ? "e-mail" : "mobile";
                        lblOtpExpiry.InnerHtml = DateTimeHelper.TimeIntervalToString(OtpCodeGenerator.GetExpiry(), TimeInterval.Minute);

                        if (otpCache.SentToEmailInstead)
                            lblOtpMsg.Text = "There is no valid mobile number defined in your profile. The OTP PIN was sent to your e-mail instead.";

                        MultiView1.SetActiveView(viewOtpVerification);
                    }
                    else
                    {
                        // Invalid Session Info
                        context.Remove(AccountConstants.ModeKey);
                        context.Redirect();
                    }
                }
                else
                {
                    // Regular Login View

                    var allowRememberLogin = DataHelper.GetBool(element.GetParameterValue(AccountConstants.EnableRememberMe), true);
                    WebUser user = null;
                    if (!WSession.Current.IsLoggedIn)
                    {
                        user = WSession.CheckLoginCookie(Context);
                        if (user != null)
                        {
                            if (allowRememberLogin)
                                WSession.Current.Login(user.Id, true);
                            else
                                WSession.ClearLoginCookie(Context);
                        }
                    }
                    else
                    {
                        user = WSession.Current.User;
                    }

                    //var redirUrl = WHelper.GetRedirectUrl(user, context, element);
                    //if (!string.IsNullOrEmpty(redirUrl))
                    if (user != null)
                    {
                        // LoginHomeUrl User-Level
                        var redirUrl = WHelper.GetRedirectUrl(user, context, element);
                        WQuery.StaticRedirect(redirUrl, false);
                    }
                    else
                    {
                        panelRememberMe.Visible = allowRememberLogin;

                        var enableOtp = DataHelper.GetBool(element.GetParameterValue("EnableOTP"), false);
                        panelOtp.Visible = enableOtp;
                        if (enableOtp)
                            rblOtp.Items[1].Enabled = DataHelper.GetBool(element.GetParameterValue("EnableSMS"), true);

                        // Configure the Sign-Up Url
                        var signUpUrl = element.GetParameterValue(AccountConstants.SignUpUrl, null);
                        if (!string.IsNullOrEmpty(signUpUrl))
                        {
                            linkSignup.HRef = signUpUrl; // Set Sign Up URL
                            panelSignUpLink.Visible = true;
                        }

                        var rememberMeText = element.GetParameterValue(AccountConstants.RememberMeText);
                        if (allowRememberLogin && !string.IsNullOrEmpty(rememberMeText))
                            chkRememberMe.Text = rememberMeText;

                        var preLoginNote = ParameterizedWebObject.GetValue("PreLoginNote", element);
                        if (!string.IsNullOrEmpty(preLoginNote))
                            DisplayAlertMessage(preLoginNote);

                        // Set Forgot Password URL
                        if (!string.IsNullOrEmpty(forgotPasswordUrl))
                            context.BasePath = forgotPasswordUrl;
                        else
                            context.Set(AccountConstants.ModeKey, AccountConstants.ModeForgot);

                        var username = context.Get("Username");
                        if (!string.IsNullOrEmpty(username))
                            txtUserName.Text = username;

                        if (linkForgot != null)
                            linkForgot.HRef = context.BuildQuery();
                        MultiView1.SetActiveView(viewLogin);
                    }

                    //LoginSecurity.GetPublicKey();
                }
            }
        }

        //private int GetLockoutDuration()
        //{
        //    return 0;
        //}

        protected void cmdLogin_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            var element = context.Element;
            var userName = txtUserName.Text.Trim();
            var password = txtPassword.Text;

            if (!Request.IsSecureConnection)
            {
                string encUserName = hUserName.Value;
                string encPassword = hPassword.Value;

                if (!string.IsNullOrEmpty(encUserName) && !string.IsNullOrEmpty(encPassword))
                {
                    var encLoginInfo = LoginSecurity.DecodeLogin(encUserName, encPassword);
                    if (encLoginInfo.Length == 2)
                    {
                        userName = encLoginInfo.First();
                        password = encLoginInfo[1];
                    }
                }
                else
                {
                    var encRequired = DataHelper.GetBool(element.GetParameterValue("EncryptionRequired"), true);
                    if (encRequired)
                    {
                        DisplayErrorMessage("Invalid input data.");
                        return;
                    }
                }
            }

            WebUser user = null;

            #region Local Methods

            Action LoginFailed = () =>
            {
                var getAccessUrl = element.GetParameterValue("GetAccessUrl");
                if (!string.IsNullOrEmpty(getAccessUrl))
                {
                    var q = new WQuery(getAccessUrl);
                    q.Set("Username", userName);
                    getAccessUrl = q.BuildQuery();
                }

                if (user != null && user.ProviderId == WConstants.NULL_ID)
                {
                    if (user.IsLoginLockedOut)
                    {
                        var waitTime = user.LastLoginFailureDate.AddMinutes(WebUser.LOGIN_LOCKOUT_MINS + 1);
                        waitTime.AddSeconds(waitTime.Second * -1);
                        DisplayErrorMessage(string.Format("You've reached the maximum invalid login attempts. Please try again after {0}.", waitTime.ToShortTimeString()));
                    }
                    else if (!string.IsNullOrEmpty(getAccessUrl))
                    {
                        DisplayErrorMessage("Your account is inactive. Would you like to recover your account?&nbsp;<a href='" + getAccessUrl + "' class='btn btn-danger btn-xs'>Recover</a>");
                    }
                    else
                    {
                        DisplayErrorMessage("Your account is inactive, please contact support.");
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(getAccessUrl))
                    {
                        DisplayErrorMessage("Incorrect username or password. Would you like to recover your account?&nbsp;<a href='" + getAccessUrl + "' class='btn btn-danger btn-xs'>Recover</a>");
                    }
                    else
                    {
                        DisplayErrorMessage("Incorrect username or password.");
                    }
                }
            };

            Action LoginSuccessSetSession = () =>
            {
                var membershipCheck = element.GetParameterValue("MembershipCheck");
                if (!string.IsNullOrEmpty(membershipCheck))
                {
                    if (!AccountHelper.IsPresentOrMember(user.Id, membershipCheck))
                    {
                        var minMembershipGroupName = element.GetParameterValue("MinMembershipGroup");
                        WebGroup minGroup = null;
                        if (!string.IsNullOrEmpty(minMembershipGroupName) && (minGroup = WebGroup.SelectNode(minMembershipGroupName)) != null && user.IsMemberOf(minGroup.Id))
                        {
                            user.AddToGroups(membershipCheck);
                        }
                        else
                        {
                            DisplayErrorMessage("Your account is not permitted.");
                            return;
                        }
                    }
                }

                string impersonateUserName = context.Get("Impersonate");
                bool impersonate = false;
                if (!string.IsNullOrEmpty(impersonateUserName) && user.IsAdministrator())
                {
                    var impersonateUser = WebUser.GetByEmailOrUsername(impersonateUserName);
                    if (impersonateUser != null)
                    {
                        user = impersonateUser;
                        impersonate = true;
                    }
                }


                // Determine where to redirect

                var updatePwdUrl = element.GetParameterValue(AccountConstants.UpdatePasswordUrl);
                var rememberLogin = DataHelper.GetBool(element.GetParameterValue(AccountConstants.EnableRememberMe), true) && chkRememberMe.Checked;

                // Check and setup OTP if enabled
                if (panelOtp.Visible)
                {
                    var redirUrl = WHelper.GetRedirectUrl(user, context, element);
                    var otpSender = new OtpCodeGenerator(user.Id);
                    var otpCache = new OtpCache(user.Id, redirUrl, otpSender.OtpCode, rblOtp.SelectedIndex, updatePwdUrl);
                    if (otpCache.OtpType == 0)
                    {
                        otpSender.SendToEmail();
                    }
                    else if (!string.IsNullOrEmpty(user.MobileNumber))
                    {
                        otpSender.SendToMobile();
                    }
                    else
                    {
                        otpCache.SentToEmailInstead = true;
                        otpSender.SendToEmail();
                    }

                    otpCache.RememberLogin = rememberLogin;
                    otpCache.Impersonate = impersonate;
                    otpCache.Password = password;

                    Session[AccountConstants.OtpCacheKey] = otpCache;

                    context.Set(AccountConstants.ModeKey, AccountConstants.OtpVerify);
                    context.Redirect();
                    //MultiView1.SetActiveView(viewOtpVerification);
                }
                else
                {
                    var browser = WSession.Current.Login(user, !impersonate);
                    browser.IPLocation = hLocation.Value.Trim();

                    if (rememberLogin && !impersonate)
                        WSession.RememberLogin(user, password, Context);

                    var redirUrl = WHelper.GetRedirectUrl(user, context, element);
                    if (user.IsPasswordExpired && !string.IsNullOrEmpty(updatePwdUrl))
                    {
                        var query = new WQuery(updatePwdUrl);
                        query.Set(WQuery.SourceKey, redirUrl);
                        query.Redirect();
                    }
                    else
                    {
                        WQuery.StaticBaseRedirect(redirUrl);
                    }
                }
            };

            #endregion

            bool enableExternalAccounts = DataHelper.GetBool(element.GetParameterValue(AccountConstants.EnableExternalAccounts), false);
            IUserProvider provider = null;
            if (enableExternalAccounts)
            {
                var userProvider = UserProvider.Provider.Get(AccountConstants.DefaultExternalProvider);
                provider = userProvider.ResolveProvider();
                if (provider != null)
                    provider.Context = context;
            }

            // ----- Check Lockout

            user = AccountHelper.FindUser(userName);
            if (user != null)
            {
                if (user.IsActive)
                {
                    // Check user's account provider
                    if (user.IsLoginLockedOut)
                    {
                        // LoginFailed will be invoked in below code.
                    }
                    else if (user.ProviderId == WConstants.NULL_ID)
                    {
                        user = AccountHelper.ValidateLogin(user, password);
                        if (user != null)
                        {
                            // Native Login
                            LoginSuccessSetSession();
                            return;
                        }
                    }
                    else if (enableExternalAccounts && user.ProviderId == AccountConstants.DefaultExternalProvider)
                    {
                        // using ONE or other account providers
                        var loginResult = provider.LoginCheck(user, password);
                        if (loginResult.StatusCode == LoginCodes.Success)
                        {
                            var hash = SecurityHelper.CreatePasswordHash(password, SecurityHelper.SALT);
                            if (!user.Password.Equals(hash))
                            {
                                // Sync Pwd
                                user.Password = hash;
                                user.Update();
                            }

                            /*
                            if (user.Password != password)
                            {
                                // Sync Pwd
                                user.Password = password;
                                user.Update();
                            }*/

                            LoginSuccessSetSession();
                            return;
                        }
                    }
                }
            }
            else if (enableExternalAccounts)
            {
                var loginResult = provider.LoginCheck(userName, password);
                if (loginResult.StatusCode == LoginCodes.Success)
                {
                    // First time login

                    var loginInfo = new KeyValuePair<string, string>(userName, password);
                    Session[AccountConstants.LoginInfoSessionKey] = loginInfo;

                    var signUpUrl = element.GetParameterValue(AccountConstants.SignUpUrl);
                    WQuery.StaticRedirect(signUpUrl);
                    return;
                }
            }

            LoginFailed();
            return;

            //if (user.IsLockedOut)
            //{
            // Check Lockout duration
            //int iLockoutDuration = this.GetLockoutDuration();
            //if (iLockoutDuration > 0)
            //{
            //    TimeSpan ts = DateTime.Now.Subtract(user.LastLockoutDate);
            //    if (ts.Minutes >= iLockoutDuration)
            //    {
            //        user.UnlockUser();
            //    }
            //}
            //}
        }

        private void DisplayErrorMessage(string message)
        {
            lblError.InnerHtml = message;
            lblError.Visible = true;
        }

        private void DisplayAlertMessage(string message)
        {
            lblAlert.Text = message;
            lblAlert.Visible = true;
        }

        protected void cmdRetrieve_Click(object sender, EventArgs e)
        {
            var userNameOrEmail = txtRetrieveUserName.Text.Trim();
            if (!string.IsNullOrEmpty(userNameOrEmail))
            {
                var user = WebUser.GetByEmailOrUsername(userNameOrEmail);
                if (user != null)
                {
                    var smtp = new WebMailMessage();
                    smtp.Subject = "mPortal - Your Password";
                    smtp.To.Add(string.Format(@"""{0}"" <{1}>", user.FullName, user.Email));
                    smtp.Body = "Your password: " + user.Password;
                    smtp.Send();

                    mvForgotPassword.SetActiveView(viewRetrieveSuccess);
                    lblEmail.InnerHtml = user.Email;
                }
                else
                {
                    lblRetrieveFailed.Text = "The Username or E-mail you provided does not exist.";
                }
            }
        }

        protected void cmdOtpCancel_Click(object sender, EventArgs e)
        {
            Session[AccountConstants.OtpCacheKey] = null;

            var query = new WQuery(this);
            query.Remove(AccountConstants.ModeKey);
            query.Redirect();
        }

        protected void cmdOtpSubmit_Click(object sender, EventArgs e)
        {
            string otpCode = txtOtpCode.Text.Trim();
            if (!string.IsNullOrEmpty(otpCode))
            {
                var otpCache = Session[AccountConstants.OtpCacheKey] as OtpCache;
                if (otpCache != null && otpCache.OtpCode.Equals(otpCode, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (otpCache.DateTime.AddMinutes(OtpCodeGenerator.GetExpiry()) < DateTime.Now)
                    {
                        lblOtpMsg.Text = "Your OTP PIN has expired. Please request a new one.";
                        return;
                    }
                    else
                    {
                        Session[AccountConstants.OtpCacheKey] = null;

                        WSession.Current.Login(otpCache.UserId, !otpCache.Impersonate);
                        var user = WebUser.Get(otpCache.UserId);

                        if (otpCache.RememberLogin && !otpCache.Impersonate)
                            WSession.RememberLogin(user, otpCache.Password, Context);

                        if (user.IsPasswordExpired && !string.IsNullOrEmpty(otpCache.UpdatePwdUrl))
                        {
                            var query = new WQuery(otpCache.UpdatePwdUrl);
                            query.Set(WQuery.SourceKey, otpCache.RedirUrl);
                            query.Redirect();
                        }
                        else
                        {
                            WQuery.StaticBaseRedirect(otpCache.RedirUrl);
                        }

                        return;
                    }
                }
            }

            lblOtpMsg.Text = "Invalid OTP PIN.";
        }

        protected void cmdNewOtpPin_Click(object sender, EventArgs e)
        {
            var otpCache = Session[AccountConstants.OtpCacheKey] as OtpCache;
            if (otpCache != null)
            {
                var otpSender = new OtpCodeGenerator(otpCache.UserId);
                if (otpCache.OtpType == 0)
                    otpSender.SendToEmail();
                else
                    otpSender.SendToMobile();

                otpCache.OtpCode = otpSender.OtpCode;
                otpCache.DateTime = DateTime.Now;
                Session[AccountConstants.OtpCacheKey] = otpCache;

                lblOtpMsg.Text = string.Format("A new OTP PIN has been sent to your {0}.", otpCache.OtpType == 0 ? "e-mail" : "mobile");
            }
        }
    }
}