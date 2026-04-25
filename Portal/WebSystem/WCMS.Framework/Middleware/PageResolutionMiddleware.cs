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

            var allIdentities = WebSiteIdentity.Provider.GetList();
            var identityMatches = FindIdentityMatches(allIdentities, hostName, loweredPath);

            foreach (var identity in identityMatches)
            {
                var resolvedPath = StripIdentityBasePath(loweredPath, identity?.UrlPath);
                var resolved = ResolvePageWithinSite(identity?.Site, resolvedPath);
                if (resolved != null)
                    return resolved;
            }

            return ResolvePageWithinSite(null, loweredPath);
        }

        private static WPage ResolvePageWithinSite(WSite site, string resolvedPath)
        {
            if (resolvedPath == "/")
            {
                if (site == null)
                    site = WConfig.DefaultSite;

                return site?.HomePage;
            }

            string[] urls = NormalizePathSegments(resolvedPath);

            if (urls.Length == 1)
            {
                try
                {
                    var shortUrl = WebShortUrl.Provider.Get(urls.First());
                    if (shortUrl != null)
                        return shortUrl.Page;
                }
                catch
                {
                    // Ignore short-url lookup failures and continue regular site/page resolution.
                }
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
                if (site == null)
                    site = WConfig.DefaultSite;
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
            return normalized
                .Trim('/')
                .Split('/', StringSplitOptions.RemoveEmptyEntries);
        }

        private static IEnumerable<WebSiteIdentity> FindIdentityMatches(IEnumerable<WebSiteIdentity> identities, string hostName, string loweredPath)
        {
            if (identities == null)
                return Enumerable.Empty<WebSiteIdentity>();

            var requestHost = NormalizeHost(hostName);
            var requestPath = NormalizeIdentityPath(loweredPath);
            var candidates = new List<(WebSiteIdentity Identity, int HostScore, int PathLength)>();

            foreach (var identity in identities)
            {
                var hostScore = GetHostMatchScore(requestHost, identity?.HostName);
                if (hostScore <= 0)
                    continue;

                var identityPath = NormalizeIdentityPath(identity?.UrlPath);
                if (!PathMatchesIdentityBase(requestPath, identityPath))
                    continue;

                candidates.Add((identity, hostScore, identityPath == "/" ? 0 : identityPath.Length));
            }

            return candidates
                .OrderByDescending(c => c.HostScore)
                .ThenByDescending(c => c.PathLength)
                .ThenBy(c => c.Identity?.Id ?? int.MaxValue)
                .Select(c => c.Identity);
        }

        private static int GetHostMatchScore(string requestHost, string identityHost)
        {
            if (string.IsNullOrWhiteSpace(requestHost) || string.IsNullOrWhiteSpace(identityHost))
                return 0;

            var normalizedIdentity = NormalizeHost(identityHost);
            if (string.Equals(requestHost, normalizedIdentity, StringComparison.OrdinalIgnoreCase))
                return 2;

            // Legacy data uses placeholder hosts such as "_localhost" / "localhost_".
            var canonicalRequest = CanonicalizeHost(requestHost);
            var canonicalIdentity = CanonicalizeHost(normalizedIdentity);

            if (string.IsNullOrWhiteSpace(canonicalRequest) || string.IsNullOrWhiteSpace(canonicalIdentity))
                return 0;

            return string.Equals(canonicalRequest, canonicalIdentity, StringComparison.OrdinalIgnoreCase) ? 1 : 0;
        }

        private static bool PathMatchesIdentityBase(string requestPath, string identityPath)
        {
            if (identityPath == "/")
                return true;

            if (string.Equals(requestPath, identityPath, StringComparison.OrdinalIgnoreCase))
                return true;

            return requestPath.StartsWith(identityPath + "/", StringComparison.OrdinalIgnoreCase);
        }

        private static string StripIdentityBasePath(string requestPath, string identityPath)
        {
            var normalizedRequest = NormalizeIdentityPath(requestPath);
            var normalizedIdentity = NormalizeIdentityPath(identityPath);

            if (normalizedIdentity == "/")
                return normalizedRequest;

            if (string.Equals(normalizedRequest, normalizedIdentity, StringComparison.OrdinalIgnoreCase))
                return "/";

            if (normalizedRequest.StartsWith(normalizedIdentity + "/", StringComparison.OrdinalIgnoreCase))
                return normalizedRequest.Substring(normalizedIdentity.Length);

            return normalizedRequest;
        }

        private static string NormalizeHost(string host)
        {
            if (string.IsNullOrWhiteSpace(host))
                return string.Empty;

            return host.Trim().TrimEnd('.').ToLowerInvariant();
        }

        private static string CanonicalizeHost(string host)
        {
            if (string.IsNullOrWhiteSpace(host))
                return string.Empty;

            return host.Trim('_');
        }

        private static string NormalizeIdentityPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return "/";

            var normalized = path.Trim();
            if (!normalized.StartsWith("/"))
                normalized = "/" + normalized;

            normalized = normalized.TrimEnd('/');
            return normalized.Length == 0 ? "/" : normalized.ToLowerInvariant();
        }
    }
}
