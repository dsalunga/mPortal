using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;

using WCMS.Common.Utilities;

namespace WCMS.Framework
{
    /// <summary>
    /// Use this class in Admin section to manage redirection. For controls also.
    /// </summary>
    public abstract class WebRedirector
    {
        public static void RedirectToLogin(bool useCentralLogin = false)
        {
            var query = new WQuery(WSession.Context);
            var q = new WQuery();

            string queryString = query.BuildQuery();
            if (queryString != "/" && queryString != "/default.aspx")
                q.SetSource(queryString);

            q.Redirect(useCentralLogin ? CentralPages.Login : WConfig.DefaultLoginPage);
        }

        public static void ReturnFromContentMgt()
        {
            var query = new WQuery(HttpContext.Current);
            WPage page = null;
            //int masterPageItemId = DataHelper.GetDbId(qs[WebColumns.MasterPageItemId]);
            int pageElementId = query.GetId(WebColumns.PageElementId);
            int pageId = query.GetId(WebColumns.PageId);

            if (pageId > 0 || pageElementId > 0)
            {
                if (pageElementId > 0)
                {
                    WebPageElement item = WebPageElement.Get(pageElementId);
                    if (pageId > 0)
                    {
                        // MasterPageElement
                        page = WPage.Get(pageId);
                    }
                    else
                    {
                        // PageElement
                        page = item.Page;
                    }
                }
                //else if (masterPageItemId > 0)
                //{
                //    // Obsolete
                //    WebPageElement item = WebPageElement.Get(masterPageItemId);
                //    page = item.MasterPage.OwnerPage;
                //}
                else if (pageId > 0)
                {
                    page = WPage.Get(pageId);
                }

                query.Remove(WebColumns.TemplatePanelId);
                //qs.Remove(WebColumns.MasterPageItemId);
                query.Remove(WebColumns.PageId);
                query.Remove(WebColumns.PageElementId);
                query.Remove(WebColumns.MasterPageId);
                query.Remove(WebColumns.SiteId);
                query.Remove(WConstants.Load);
                query.BasePath = page.BuildRelativeUrl();

                // Redirect to current page
                query.Redirect();
            }
            else
            {
                // throw
            }
        }
    }
}
