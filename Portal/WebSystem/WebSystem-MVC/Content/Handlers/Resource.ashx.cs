using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.SessionState;

using WCMS.Framework;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Handlers
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class TextResource : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            WebTextResource res = null;
            int id = DataHelper.GetId(context.Request, WebColumns.Id);

            if (id > 0 && (res = WebTextResource.Get(id)) != null) { }
            else
            {
                var name = DataHelper.Get(context.Request, WebColumns.Name);
                if (!string.IsNullOrEmpty(name))
                    res = WebTextResource.Provider.Get(name);
            }

            // 6-months caching by browser
            context.Response.Cache.SetExpires(DateTime.Now.AddDays(180));
            
            if (res != null)
            {
                context.Response.ContentType = res.ContentType.Value;
                context.Response.Write(res.Content);
                return;
            }
            else
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("Hello world!");
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
