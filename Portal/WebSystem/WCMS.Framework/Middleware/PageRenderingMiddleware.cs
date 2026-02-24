using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using WCMS.Framework.Core;

namespace WCMS.Framework.Middleware
{
    /// <summary>
    /// ASP.NET Core middleware that renders CMS pages resolved by PageResolutionMiddleware.
    /// Loads the WebTemplate, iterates WebTemplatePanel zones, and renders WebPageElement
    /// instances via ViewComponents into the final HTML response.
    /// </summary>
    public class PageRenderingMiddleware
    {
        private readonly RequestDelegate _next;

        public PageRenderingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var page = httpContext.Items["ResolvedPage"] as WPage;
            if (page == null)
            {
                await _next(httpContext);
                return;
            }

            // Determine the master page and its template
            var masterPage = page.MasterPage;

            // Store rendering context for downstream consumers (ViewComponents, controllers)
            httpContext.Items["CmsMasterPage"] = masterPage;
            httpContext.Items["CmsPageElements"] = page.Elements;

            // Determine layout based on theme
            var theme = page.Theme;
            if (theme != null)
            {
                httpContext.Items["CmsTheme"] = theme;
                httpContext.Items["CmsThemeName"] = theme.Name;
                httpContext.Items["CmsLayout"] = GetLayoutPath(theme);
            }

            // Build panel-to-elements mapping
            var panelElements = new Dictionary<int, List<WebPageElement>>();
            var panels = page.Panels;
            if (panels != null)
            {
                foreach (var panel in panels)
                {
                    var elements = page.Elements?
                        .Where(e => e.TemplatePanelId == panel.TemplatePanelId && e.IsActive)
                        .ToList();

                    panelElements[panel.TemplatePanelId] = elements ?? new List<WebPageElement>();
                }
            }

            httpContext.Items["CmsPanelElements"] = panelElements;

            await _next(httpContext);
        }

        private static string GetLayoutPath(WebTheme theme)
        {
            if (theme == null)
                return "_Layout";

            // Map theme name to layout file
            return $"_Layout_{theme.Name}";
        }
    }
}
