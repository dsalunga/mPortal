using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;

namespace WCMS.WebSystem.Api
{
    /// <summary>
    /// Replaces the legacy ASMX AccountService (AccountService.asmx).
    /// Requires authentication for session management operations.
    /// </summary>
    [ApiController]
    [Route("api/account")]
    [Authorize]
    public class AccountApiController : ControllerBase
    {
        [HttpGet("session/{sessionId}")]
        [ResponseCache(Duration = 0, NoStore = true)]
        public IActionResult GetUserSession(string sessionId)
        {
            UserSession session = null;
            if (!string.IsNullOrEmpty(sessionId))
            {
                var guid = new Guid(sessionId);
                session = WSession.UserSessions.Sessions.FirstOrDefault(i => i.SessionId.Equals(guid));
            }

            return Ok(session);
        }

        [HttpGet("session/by-auth-key/{authKey}")]
        public IActionResult GetUserSessionByAuthKey(string authKey)
        {
            UserSession session = null;
            if (!string.IsNullOrEmpty(authKey))
            {
                var authKeyGuid = new Guid(authKey);
                session = WSession.UserSessions.Sessions.FirstOrDefault(i => i.AuthKey.Equals(authKeyGuid));
                if (session != null)
                    session.AuthKey = Guid.NewGuid();
            }

            return Ok(session);
        }
    }
}
