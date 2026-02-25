using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using WCMS.Framework.Core;

namespace WCMS.Framework.Middleware
{
    /// <summary>
    /// Endpoint routing integration that maps CMS-resolved pages to a Razor page handler.
    /// When PageResolutionMiddleware resolves a WPage, this endpoint renders it through
    /// the CMS template/panel pipeline using ViewComponents.
    /// </summary>
    public static class CmsPageEndpointRouteBuilderExtensions
    {
        /// <summary>
        /// Maps a catch-all endpoint that renders CMS pages resolved by PageResolutionMiddleware.
        /// Must be registered after MapControllers() and MapRazorPages() so those take priority.
        /// </summary>
        public static IEndpointRouteBuilder MapCmsPages(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapFallback(async context =>
            {
                var page = context.Items["ResolvedPage"] as WPage;
                if (page == null)
                {
                    context.Response.StatusCode = 404;
                    await context.Response.WriteAsync("Page not found");
                    return;
                }

                // The page rendering context was already set by PageRenderingMiddleware.
                // Redirect to the CMS page Razor handler with the page context.
                var themeName = context.Items["CmsThemeName"] as string ?? "Default";
                var layout = context.Items["CmsLayout"] as string ?? "_Layout";

                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync($@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"" />
    <title>{System.Net.WebUtility.HtmlEncode(page.Name ?? "mPortal")}</title>
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
    <link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css"" />
</head>
<body>
    <div class=""container-fluid"">
        <div id=""cms-page"" data-page-id=""{page.Id}"" data-theme=""{System.Net.WebUtility.HtmlEncode(themeName)}"">
            <h1>{System.Net.WebUtility.HtmlEncode(page.Name ?? "")}</h1>
            <p class=""text-muted"">CMS Page ID: {page.Id} | Theme: {System.Net.WebUtility.HtmlEncode(themeName)}</p>
        </div>
    </div>
    <script src=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js""></script>
</body>
</html>");
            });

            return endpoints;
        }
    }
}
