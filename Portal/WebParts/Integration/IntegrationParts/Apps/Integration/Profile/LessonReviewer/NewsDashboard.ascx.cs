using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS;
using WCMS.Common.Utilities;

using WCMS.Framework;

using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.WebParts.Profile.LessonReviewer
{
    public partial class NewsDashboard : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var context = new WContext(this);
                var element = context.Element;
                var session = Session[MakeUpSession.SessionKey] as MakeUpSession;
                if (session == null)
                    session = MemberHelper.TryRecreateSession(context);
                if (session == null)
                {
                    //var makeUpHomeUrl = element.GetParameterValue("MakeUpHomeUrl");
                    //CancelRedirect(makeUpHomeUrl);
                }
                else
                {
                    var localeMakeUpWebHandlerUrl = element.GetParameterValue("LocaleMakeUpWebHandlerUrl");
                    if (!string.IsNullOrEmpty(localeMakeUpWebHandlerUrl))
                    {
                        var query = new WQuery(localeMakeUpWebHandlerUrl);
                        query.Set("Method", "Status");
                        query.Set("VarName", "makeUpTest");
                        query.Set("Ticks", DateTime.Now.Ticks);
                        scriptMakeUpTest.Text = WebUtil.BuildScriptTagWithSource(query.BuildQuery(true));
                    }
                }
            }
        }

        private static void CancelRedirect(string makeUpHomeUrl)
        {
            if (!string.IsNullOrEmpty(makeUpHomeUrl))
                QueryParser.StaticRedirect(makeUpHomeUrl, false);
            else
                QueryParser.StaticRedirect("/", false);
        }

        protected void cmdContinue_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            var playbackUrl = context.Element.GetParameterValue("PlaybackUrl");
            if (!string.IsNullOrEmpty(playbackUrl))
            {
                var query = new WQuery(playbackUrl);
                query.Add(context.Query);
                query.Redirect();
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            Session[MakeUpSession.SessionKey] = null;

            var context = new WContext(this);
            var makeUpHomeUrl = context.Element.GetParameterValue("MakeUpHomeUrl");
            var date = context.Get("Date");
            var query = new WQuery(makeUpHomeUrl);
            if (!string.IsNullOrEmpty(date))
                query.Set("Date", date);

            CancelRedirect(query.BuildQuery(true));
        }
    }
}