using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebPages;

using RazorEngine;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem
{
    public class RazorHelper
    {
        public static IDictionary<object, dynamic> SetPanel(IDictionary<object, dynamic> pageData, string panelName)
        {
            pageData["PanelName"] = panelName;

            return pageData;
        }

        public static HelperResult RenderPanel(string panelName, WebPage page) //, params object[] data)
        {
            return page.RenderPage("~/_loader.cshtml", SetPanel(page.PageData, panelName));
        }

        public static string RenderPage(string template, object model, string cacheName)
        {
            string tmpl = null;
            if (WCMS.Framework.WConfig.Environment == WCMS.Framework.SystemEnvironment.PROD)
            {
                var fileCacheName = "WCMS.RazorTmpl-" + cacheName;
                var cache = MemoryCache.Default;
                tmpl = cache[fileCacheName] as string;
                if (tmpl == null)
                {
                    var cachedFilePath = HttpContext.Current.Server.MapPath(template);
                    var filePaths = new List<string>();
                    filePaths.Add(cachedFilePath);

                    var policy = new CacheItemPolicy();
                    policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(15.0);
                    policy.ChangeMonitors.Add(new HostFileChangeMonitor(filePaths));

                    // Fetch the file contents.
                    tmpl = File.ReadAllText(cachedFilePath);
                    cache.Set(fileCacheName, tmpl, policy);
                }
            }
            else
            {
                tmpl = File.ReadAllText(HttpContext.Current.Server.MapPath(template));
            }

            var output = Razor.Parse(tmpl, model, cacheName);
            return output;
        }

        public static string RenderPage(string template, object model)
        {
            return RenderPage(template, model, null);
        }

        public static string FormatJsString(string s)
        {
            return WebHelper.FormatJsString(s);
        }
    }
}
