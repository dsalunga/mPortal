using System;
using System.IO;
using System.Runtime.Caching;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem
{
    public class RazorHelper
    {
        public static string RenderPage(string template, object model, string cacheName)
        {
            string tmpl = null;
            if (WConfig.Environment == SystemEnvironment.PROD)
            {
                var fileCacheName = "WCMS.RazorTmpl-" + cacheName;
                var cache = MemoryCache.Default;
                tmpl = cache[fileCacheName] as string;
                if (tmpl == null)
                {
                    var cachedFilePath = WebHelper.MapPath(template, true);
                    var filePaths = new List<string>();
                    filePaths.Add(cachedFilePath);

                    var policy = new CacheItemPolicy();
                    policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(15.0);
                    policy.ChangeMonitors.Add(new HostFileChangeMonitor(filePaths));

                    tmpl = File.ReadAllText(cachedFilePath);
                    cache.Set(fileCacheName, tmpl, policy);
                }
            }
            else
            {
                tmpl = File.ReadAllText(WebHelper.MapPath(template, true));
            }

            // TODO: Replace with Razor templating engine (e.g. RazorLight) for model binding.
            // Currently returns the raw template content.
            return tmpl;
        }

        public static string RenderPage(string template, object model)
        {
            return RenderPage(template, model, null);
        }

        public static string FormatJsString(string s)
        {
            return WebHelper.FormatJsString(s);
        }

        public static async Task<IHtmlContent> RenderPanelAsync(string panelName, IHtmlHelper html)
        {
            html.ViewData["PanelName"] = panelName;
            return await html.PartialAsync("~/_loader.cshtml");
        }
    }
}
