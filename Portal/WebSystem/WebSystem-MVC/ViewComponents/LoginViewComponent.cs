using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Security;
using WCMS.Framework.Utilities;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Login / authentication View Component. Replaces Login.ascx (Common).
    /// Handles login form, forgot password, OTP verification, and logout.
    /// Usage: @await Component.InvokeAsync("Login")
    /// </summary>
    public class LoginViewComponent : WViewComponent
    {
        public LoginViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke()
        {
            var element = WcmsContext.Element;
            var mode = WcmsContext.Get(AccountConstants.ModeKey);

            var model = new LoginViewModel();

            if (mode == AccountConstants.ModeLogOff)
            {
                // Log-Off handled by middleware / API controller
                model.View = "LoggedOut";
                model.RedirectUrl = "/";
            }
            else if (mode == AccountConstants.ModeForgot)
            {
                model.View = "ForgotPassword";
                model.LoginUrl = element?.GetParameterValue(AccountConstants.LoginUrl);
                model.ForgotPasswordUrl = element?.GetParameterValue(AccountConstants.ForgotPasswordUrl);
            }
            else if (mode == AccountConstants.OtpVerify)
            {
                model.View = "OtpVerification";
            }
            else
            {
                // Regular login form
                model.View = "LoginForm";
                model.EnableRememberMe = DataUtil.GetBool(
                    element?.GetParameterValue(AccountConstants.EnableRememberMe), true);
                model.EnableOtp = DataUtil.GetBool(
                    element?.GetParameterValue("EnableOTP"), false);
                model.EnableSms = DataUtil.GetBool(
                    element?.GetParameterValue("EnableSMS"), true);
                model.SignUpUrl = element?.GetParameterValue(AccountConstants.SignUpUrl, null);
                model.ForgotPasswordUrl = element?.GetParameterValue(AccountConstants.ForgotPasswordUrl);
                model.RememberMeText = element?.GetParameterValue(AccountConstants.RememberMeText);
                model.PreLoginNote = ParameterizedWebObject.GetValue("PreLoginNote", element);
                model.IsLoggedIn = WcmsSession.IsLoggedIn;

                if (WcmsSession.IsLoggedIn)
                {
                    model.UserDisplayName = WcmsSession.User?.FullName;
                }
            }

            return View(model);
        }
    }

    public class LoginViewModel
    {
        public string View { get; set; }
        public bool IsLoggedIn { get; set; }
        public string UserDisplayName { get; set; }
        public bool EnableRememberMe { get; set; }
        public bool EnableOtp { get; set; }
        public bool EnableSms { get; set; }
        public string SignUpUrl { get; set; }
        public string ForgotPasswordUrl { get; set; }
        public string LoginUrl { get; set; }
        public string RememberMeText { get; set; }
        public string PreLoginNote { get; set; }
        public string RedirectUrl { get; set; }
        public string ErrorMessage { get; set; }
    }
}
