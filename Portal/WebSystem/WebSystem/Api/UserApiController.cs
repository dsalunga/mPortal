using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.Utilities;

namespace WCMS.WebSystem.Api
{
    /// <summary>
    /// Replaces the legacy WCF User.svc service.
    /// Requires authentication for user management operations.
    /// </summary>
    [ApiController]
    [Route("api/user")]
    [Authorize]
    public class UserApiController : ControllerBase
    {
        [HttpPost("check")]
        public IActionResult Check([FromBody] UserCheckRequest request)
        {
            var user = AccountHelper.ValidateLogin(request.UserName, request.Password);
            return Ok(user != null && user.IsActive);
        }

        [HttpPost("info-from-session")]
        public IActionResult GetInfoFromSession([FromBody] SessionInfoRequest request)
        {
            if (!string.IsNullOrEmpty(request.SessionId))
            {
                var guid = new Guid(request.SessionId);
                var session = WSession.UserSessions.Sessions.FirstOrDefault(i => i.SessionId.Equals(guid));
                if (session != null)
                {
                    var browser = WSession.UserSessions.BrowserCache.Values
                        .FirstOrDefault(i => i.IPAddress.Equals(request.IpAddress) && i.UserAgent.Equals(request.UserAgent));
                    if (browser != null)
                        return Ok(new WSUserInfo(WebUser.Get(session.UserId)));
                }
            }

            return Ok((WSUserInfo)null);
        }

        [HttpPost("info")]
        public IActionResult GetInfo([FromBody] UserCheckRequest request)
        {
            var user = AccountHelper.ValidateLogin(request.UserName, request.Password);
            if (user != null && user.IsActive)
                return Ok(new WSUserInfo(user));

            return Ok((WSUserInfo)null);
        }

        [HttpPost("info2")]
        public IActionResult GetInfo2([FromBody] UserCheckRequest request)
        {
            var user = AccountHelper.ValidateLogin(request.UserName, request.Password);
            if (user != null && user.IsActive)
                return Ok(new string[] { user.Id.ToString(), user.FirstAndLastName });

            return Ok(new string[] { });
        }
    }

    public class UserCheckRequest
    {
        public string AppKey { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class SessionInfoRequest
    {
        public string AppKey { get; set; }
        public string SessionId { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
    }
}
