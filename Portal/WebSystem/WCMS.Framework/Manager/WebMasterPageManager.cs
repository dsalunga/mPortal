using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    class WebMasterPageManager : StandardDataManager<WebMasterPage>, IWebMasterPageProvider
    {
        private IWebMasterPageProvider _provider;

        public WebMasterPageManager(IWebMasterPageProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        #region IWebMasterPageProvider Members

        public IEnumerable<WebMasterPage> GetList(int siteId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .Where(i => siteId == -1 || i.SiteId == siteId);
            }

            return _provider.GetList(siteId);
        }

        #endregion
    }
}
