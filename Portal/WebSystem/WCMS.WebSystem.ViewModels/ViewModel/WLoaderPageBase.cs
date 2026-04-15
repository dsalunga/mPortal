using System;
using System.Linq;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.ViewModel
{
    public static class WLoaderPageBase
    {
        public static bool CheckAccess(WPage page, WQuery query, WSession session)
        {
            string sessionId = query.Get(WConstants.SessionId);
            if (!string.IsNullOrEmpty(sessionId))
            {
                var context = WSession.Context;
                var userSession = WSession.UserSessions.Sessions.FirstOrDefault(i => i.SessionId.ToString()
                        .Equals(sessionId, StringComparison.InvariantCultureIgnoreCase));
                if (userSession != null && userSession.UserId > 0)
                {
                    var remoteIp = context?.Connection.RemoteIpAddress?.ToString();
                    var browserSession = WSession.UserSessions.BrowserCache.Values
                        .FirstOrDefault(i => i.UserId == userSession.UserId && i.IPAddress == remoteIp);
                    var isLoggedIn = userSession.UserId == session.UserId;
                    if (isLoggedIn || browserSession != null)
                    {
                        if (!isLoggedIn)
                            session.Login(userSession.UserId, true);

                        string openRequest = query.Get(WConstants.Open);
                        if (string.IsNullOrEmpty(openRequest))
                        {
                            query.Remove(WConstants.SessionId);
                            query.Redirect();
                        }
                        return true;
                    }

                    var sessionIdAuthAllowed = DataUtil.GetBool(page.GetParameterValue("AnyDeviceAuthBySessionId"), false);
                    if (sessionIdAuthAllowed)
                        return true;
                }
            }

            bool redirect = true;
            string loginUrl = page.Site.AbsLoginUrl;
            int accessCheck = page.GetPublicAccess(session);
            if (accessCheck == PublicAccessCheckResult.Granted)
            {
                if (session.IsLoggedIn || !WSession.IsLoginCookiePresent() || WebUtil.IsSameUrl(query.BasePath, loginUrl))
                    return true;
            }
            else if (accessCheck == PublicAccessCheckResult.NotLoggedIn && WebUtil.IsSameUrl(query.BasePath, loginUrl))
            {
                return true;
            }
            else
            {
                if (accessCheck == PublicAccessCheckResult.Denied)
                {
                    WHelper.ShowAccessDenied(page, query);
                    return false;
                }
            }

            if (redirect)
            {
                if (!string.IsNullOrEmpty(loginUrl))
                {
                    var re = new WQuery(loginUrl);
                    var loginUri = re.CreateUri();
                    var sourceUri = query.CreateUri();
                    var buildAbs = !loginUri.Host.Equals(sourceUri.Host);
                    if (buildAbs || query.BasePath != "/" && !query.BasePath.Equals("/default.aspx?", StringComparison.InvariantCultureIgnoreCase))
                    {
                        re.SetSource(query.BuildQuery(buildAbs));
                    }

                    re.Redirect();
                    return false;
                }
                else
                {
                    throw new Exception("I don't know where to redirect you or the login page is not present.");
                }
            }

            return true;
        }
    }
}
