using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.Api
{
    /// <summary>
    /// Legacy-compatible replacement for Streaming/VerifySession.ashx.
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    public class LegacyStreamingVerifySessionController : ControllerBase
    {
        [HttpGet("/Apps/Integration/Streaming/VerifySession.ashx")]
        [HttpPost("/Apps/Integration/Streaming/VerifySession.ashx")]
        [HttpGet("/Content/Parts/Integration/Streaming/VerifySession.ashx")]
        [HttpPost("/Content/Parts/Integration/Streaming/VerifySession.ashx")]
        public IActionResult Verify(
            [FromQuery] string sessionId,
            [FromQuery] string stream,
            [FromQuery] string ip,
            [FromQuery] int pageId)
        {
            sessionId = GetValue("sessionId", sessionId);
            stream = GetValue("stream", stream);
            ip = GetValue("ip", ip);
            if (pageId <= 0)
                pageId = DataUtil.GetInt32(GetValue("pageId"));

            var valid = false;
            var page = pageId > 0 ? WPage.Get(pageId) : null;
            if (page != null && !string.IsNullOrEmpty(stream))
            {
                var streamName = page.GetParameterValue("StreamName", "", true);
                if (streamName.Equals(stream, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!string.IsNullOrEmpty(sessionId))
                    {
                        var session = WSession.UserSessions.Sessions.FirstOrDefault(i =>
                            i.SessionId.ToString().Equals(sessionId, StringComparison.InvariantCultureIgnoreCase));

                        if (session != null && session.UserId > 0
                            && page.GetPublicAccess(session.UserId) == PublicAccessCheckResult.Granted)
                        {
                            valid = true;
                        }
                    }
                    else if (!string.IsNullOrEmpty(ip))
                    {
                        if (page.GetPublicAccess(-1, ip) == PublicAccessCheckResult.Granted)
                            valid = true;
                    }
                }
            }

            return Content(valid ? "OK" : "FAIL", "text/plain");
        }

        private string GetValue(string key, string queryValue = null)
        {
            if (!string.IsNullOrEmpty(queryValue))
                return queryValue;

            var query = Request.Query[key].ToString();
            if (!string.IsNullOrEmpty(query))
                return query;

            if (Request.HasFormContentType)
                return Request.Form[key].ToString();

            return string.Empty;
        }
    }
}
