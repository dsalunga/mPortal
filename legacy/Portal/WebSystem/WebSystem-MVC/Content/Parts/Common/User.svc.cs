using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCMS.Framework;
using WCMS.Framework.Utilities;

namespace WCMS.WebSystem.WebParts.Common
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUser" in both code and config file together.
    [ServiceContract]
    public interface IUser
    {
        [OperationContract]
        bool Check(string appKey, string userName, string password);

        [OperationContract]
        WSUserInfo GetInfoFromSession(string appKey, string sessionId, string ipAddress, string userAgent);

        [OperationContract]
        WSUserInfo GetInfo(string appKey, string userName, string password);

        [OperationContract]
        string[] GetInfo2(string appKey, string userName, string password);
    }

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "User" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select User.svc or User.svc.cs at the Solution Explorer and start debugging.
    public class User : IUser
    {
        public bool Check(string appKey, string userName, string password)
        {
            var user = AccountHelper.ValidateLogin(userName, password);
            if (user != null && user.IsActive)
                return true;

            return false;
        }

        public WSUserInfo GetInfoFromSession(string appKey, string sessionId, string ipAddress, string userAgent)
        {
            if (!string.IsNullOrEmpty(sessionId))
            {
                var guid = new Guid(sessionId);
                var session = WSession.UserSessions.Sessions.FirstOrDefault(i => i.SessionId.Equals(guid));
                if (session != null)
                {
                    var browser = WSession.UserSessions.BrowserCache.Values.FirstOrDefault(i => i.IPAddress.Equals(ipAddress) && i.UserAgent.Equals(userAgent));
                    if (browser != null)
                        return new WSUserInfo(WebUser.Get(session.UserId));
                }
            }

            return null;
        }

        public WSUserInfo GetInfo(string appKey, string userName, string password)
        {
            var user = AccountHelper.ValidateLogin(userName, password);
            if (user != null && user.IsActive)
            {
                return new WSUserInfo(user);

                //var item = new WSUserInfo();
                //item.Id = user.Id;
                //item.FirstName = user.FirstName;
                //item.LastName = user.LastName;
                //item.UserName = user.UserName;
                //item.MobileNumber = user.MobileNumber;
                //item.Email = user.Email;

                //if (user.IsServiceAccount())
                //    item.IsServiceAccount = true;

                //return new string[] { user.Id.ToString(), user.FirstAndLastName };
            }

            return null;
        }

        public string[] GetInfo2(string appKey, string userName, string password)
        {
            var user = AccountHelper.ValidateLogin(userName, password);
            if (user != null && user.IsActive)
            {
                //var item = new WSUserInfo();
                //item.Id = user.Id;
                //item.FirstName = user.FirstName;
                //item.LastName = user.LastName;
                //item.UserName = user.UserName;
                //item.MobileNumber = user.MobileNumber;
                //item.Email = user.Email;

                //if (user.IsServiceAccount())
                //    item.IsServiceAccount = true;

                return new string[] { user.Id.ToString(), user.FirstAndLastName };
            }

            return new string[] { };
        }
    }
}
