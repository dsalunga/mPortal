using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Security;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration.ExtWebService;

namespace WCMS.WebSystem.Apps.Integration.Ext
{
    public class ExtProvider : IUserProvider
    {
        public WContext Context { get; set; }

        public ExternalLoginResult LoginCheck(WebUser user, string userName, string password)
        {
            string response1;

            if (WConfig.Environment == SystemEnvironment.DEV_ISOLATED)
            {
                response1 = "1";
            }
            else
            {
                if (WConfig.Environment != SystemEnvironment.PROD)
                {
                    WebRequest.DefaultWebProxy = new WebProxy("host_ip", 808)
                    {
                        Credentials = new NetworkCredential("username", "password")
                    };
                }

                var client = new webservice01someorgSoapClient();
                //var response1 = client.CheckUserAppPermission1("M4348DFFF32DF342ERKLK", "MAG00049", "pwd");
                //var response2 = client.GetUserInfo1("M4348DFFF32DF342ERKLK", "MAG00049");

                var externalId = userName;
                if (user != null)
                {
                    var link = MemberLink.Provider.GetByUserId(user.Id);
                    if (link != null && !string.IsNullOrEmpty(link.ExternalIdNo))
                        externalId = link.ExternalIdNo;
                }

                WSite site = Context == null ? null : Context.Site;
                //PageElementBase element = context == null ? null : context.Element;
                var integrationPortalName = ParameterizedWebObject.GetValue(ExtConstants.PARAM_APP_ONE_NAME, "", site);
                var oneAppKey = integrationPortalName.Equals(ExtConstants.TKP_APP_ONE_NAME, StringComparison.InvariantCultureIgnoreCase) ? ExtConstants.TKP_APP_ONE_KEY : ExtConstants.MMP_APP_ONE_KEY;

                response1 = client.CheckUserAppPermission1(oneAppKey, externalId, password);
            }

            var result = new ExternalLoginResult();
            result.StatusCode = response1.Equals("1") ? LoginCodes.Success : LoginCodes.Failed;

            return result;
        }

        public ExternalLoginResult LoginCheck(WebUser user, string password)
        {
            return LoginCheck(user, null, password);
        }

        public ExternalLoginResult LoginCheck(string userName, string password)
        {
            return LoginCheck(null, userName, password);
        }

        public static ExtUserInfo GetUserInfo(string externalId, WContext context = null)
        {
            if (WConfig.Environment == SystemEnvironment.DEV_ISOLATED)
            {
                var item = new ExtUserInfo();
                item.UserName = "UserName";
                item.ExternalId = "ExternalId";
                item.LastName = "LastName";
                item.FirstName = "FirstName";
                item.MiddleName = "MiddleName";
                item.Email = "email@someorg.org";
                return item;
            }


            if (WConfig.Environment != SystemEnvironment.PROD)
            {
                WebRequest.DefaultWebProxy = new WebProxy("host_ip", 808)
                {
                    Credentials = new NetworkCredential("username", "password")
                };
            }

            var client = new webservice01someorgSoapClient();
            //var response1 = client.CheckUserAppPermission1("appKey", "username", "pwd");

            WSite site = context == null ? null : context.Site;
            //PageElementBase element = context == null ? null : context.Element;
            var appName = ParameterizedWebObject.GetValue(ExtConstants.PARAM_APP_ONE_NAME, "", site);

            var oneAppKey = !string.IsNullOrEmpty(appName) && appName.Equals(ExtConstants.TKP_APP_ONE_NAME, StringComparison.InvariantCultureIgnoreCase) ? ExtConstants.TKP_APP_ONE_KEY : ExtConstants.MMP_APP_ONE_KEY;

            var response = client.GetUserInfo1(oneAppKey, externalId);
            if (response != null && response.Tables.Count > 0)
            {
                var userInfoTable = response.Tables[0];
                if (userInfoTable.Rows.Count > 0)
                    return ExtUserInfo.From(userInfoTable.Rows[0]);
            }

            return null;
        }
    }
}
