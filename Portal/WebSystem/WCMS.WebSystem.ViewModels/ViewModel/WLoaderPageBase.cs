using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.UI;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.ViewModel
{
    public abstract class WLoaderPageBase : WPageControl
    {
        public static bool CheckAccess(WPage page, WQuery query, WSession session)
        {
            //if (WSession.Current.IsAdministrator)
            //    return true;

            string sessionId = query.Get(WConstants.SessionId);
            if (!string.IsNullOrEmpty(sessionId))
            {
                // Propagate existing session, only when:
                // - Existing user session is present and the IP address matches the new domain

                var context = WSession.Context;
                var request = context.Request;
                var userSession = WSession.UserSessions.Sessions.FirstOrDefault(i => i.SessionId.ToString()
                        .Equals(sessionId, StringComparison.InvariantCultureIgnoreCase));
                //WSession.UserSessions.BrowserCache.TryGetValue(context.Session.SessionID, out browserSession);
                if (userSession != null && userSession.UserId > 0) // Make sure user has a valid active session
                {
                    var browserSession = WSession.UserSessions.BrowserCache.Values
                        .FirstOrDefault(i => i.UserId == userSession.UserId && i.IPAddress == context.Request.UserHostAddress);
                    var isLoggedIn = userSession.UserId == session.UserId;
                    if (isLoggedIn || browserSession != null) // May cause issue locally due to ipv6 ::1 vs 127.0.0.1; //request.UserAgent == browserSession.UserAgent
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

                    var sessionIdAuthAllowed = DataHelper.GetBool(page.GetParameterValue("AnyDeviceAuthBySessionId"), false);
                    if (sessionIdAuthAllowed)
                        return true;
                }
            }

            bool redirect = true;
            string loginUrl = page.Site.AbsLoginUrl;
            int accessCheck = page.GetPublicAccess(session);
            if (accessCheck == PublicAccessCheckResult.Granted)
            {
                // If cookie present, not logged in, not in login page then trigger auth.
                if (session.IsLoggedIn || !WSession.IsLoginCookiePresent() || WebHelper.IsSameUrl(query.BasePath, loginUrl))
                    return true;
            }
            else if (accessCheck == PublicAccessCheckResult.NotLoggedIn && WebHelper.IsSameUrl(query.BasePath, loginUrl))
            {
                // Granted or this is a login page
                return true;
            }
            else
            {
                // Can be NotLoggedIn or Denied
                if (accessCheck == PublicAccessCheckResult.Denied)
                {
                    WHelper.ShowAccessDenied(page, query);
                    return false;
                }
            }

            if (redirect)
            {
                if (!string.IsNullOrEmpty(loginUrl)) // && (string.IsNullOrEmpty(sessionId) || WebHelper.IsSameDomain(WSession.Context, loginUrl)))
                {
                    // Redirect to Login page
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
                //else if (!string.IsNullOrEmpty(sessionId))
                //{
                //    throw new Exception("SessionId is invalid or expired. Make sure your login is valid.");
                //}
                else
                {
                    throw new Exception("I don't know where to redirect you or the login page is not present.");
                }
            }

            return true;
        }
    }
}
