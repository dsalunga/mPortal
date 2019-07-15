using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WCMS.Framework;
using WCMS.Framework.Security;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.Ext;

namespace WCMS.WebSystem.Apps.Integration
{
    /// <summary>
    /// Summary description for Member
    /// </summary>
    [WebService(Namespace = "http://someorg.org/webservices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class MemberWebService : System.Web.Services.WebService
    {
        //[WebMethod]
        //public string HelloWorld()
        //{
        //    return "Hello World";
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="loginSession">Not implemented</param>
        /// <returns></returns>
        [WebMethod]
        public WSUserInfo OneLogin(string userName, string password, bool loginSession = false)
        {
            WSUserInfo userInfo = null;
            var provider = new ExtProvider();
            var result = provider.LoginCheck(userName, password);

            if (result.StatusCode == LoginCodes.Success)
            {
                var oneUser = ExtProvider.GetUserInfo(userName);
                if (oneUser != null)
                {
                    userInfo = new WSUserInfo();
                    userInfo.UserName = oneUser.UserName;
                    userInfo.Email = oneUser.Email;
                    userInfo.FirstName = oneUser.FirstName;
                    userInfo.LastName = oneUser.LastName;
                    userInfo.MiddleName = oneUser.MiddleName;
                }
            }

            return userInfo;
        }

        [WebMethod]
        public bool Check(string appKey, string userName, string password, bool includeAllAccounts)
        {
            var user = AccountHelper.ValidateLogin(userName, password);
            if (user != null && user.IsActive)
            {
                if (includeAllAccounts)
                    return true;

                MemberLink link = MemberLink.Provider.GetByUserId(user.Id);
                if (link != null && link.MemberId > 0)
                    return true;
            }

            return false;
        }

        [WebMethod]
        public WSMemberInfo GetInfo(string appKey, string userName, string password, bool includeAllAccounts)
        {
            var user = AccountHelper.ValidateLogin(userName, password);
            if (user != null && user.IsActive)
            {
                MemberLink link = MemberLink.Provider.GetByUserId(user.Id);
                if (link != null && link.MemberId > 0)
                    return new WSMemberInfo(user, link);

                if (includeAllAccounts)
                    return new WSMemberInfo(user);
            }

            return null;
        }

        [WebMethod]
        public string[] GetInfo2(string appKey, string userName, string password, bool includeAllAccounts)
        {
            var user = AccountHelper.ValidateLogin(userName, password);
            if (user != null && user.IsActive)
            {
                MemberLink link = MemberLink.Provider.GetByUserId(user.Id);
                if (link != null && link.MemberId > 0)
                    return new string[] { user.Id.ToString(), user.FirstAndLastName, link.MemberId.ToString() };

                if (includeAllAccounts)
                    return new string[] { user.Id.ToString(), user.FirstAndLastName };
            }

            return new string[] { };
        }
    }
}
