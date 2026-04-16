using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.Utilities;

namespace WCMS.WebSystem.Api
{
    /// <summary>
    /// Legacy-compatible replacement for /Content/Parts/Common/User.svc.
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    public class LegacyUserServiceController : ControllerBase
    {
        [HttpGet("/Content/Parts/Common/User.svc/Check")]
        [HttpPost("/Content/Parts/Common/User.svc/Check")]
        public IActionResult Check(string appKey = null, string userName = null, string password = null)
        {
            userName = ResolveParam("userName", userName);
            password = ResolveParam("password", password);

            var user = AccountHelper.ValidateLogin(userName, password);
            return Ok(user != null && user.IsActive);
        }

        [HttpGet("/Content/Parts/Common/User.svc/GetInfoFromSession")]
        [HttpPost("/Content/Parts/Common/User.svc/GetInfoFromSession")]
        public IActionResult GetInfoFromSession(string appKey = null, string sessionId = null, string ipAddress = null, string userAgent = null)
        {
            sessionId = ResolveParam("sessionId", sessionId);
            ipAddress = ResolveParam("ipAddress", ipAddress);
            userAgent = ResolveParam("userAgent", userAgent);

            if (!string.IsNullOrEmpty(sessionId) && Guid.TryParse(sessionId, out var guid))
            {
                var session = WSession.UserSessions.Sessions.FirstOrDefault(i => i.SessionId.Equals(guid));
                if (session != null)
                {
                    var browser = WSession.UserSessions.BrowserCache.Values
                        .FirstOrDefault(i => i.IPAddress.Equals(ipAddress) && i.UserAgent.Equals(userAgent));
                    if (browser != null)
                        return Ok(new WSUserInfo(WebUser.Get(session.UserId)));
                }
            }

            return Ok((WSUserInfo)null);
        }

        [HttpGet("/Content/Parts/Common/User.svc/GetInfo")]
        [HttpPost("/Content/Parts/Common/User.svc/GetInfo")]
        public IActionResult GetInfo(string appKey = null, string userName = null, string password = null)
        {
            userName = ResolveParam("userName", userName);
            password = ResolveParam("password", password);

            var user = AccountHelper.ValidateLogin(userName, password);
            if (user != null && user.IsActive)
                return Ok(new WSUserInfo(user));

            return Ok((WSUserInfo)null);
        }

        [HttpGet("/Content/Parts/Common/User.svc/GetInfo2")]
        [HttpPost("/Content/Parts/Common/User.svc/GetInfo2")]
        public IActionResult GetInfo2(string appKey = null, string userName = null, string password = null)
        {
            userName = ResolveParam("userName", userName);
            password = ResolveParam("password", password);

            var user = AccountHelper.ValidateLogin(userName, password);
            if (user != null && user.IsActive)
                return Ok(new[] { user.Id.ToString(), user.FirstAndLastName });

            return Ok(Array.Empty<string>());
        }

        private string ResolveParam(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
                return value;

            if (Request.Query.TryGetValue(key, out var queryValue))
            {
                var val = queryValue.FirstOrDefault();
                if (!string.IsNullOrEmpty(val))
                    return val;
            }

            if (Request.HasFormContentType && Request.Form.TryGetValue(key, out var formValue))
            {
                var val = formValue.FirstOrDefault();
                if (!string.IsNullOrEmpty(val))
                    return val;
            }

            return value;
        }
    }
}
