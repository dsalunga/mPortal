using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WCMS.LessonReviewer.Handers
{
    /// <summary>
    /// Summary description for AjaxHandler
    /// </summary>
    public class AjaxHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            var method = context.Request["Method"];

            switch (method)
            {
                case "Status":
                    var varName = context.Request["VarName"];
                    if (!string.IsNullOrEmpty(varName))
                    {
                        context.Request.ContentType = "application/javascript";
                        context.Response.Write(string.Format("var {0} = \"OK\";", varName));
                    }
                    break;

                case "KeepAlive":
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("OK");

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