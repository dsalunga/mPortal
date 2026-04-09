using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;

namespace WCMS.WebSystem.Api
{
    /// <summary>
    /// Legacy-compatible replacement for /Content/Parts/Central/AccountService.asmx.
    /// Returns ASP.NET AJAX style payload: { d: ... }.
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    public class LegacyAccountServiceController : ControllerBase
    {
        [HttpGet("/Content/Parts/Central/AccountService.asmx/GetUserSession")]
        [HttpPost("/Content/Parts/Central/AccountService.asmx/GetUserSession")]
        public IActionResult GetUserSession([FromQuery] string sessionId, [FromBody] SessionRequest request)
        {
            sessionId = Coalesce(sessionId, request?.SessionId);
            UserSession session = null;

            if (Guid.TryParse(sessionId, out var guid))
                session = WSession.UserSessions.Sessions.FirstOrDefault(i => i.SessionId.Equals(guid));

            return new JsonResult(new { d = session });
        }

        [HttpGet("/Content/Parts/Central/AccountService.asmx/GetUserSessionByAuthKey")]
        [HttpPost("/Content/Parts/Central/AccountService.asmx/GetUserSessionByAuthKey")]
        public IActionResult GetUserSessionByAuthKey([FromQuery] string authKey, [FromBody] AuthKeyRequest request)
        {
            authKey = Coalesce(authKey, request?.AuthKey);
            UserSession session = null;

            if (Guid.TryParse(authKey, out var authKeyGuid))
            {
                session = WSession.UserSessions.Sessions.FirstOrDefault(i => i.AuthKey.Equals(authKeyGuid));
                if (session != null)
                    session.AuthKey = Guid.NewGuid();
            }

            return new JsonResult(new { d = session });
        }

        private static string Coalesce(params string[] values)
        {
            foreach (var value in values)
            {
                if (!string.IsNullOrWhiteSpace(value))
                    return value;
            }

            return string.Empty;
        }
    }

    public class SessionRequest
    {
        public string SessionId { get; set; }
    }

    public class AuthKeyRequest
    {
        public string AuthKey { get; set; }
    }
}
