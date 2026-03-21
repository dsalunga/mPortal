using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.WebParts.Profile
{
    public partial class ExternalAuth : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                WContext context = new WContext(this);
                var element = context.Element;

                var amsAuthUrl = element.GetParameterValue(MemberConstants.External_AUTH_URL_KEY);
                if (!string.IsNullOrEmpty(amsAuthUrl) && WSession.Current.IsLoggedIn)
                {
                    string returnUrl = element.GetParameterValue(MemberConstants.External_AUTH_RETURN_URL_KEY);

                    var userSession = WSession.UserSessions.Sessions.FirstOrDefault(i => i.UserId == WSession.Current.UserId);
                    if (userSession != null)
                    {
                        var query = new WQuery(amsAuthUrl);
                        query.Set(MemberConstants.External_AUTH_PARAM_KEY, userSession.AuthKey);
                        query.Set("From", "Integration-Portal");

                        if (!string.IsNullOrEmpty(returnUrl))
                            query.Set(MemberConstants.External_AUTH_RETURN_URL_KEY, returnUrl);

                        query.Redirect();
                    }
                }
            }
        }
    }
}
