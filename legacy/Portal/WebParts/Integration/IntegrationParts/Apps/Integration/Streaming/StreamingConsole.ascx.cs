using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.Integration.Streaming
{
    public partial class StreamingConsole : System.Web.UI.UserControl
    {
        public string LogOutUrl { get; set; }
        public string StreamName { get; set; }
        public string StreamOwner { get; set; }
        public bool CheckSession { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var context = new WContext(this);
                var element = context.Element;
                var set = context.GetParameterSet();

                var homeUrl = ParameterizedWebObject.GetValue("HomeUrl", element, set);
                var logOutUrl = ParameterizedWebObject.GetValue("LogOutUrl", element, set);

                if (string.IsNullOrEmpty(homeUrl))
                    homeUrl = context.Site.BuildRelativeUrl();

                linkHome.HRef = homeUrl;
                linkLogOut.HRef = logOutUrl;

                var adminGroup = ParameterizedWebObject.GetValue("AdminGroup", element, set);
                if (!string.IsNullOrEmpty(adminGroup))
                {
                    var group = WebGroup.Get(adminGroup);
                    if (group != null && group.IsMember(WSession.Current.UserId))
                    {
                        var adminUrl = ParameterizedWebObject.GetValue("AdminUrl", element, set);
                        linkAdmin.HRef = adminUrl;
                        panelAdminLink.Visible = true;
                    }
                }

                LogOutUrl = logOutUrl;
                StreamName = ParameterizedWebObject.GetValue("StreamName", "webcast", element, set);
                StreamOwner = ParameterizedWebObject.GetValue("StreamOwner", "Integration Portal", element, set);
                CheckSession = WSession.Current.IsLoggedIn;
            }
        }
    }
}