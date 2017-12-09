using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public class WebShortUrl : NamedWebObject, ISelfManager
    {
        private static IWebShortUrlProvider _manager;

        static WebShortUrl()
        {
            _manager = WebObject.ResolveManager<WebShortUrl, IWebShortUrlProvider>(WebObject.ResolveProvider<WebShortUrl, IWebShortUrlProvider>());
        }

        public WebShortUrl()
        {
            PageId = -1;
        }

        public int PageId { get; set; }
        public string PageUrl { get; set; }
        public override int OBJECT_ID { get { return -1; } }
        public static IWebShortUrlProvider Provider { get { return _manager; } }

        //public string GetRelativeUrl()
        //{
        //    return WebRewriter.BuildUrl(Page);
        //}

        public WPage Page
        {
            get
            {
                return PageId > 0 ? WPage.Get(PageId) : null;
            }
        }

        private string _url = string.Empty;
        public string EvalUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_url))
                {
                    if (!string.IsNullOrEmpty(PageUrl))
                        _url = PageUrl;
                    else if (PageId > 0)
                    {
                        var page = WPage.Get(PageId);
                        if (page != null)
                            _url = page.BuildAbsoluteUrl();
                    }
                }

                return _url;
            }
        }

        public int Update()
        {
            return _manager.Update(this);
        }

        public bool Delete()
        {
            return _manager.Delete(this.Id);
        }
    }
}
