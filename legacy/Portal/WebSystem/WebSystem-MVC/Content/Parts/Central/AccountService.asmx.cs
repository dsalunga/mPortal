using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Central
{
    /// <summary>
    /// Summary description for AccountService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AccountService : System.Web.Services.WebService
    {
        [WebMethod]
        public UserSession GetUserSession(string sessionId)
        {
            UserSession session = null;
            if (!string.IsNullOrEmpty(sessionId))
            {
                var guid = new Guid(sessionId);
                session = WSession.UserSessions.Sessions.FirstOrDefault(i => i.SessionId.Equals(guid));
            }

            return session;
        }

        [WebMethod]
        public UserSession GetUserSessionByAuthKey(string authKey)
        {
            UserSession session = null;
            if (!string.IsNullOrEmpty(authKey))
            {
                var authKeyGuid = new Guid(authKey);
                session = WSession.UserSessions.Sessions.FirstOrDefault(i => i.AuthKey.Equals(authKeyGuid));
                if (session != null)
                    session.AuthKey = Guid.NewGuid();
            }

            return session;
        }
    }
}
