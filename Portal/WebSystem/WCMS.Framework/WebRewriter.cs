using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public class WebRewriter
    {
        /// <summary>
        /// Builds only the parts related to a page, leaves out all related to site
        /// </summary>
        /// <param name="page"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string BuildUrl(WPage page, HttpContext context = null)
        {
            if (page != null)
            {
                //bool siteIsDefault = false;
                //int defaultSiteId = -1;

                //context = context ?? HttpContext.Current;
                //if (context != null)
                //{
                //    string hostName = context.Request.ServerVariables["SERVER_NAME"];
                //    int port = DataHelper.GetInt32(context.Request.ServerVariables["SERVER_PORT"], 80);

                //    defaultSiteId = WebSiteIdentity.GetDefaultSite(hostName, port);
                //    siteIsDefault = defaultSiteId == page.SiteId;
                //}

                var site = page.Site;
                if (/*siteIsDefault &&*/ page.Id == site.HomePageId)
                    return "/";

                var builder = new StringBuilder();
                //if (page.Id != site.HomePageId)
                //{
                builder.AppendFormat("/{0}", page.Identity);
                var parentPage = page.Parent;
                do
                {
                    if (parentPage != null)
                        builder.Insert(0, "/" + parentPage.Identity);
                }
                while (parentPage != null && (parentPage = parentPage.Parent) != null);
                //}

                //if (!siteIsDefault)
                //{
                //    builder.Insert(0, "/" + site.Identity);
                //    // Build parent site heirarchy path
                //    var parentSite = site.Parent;
                //    do
                //    {
                //        if (parentSite != null)
                //        {
                //            if (parentSite.ParentId == -1 && parentSite.Id == defaultSiteId)
                //                break;
                //            else
                //                builder.Insert(0, "/" + parentSite.Identity);
                //        }
                //    }
                //    while (parentSite != null && (parentSite = parentSite.Parent) != null);
                //}
                return builder.Append(WConfig.EvalPageExt).ToString();
            }
            return null;
        }

        public static string BuildUrl(WSite site, bool absolute = false, /*bool includeHomePage = false,*/ HttpContext context = null)
        {
            if (site != null)
            {
                if (absolute)
                {
                    string baseAddress = site.BaseAddress;
                    if (!string.IsNullOrEmpty(baseAddress))
                        return baseAddress;

                    var identity = site.GetPrimaryIdentity();
                    if (identity != null)
                        return identity.Build(true);

                    // else throw an error or log it?
                    // Build parent site heirarchy path
                    var defaultSiteId = WConfig.DefaultSite.Id;
                    var builder = new StringBuilder();
                    var parentSite = site;
                    do
                    {
                        if (parentSite.Id != defaultSiteId)
                        {
                            string bAddress = parentSite.BaseAddress;
                            if (!string.IsNullOrEmpty(bAddress))
                                return builder.Insert(0, bAddress).ToString();

                            var identity2 = parentSite.GetPrimaryIdentity();
                            if (identity2 != null)
                                return builder.Insert(0, identity2.Build(true)).ToString();

                            builder.Insert(0, "/" + parentSite.Identity);
                        }
                    }
                    while ((parentSite = parentSite.Parent) != null);
                    return builder.Length == 0 ? WConfig.BaseAddress : WebHelper.CombineAddress(WConfig.BaseAddress, builder.ToString());
                }
                else
                {

                    int defaultSiteId = -1;
                    context = context ?? HttpContext.Current;
                    if (context != null)
                    {
                        string hostName = context.Request.ServerVariables["SERVER_NAME"];
                        int port = DataHelper.GetInt32(context.Request.ServerVariables["SERVER_PORT"], 80);

                        defaultSiteId = WebSiteIdentity.GetDefaultSite(hostName, port);
                        if (defaultSiteId == site.Id)
                            return "/";
                    }

                    string baseAddress = site.BaseAddress;
                    if (!string.IsNullOrEmpty(baseAddress))
                        return baseAddress;

                    var identity = site.GetPrimaryIdentity();
                    if (identity != null)
                        return identity.Build(true);

                    // If it has a homepage then return its url
                    //if (includeHomePage)
                    //{
                    //    var home = site.HomePage;
                    //    if (home != null)
                    //        return site.HomePage.BuildRelativeUrl();

                    //    // Means, no homepage...
                    //    builder.AppendFormat("/{0}{1}", site.Identity, WConfig.EvalPageExt);
                    //}
                    //else
                    //{
                    //    // Means, no homepage...
                    //    builder.AppendFormat("/{0}", site.Identity);
                    //}

                    // else throw an error or log it?
                    // Build parent site heirarchy path
                    var builder = new StringBuilder();
                    var parentSite = site;
                    do
                    {
                        if (parentSite.Id != defaultSiteId)
                            builder.Insert(0, "/" + parentSite.Identity);
                    }
                    while ((parentSite = parentSite.Parent) != null);
                    return builder.ToString();
                }
            }

            return string.Empty;
        }

        public static string GetBasePage(string rawUrl)
        {
            int rawEndIndex = rawUrl.IndexOf('?');
            return rawEndIndex > 0 ? rawUrl.Substring(0, rawEndIndex) : rawUrl;
        }

        public static WPage ResolvePage(string urlPath)
        {
            var context = HttpContext.Current;
            return ResolvePageLowered(context, urlPath.ToLower()).Item1;
        }

        public static WPage ResolvePage(HttpContext context, string urlPath)
        {
            return ResolvePageLowered(context, urlPath.ToLower()).Item1;
        }

        public static string BuildPreUrl(WPage page, bool asParent = false)
        {
            var url = page.BuildRelativeUrl();

            if (asParent)
                return url.Remove(url.LastIndexOf(WConfig.EvalPageExt)) + "/";
            else
            {
                var lastIndex = url.LastIndexOf(page.Identity);
                return lastIndex >= 0 ? url.Substring(0, lastIndex) : url;
            }
        }

        public static string BuildPreUrl(WSite site, bool asParent = false)
        {
            var url = BuildUrl(site);

            if (!string.IsNullOrEmpty(url))
                if (!asParent)
                    return url.Substring(0, url.LastIndexOf(site.Identity));

            return url;
        }

        public static Tuple<WPage, string> ResolvePageLowered(HttpContext context, string loweredPath)
        {
            if (string.IsNullOrEmpty(loweredPath))
                return new Tuple<WPage, string>(null, null);

            string hostName = context.Request.ServerVariables["SERVER_NAME"];
            WSite site = null;

            if (loweredPath.Equals("/"))
            {
                var identities = WebSiteIdentity.Provider.GetList();
                var foundIdentities = identities
                    .Where(i => i.HostName.Equals(hostName, StringComparison.InvariantCultureIgnoreCase));
                var count = foundIdentities.Count();
                if (count == 1)
                {
                    var id = foundIdentities.First();
                    if (id.RedirectUrl != string.Empty)
                        return new Tuple<WPage, string>(null, id.RedirectUrl);
                    else
                        site = id.Site;
                }
                else if (count > 1)
                    site = foundIdentities.First().Site;
                else
                    site = WConfig.DefaultSite;

                if (site != null)
                    return new Tuple<WPage, string>(site.HomePage, null);

                return new Tuple<WPage, string>(null, null);
            }

            string ext = VirtualPathUtility.GetExtension(loweredPath);
            string[] urls = (ext.StartsWith(".") ? loweredPath.Replace(ext, "/") : loweredPath).Trim('/').Split('/');
            if (urls.Count() == 1)
            {
                var shortUrl = WebShortUrl.Provider.Get(urls.First());
                if (shortUrl != null)
                    return new Tuple<WPage, string>(shortUrl.Page, shortUrl.PageUrl);
            }

            var urlQ = new Queue<string>();
            var sites = WSite.GetList();
            IEnumerable<WSite> searchSites = new List<WSite>();

            // After splitting the url, insert each items into a Q
            foreach (string url in urls)
                urlQ.Enqueue(url);

            // Search by Identity first
            var urlPeek = urlQ.Peek().ToLower();


            if (urlPeek.Equals("default"))
                urlQ.Dequeue();
            else
                searchSites = sites.Where(i => i.Identity.ToLower().Equals(urlPeek));

            if (searchSites.Count() == 0)
            {
                var identities = WebSiteIdentity.Provider.GetList();
                var foundIdentities = identities
                    .Where(i => i.HostName.Equals(hostName, StringComparison.InvariantCultureIgnoreCase));

                if (foundIdentities.Count() > 0)
                    site = foundIdentities.First().Site;
            }
            else
            {
                urlQ.Dequeue();
                site = searchSites.First();
            }

            //if (searchSites.Count == 0)
            //{
            //    // No sites found by Host Name, then search by Identity /FirstItem = SiteName
            //    string firstUrl = urlQ.Dequeue().ToLower();
            //    searchSites = sites.FindAll(site => !site.HostName.ToLower().Equals(hostName) &&
            //        site.Identity.ToLower().Equals(firstUrl));
            //}
            //else if (searchSites.Count > 1)
            //{
            //    // [Non-ideal, further evaluation] Multiple sites found, then search by Identity
            //    string firstUrl = urlQ.Dequeue().ToLower();
            //    searchSites = searchSites.FindAll(site => site.HostName.ToLower().Equals(hostName) &&
            //        site.Identity.ToLower().Equals(firstUrl));
            //}

            WPage page = null;
            if (site != null)
            {
                //var site = searchSites.First();
                if (urlQ.Count > 0)
                {
                    // Search for subsite
                    WSite subSite = null;
                    do
                    {
                        urlPeek = urlQ.Peek().ToLower();
                        subSite = site.Children.FirstOrDefault(item => item.ParentId == site.Id && item.Identity.ToLower() == urlPeek);
                        if (subSite != null)
                        {
                            site = subSite;
                            urlQ.Dequeue();
                        }
                    }
                    while (subSite != null && urlQ.Count > 0);
                }

                // Site is found, now search for the Page
                int parentPageId = -1;
                var pages = site.Pages;
                if (urlQ.Count > 0 && !(urlQ.Count == 1 && parentPageId > 0 && urlQ.Peek().Equals("default")))
                {
                    while (urlQ.Count > 0)
                    {
                        string nextUrl = urlQ.Dequeue();
                        page = pages.FirstOrDefault(webPage => webPage.ParentId == parentPageId && webPage.Identity.ToLower().Equals(nextUrl));
                        if (page != null)
                            parentPageId = page.Id;
                        else
                            break;
                    }
                }
                else
                {
                    // No Page can be extracted from the URL, load the Home Page
                    page = site.HomePage;
                }
            }

            return new Tuple<WPage, string>(page, null);
        }
    }
}
