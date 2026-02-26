using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WCMS.Framework.Core;

namespace WCMS.Framework.Middleware
{
    /// <summary>
    /// ASP.NET Core middleware that resolves CMS pages from request URLs.
    /// Replaces the Application_BeginRequest URL rewriting from Global.asax.cs.
    /// Resolved pages are stored in HttpContext.Items for downstream use.
    /// </summary>
    public class PageResolutionMiddleware
    {
        private readonly RequestDelegate _next;

        public PageResolutionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var path = httpContext.Request.Path.Value?.ToLower() ?? "/";
            var pageExt = WConfig.PageExt;
            var isPageExtensionRequest = !string.IsNullOrWhiteSpace(pageExt) &&
                                         path.EndsWith(pageExt, StringComparison.OrdinalIgnoreCase);

            // Skip static files, API routes, content paths, and health checks
            if (path.StartsWith("/u/") || path.StartsWith("/content/") ||
                path.StartsWith("/api/") || path.StartsWith("/health") ||
                (Path.HasExtension(path) && !isPageExtensionRequest))
            {
                await _next(httpContext);
                return;
            }

            if (!WebObject.IsInitialized)
            {
                await _next(httpContext);
                return;
            }

            try
            {
                string hostName = httpContext.Request.Host.Host;
                var page = ResolvePageFromPath(hostName, path);

                if (page != null && page.IsActive)
                {
                    httpContext.Items["ResolvedPage"] = page;
                    httpContext.Items["ResolvedPageId"] = page.Id;
                }
            }
            catch
            {
                // Page resolution failed — fall through to next middleware
            }

            await _next(httpContext);
        }

        private static WPage ResolvePageFromPath(string hostName, string loweredPath)
        {
            if (string.IsNullOrEmpty(loweredPath))
                return null;

            WSite site = null;

            if (loweredPath == "/")
            {
                var identities = WebSiteIdentity.Provider.GetList();
                var found = identities
                    .Where(i => i.HostName.Equals(hostName, StringComparison.OrdinalIgnoreCase));
                if (found.Any())
                    site = found.First().Site;
                else
                    site = WConfig.DefaultSite;

                return site?.HomePage;
            }

            string[] urls = NormalizePathSegments(loweredPath);

            if (urls.Length == 1)
            {
                var shortUrl = WebShortUrl.Provider.Get(urls.First());
                if (shortUrl != null)
                    return shortUrl.Page;
            }

            var urlQ = new Queue<string>(urls);
            var sites = WSite.GetList();
            IEnumerable<WSite> searchSites = Enumerable.Empty<WSite>();

            var urlPeek = urlQ.Peek().ToLower();
            if (urlPeek == "default")
                urlQ.Dequeue();
            else
                searchSites = sites.Where(i => i.Identity.ToLower() == urlPeek);

            if (!searchSites.Any())
            {
                var identities = WebSiteIdentity.Provider.GetList();
                var found = identities
                    .Where(i => i.HostName.Equals(hostName, StringComparison.OrdinalIgnoreCase));
                if (found.Any())
                    site = found.First().Site;
            }
            else
            {
                urlQ.Dequeue();
                site = searchSites.First();
            }

            WPage page = null;
            if (site != null)
            {
                if (urlQ.Count > 0)
                {
                    WSite subSite;
                    do
                    {
                        urlPeek = urlQ.Peek().ToLower();
                        subSite = site.Children.FirstOrDefault(
                            s => s.ParentId == site.Id && s.Identity.ToLower() == urlPeek);
                        if (subSite != null) { site = subSite; urlQ.Dequeue(); }
                    } while (subSite != null && urlQ.Count > 0);
                }

                int parentPageId = -1;
                var pages = site.Pages;
                if (urlQ.Count > 0)
                {
                    while (urlQ.Count > 0)
                    {
                        var next = urlQ.Dequeue();
                        page = pages.FirstOrDefault(
                            p => p.ParentId == parentPageId && p.Identity.ToLower() == next);
                        if (page != null)
                            parentPageId = page.Id;
                        else
                            break;
                    }
                }
                else
                {
                    page = site.HomePage;
                }
            }

            return page;
        }

        /// <summary>
        /// Strips the file extension (if present) and splits the path into URL segments.
        /// </summary>
        private static string[] NormalizePathSegments(string loweredPath)
        {
            string ext = Path.GetExtension(loweredPath);
            string normalized = ext.StartsWith(".") ? loweredPath.Replace(ext, "/") : loweredPath;
            return normalized.Trim('/').Split('/');
        }
    }
}
