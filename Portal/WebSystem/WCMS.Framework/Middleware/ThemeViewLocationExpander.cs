using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Http;

namespace WCMS.Framework.Middleware
{
    /// <summary>
    /// Custom IViewLocationExpander that maps CMS themes to Razor layout files.
    /// Adds theme-specific view locations based on the resolved page's WebTheme.
    /// </summary>
    public class ThemeViewLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            var httpContext = context.ActionContext.HttpContext;
            var themeName = httpContext.Items["CmsThemeName"] as string;
            if (!string.IsNullOrEmpty(themeName))
            {
                context.Values["CmsTheme"] = themeName;
            }
        }

        public IEnumerable<string> ExpandViewLocations(
            ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            if (context.Values.TryGetValue("CmsTheme", out var themeName)
                && !string.IsNullOrEmpty(themeName))
            {
                var themeLocations = new List<string>
                {
                    $"/Views/Themes/{themeName}/{{1}}/{{0}}.cshtml",
                    $"/Views/Themes/{themeName}/Shared/{{0}}.cshtml",
                    $"/Views/Themes/{themeName}/{{0}}.cshtml"
                };

                // Theme-specific locations take priority
                return themeLocations.Concat(viewLocations);
            }

            return viewLocations;
        }
    }
}
