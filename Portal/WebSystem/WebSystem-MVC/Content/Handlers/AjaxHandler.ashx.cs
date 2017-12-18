using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.Services;
using System.Web.Services.Protocols;

using WCMS.Framework;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.Handlers
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AjaxHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string method = context.Request["Method"];

            switch (method)
            {
                case "SetDesignPanel":
                    var expanded = context.Request["Expanded"];
                    var left = context.Request["Left"];
                    var top = context.Request["Top"];
                    var init = context.Request["Init"];

                    if (!string.IsNullOrEmpty(expanded))
                        WSession.Current.InDesignPanelExpanded = expanded == "1";

                    if (!string.IsNullOrEmpty(left))
                        WSession.Current.InDesignPanelLeft = DataHelper.GetInt32(left);

                    if (!string.IsNullOrEmpty(top))
                        WSession.Current.InDesignPanelTop = DataHelper.GetInt32(top);

                    if (!string.IsNullOrEmpty(init))
                        WSession.Current.IsDesignInitiated = init == "1";

                    //context.Response.Write("Hello World: " + WSession.Current.InDesignPanelExpanded);
                    break;

                case "GetText":
                    var pageId = DataHelper.GetId(context.Request, "PageId");
                    var pageUrl = DataHelper.Get(context.Request, "Url");
                    WPage page = null;

                    if (pageId > 0)
                        page = WPage.Get(pageId);
                    else if (!string.IsNullOrEmpty(pageUrl))
                        page = WebRewriter.ResolvePage(pageUrl);

                    if (page != null)
                        context.Response.Write(page.Name);

                    break;

                case "Status": // fire and forget
                case "KeepAlive":
                    context.Session["Heartbeat"] = DateTime.Now;
                    context.Response.Write("OK");
                    break;

                case "SessionValid": // Checks status
                    var session = WSession.Current.UserSession;
                    if (session != null)
                    {
                        // && session.AspNetSessionID.Equals(context.Session.SessionID))
                        context.Session["Heartbeat"] = DateTime.Now;
                        context.Response.Write("1");
                    }
                    else
                    {
                        context.Response.Write("0");
                    }

                    break;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
