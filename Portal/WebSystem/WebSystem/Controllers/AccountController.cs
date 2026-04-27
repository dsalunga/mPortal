using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Security;
using WCMS.Framework.Utilities;

namespace WCMS.WebSystem.Controllers
{
    [AllowAnonymous]
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        public const string LoginErrorQueryKey = "LoginError";
        public const string LoginInfoQueryKey = "LoginInfo";

        [HttpGet("/login")]
        public IActionResult LoginPage([FromQuery] string returnUrl = null)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }

            return LocalRedirect("/");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginFormRequest request, [FromQuery] string returnUrl = null)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.UserName) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return RedirectBackWithMessage(LoginErrorQueryKey, "Please provide both username and password.");
            }

            WebUser user = null;
            try
            {
                user = AccountHelper.ValidateLogin(request.UserName, request.Password);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Account login validation failed for user '{UserName}'.", request.UserName);
                return RedirectBackWithMessage(LoginErrorQueryKey, "Authentication service is currently unavailable.");
            }

            if (user == null || !user.IsActive)
            {
                return RedirectBackWithMessage(LoginErrorQueryKey, "Invalid username or password.");
            }

            WSession.Current.Login(user.Id, logSession: true);

            if (request.RememberMe)
            {
                try
                {
                    WSession.RememberLogin(user, request.Password, HttpContext);
                }
                catch
                {
                    // Remember-me cookie fallback is non-fatal.
                }
            }

            await SignInCookieAsync(user, request.RememberMe);

            var redirectTo = ResolvePostLoginRedirect(returnUrl, request?.RequestUrl, Request.Headers.Referer.ToString());
            if (!string.IsNullOrWhiteSpace(redirectTo))
            {
                return LocalRedirect(redirectTo);
            }

            var redirectUrl = BuildRedirectTarget(Request.Headers.Referer.ToString(), clearMessageKeys: true);
            return LocalRedirect(redirectUrl);
        }

        [HttpPost("forgotpassword")]
        public IActionResult ForgotPassword([FromForm] ForgotPasswordFormRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.UserNameOrEmail))
            {
                return RedirectBackWithMessage(
                    LoginErrorQueryKey,
                    "Please enter your username or email.",
                    mode: AccountConstants.ModeForgot);
            }

            // Keep message generic to avoid account enumeration.
            try
            {
                _ = WebUser.GetByEmailOrUsername(request.UserNameOrEmail);
            }
            catch
            {
                return RedirectBackWithMessage(
                    LoginErrorQueryKey,
                    "Password recovery service is currently unavailable.",
                    mode: AccountConstants.ModeForgot);
            }

            return RedirectBackWithMessage(
                LoginInfoQueryKey,
                "If the account exists, password recovery instructions were sent.",
                mode: AccountConstants.ModeForgot);
        }

        [HttpPost("verifyotp")]
        public IActionResult VerifyOtp([FromForm] VerifyOtpFormRequest request)
        {
            if (request != null &&
                request.Action != null &&
                request.Action.Equals("cancel", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectBackWithMessage(LoginInfoQueryKey, "OTP verification cancelled.", mode: null);
            }

            return RedirectBackWithMessage(
                LoginInfoQueryKey,
                "OTP verification is not active for this login flow.",
                mode: AccountConstants.OtpVerify);
        }

        [HttpGet("/logout")]
        [HttpPost("/logout")]
        public async Task<IActionResult> Logout([FromQuery] string returnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            WSession.LogOff();

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }

            return LocalRedirect("/");
        }

        private async Task SignInCookieAsync(WebUser user, bool isPersistent)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(CultureInfo.InvariantCulture)),
                new Claim(ClaimTypes.Name, string.IsNullOrWhiteSpace(user.UserName) ? $"user-{user.Id}" : user.UserName)
            };

            if (user.IsAdministrator())
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = isPersistent,
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
        }

        private IActionResult RedirectBackWithMessage(string key, string message, string mode = null)
        {
            var updates = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                [key] = message
            };

            if (!string.IsNullOrWhiteSpace(mode))
            {
                updates[AccountConstants.ModeKey] = mode;
            }
            else
            {
                updates[AccountConstants.ModeKey] = null;
            }

            var redirectUrl = BuildRedirectTarget(Request.Headers.Referer.ToString(), clearMessageKeys: false, updates: updates);
            return LocalRedirect(redirectUrl);
        }

        private string BuildRedirectTarget(
            string referer,
            bool clearMessageKeys,
            IDictionary<string, string> updates = null)
        {
            string localPath = "/";
            var queryValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (!string.IsNullOrWhiteSpace(referer))
            {
                Uri parsedUri;
                if (Uri.TryCreate(referer, UriKind.Absolute, out parsedUri))
                {
                    localPath = parsedUri.AbsolutePath;
                    foreach (var pair in QueryHelpers.ParseQuery(parsedUri.Query))
                    {
                        queryValues[pair.Key] = pair.Value.FirstOrDefault();
                    }
                }
                else if (Url.IsLocalUrl(referer))
                {
                    var localUri = new Uri("http://localhost" + referer);
                    localPath = localUri.AbsolutePath;
                    foreach (var pair in QueryHelpers.ParseQuery(localUri.Query))
                    {
                        queryValues[pair.Key] = pair.Value.FirstOrDefault();
                    }
                }
            }

            if (!Url.IsLocalUrl(localPath))
            {
                localPath = "/";
                queryValues.Clear();
            }

            if (clearMessageKeys)
            {
                queryValues.Remove(LoginErrorQueryKey);
                queryValues.Remove(LoginInfoQueryKey);
            }

            if (updates != null)
            {
                foreach (var pair in updates)
                {
                    if (string.IsNullOrWhiteSpace(pair.Value))
                    {
                        queryValues.Remove(pair.Key);
                    }
                    else
                    {
                        queryValues[pair.Key] = pair.Value;
                    }
                }
            }

            return QueryHelpers.AddQueryString(localPath, queryValues);
        }

        private string ResolvePostLoginRedirect(string returnUrl, string requestUrl, string referer)
        {
            var candidates = new[]
            {
                NormalizeLocalUrl(returnUrl),
                NormalizeLocalUrl(requestUrl),
                ResolveRequestUrlFromReferer(referer)
            };

            foreach (var candidate in candidates)
            {
                if (!string.IsNullOrWhiteSpace(candidate))
                {
                    return candidate;
                }
            }

            return null;
        }

        private string ResolveRequestUrlFromReferer(string referer)
        {
            if (string.IsNullOrWhiteSpace(referer))
                return null;

            Uri parsedUri;
            if (Uri.TryCreate(referer, UriKind.Absolute, out parsedUri))
            {
                return ResolveRequestUrlFromQuery(parsedUri.Query);
            }

            if (Url.IsLocalUrl(referer))
            {
                var localUri = new Uri("http://localhost" + referer);
                return ResolveRequestUrlFromQuery(localUri.Query);
            }

            return null;
        }

        private string ResolveRequestUrlFromQuery(string queryString)
        {
            var parsed = QueryHelpers.ParseQuery(queryString ?? string.Empty);

            if (parsed.TryGetValue(QueryParser.SourceKey, out StringValues sourceValues))
            {
                var fromSource = NormalizeLocalUrl(sourceValues.FirstOrDefault());
                if (!string.IsNullOrWhiteSpace(fromSource))
                    return fromSource;
            }

            if (parsed.TryGetValue("returnUrl", out StringValues returnValues))
            {
                var fromReturn = NormalizeLocalUrl(returnValues.FirstOrDefault());
                if (!string.IsNullOrWhiteSpace(fromReturn))
                    return fromReturn;
            }

            return null;
        }

        private string NormalizeLocalUrl(string candidate)
        {
            if (string.IsNullOrWhiteSpace(candidate))
                return null;

            var trimmed = candidate.Trim();
            if (Url.IsLocalUrl(trimmed))
                return trimmed;

            string decoded;
            try
            {
                decoded = Uri.UnescapeDataString(trimmed);
            }
            catch
            {
                decoded = trimmed;
            }

            if (Url.IsLocalUrl(decoded))
                return decoded;

            return null;
        }
    }

    public class LoginFormRequest
    {
        [FromForm(Name = "userName")]
        public string UserName { get; set; }

        [FromForm(Name = "password")]
        public string Password { get; set; }

        [FromForm(Name = "rememberMe")]
        public bool RememberMe { get; set; }

        [FromForm(Name = "otpType")]
        public int? OtpType { get; set; }

        [FromForm(Name = "RequestUrl")]
        public string RequestUrl { get; set; }
    }

    public class ForgotPasswordFormRequest
    {
        [FromForm(Name = "userNameOrEmail")]
        public string UserNameOrEmail { get; set; }
    }

    public class VerifyOtpFormRequest
    {
        [FromForm(Name = "otpCode")]
        public string OtpCode { get; set; }

        [FromForm(Name = "action")]
        public string Action { get; set; }
    }
}
