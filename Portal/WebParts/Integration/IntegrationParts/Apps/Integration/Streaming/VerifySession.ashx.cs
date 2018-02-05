using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.Apps.Integration.Streaming
{
    /// <summary>
    /// Summary description for VerifySession
    /// </summary>
    public class VerifySession : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var sessionId = context.Request["sessionId"];
            var pageId = DataUtil.GetId(context.Request, "pageId");
            var stream = context.Request["stream"];
            var ip = context.Request["ip"];
            var valid = false;
            WPage page = null;

            if (pageId > 0 && !string.IsNullOrEmpty(stream) && (page = WPage.Get(pageId)) != null)
            {
                var streamName = page.GetParameterValue("StreamName", "", true);
                if (streamName.Equals(stream, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!string.IsNullOrEmpty(sessionId))
                    {
                        var session = WSession.UserSessions.Sessions.FirstOrDefault(i => i.SessionId.ToString()
                        .Equals(sessionId, StringComparison.InvariantCultureIgnoreCase));
                        if (session != null && session.UserId > 0)
                        {
                            // Check user page access here
                            if (page.GetPublicAccess(session.UserId) == PublicAccessCheckResult.Granted)
                                valid = true;
                        }
                    }
                    else if (!string.IsNullOrEmpty(ip))
                    {
                        // Validate ip here
                        if (page.GetPublicAccess(-1, ip) == PublicAccessCheckResult.Granted)
                            valid = true;
                    }
                }
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(valid ? "OK" : "FAIL");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}