using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using WCMS.LessonReviewer.Core;

namespace WCMS.LessonReviewer
{
    public class Global : System.Web.HttpApplication, IReadOnlySessionState
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

        private void Application_PostAcquireRequestState(object sender, EventArgs e)
        {
            string path = Request.Path;
            if (path.IndexOf("/Admin/", StringComparison.InvariantCultureIgnoreCase) >= 0 && path.IndexOf(".aspx", StringComparison.InvariantCulture) >= 0)
            {
                if (path.IndexOf("/Admin/Login.aspx", StringComparison.InvariantCultureIgnoreCase) < 0)
                {
                    // Not in Login
                    var session = Context.Session[MakeUpServiceSession.SessionKey] as MakeUpServiceSession;
                    if (session == null || !session.LoggedIn)
                        Context.Response.Redirect("~/Admin/Login.aspx", true);
                }
            }
        }
    }
}
