using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework
{
    class WebShortUrlManager : StandardDataManager<WebShortUrl>, IWebShortUrlProvider
    {
        private IWebShortUrlProvider _provider;

        public WebShortUrlManager(IWebShortUrlProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        public WebShortUrl Get(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                if (_cache.CacheStatus == CacheStatus.Full)
                    return _cache.ObjectCache.Values
                        .FirstOrDefault(i => i.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

                return _provider.Get(name);
            }

            return null;
        }

        public WebShortUrl GetByPageId(int pageId)
        {
            if (pageId > 0)
            {
                if (_cache.CacheStatus == CacheStatus.Full)
                    return _cache.ObjectCache.Values.FirstOrDefault(i => i.PageId == pageId);

                return _provider.GetByPageId(pageId);
            }

            return null;
        }
    }
}
