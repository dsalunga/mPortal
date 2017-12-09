using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    public class WebSiteManager : StandardDataManager<WSite>, IWebSiteProvider
    {
        protected IWebSiteProvider _provider;

        public WebSiteManager(IWebSiteProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        public IEnumerable<WSite> GetList(int parentId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .Where(item => parentId == -2 || item.ParentId == parentId);
            }

            return _provider.GetList(parentId);
        }

        public WSite Get(string identity)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .FirstOrDefault(item => string.IsNullOrEmpty(identity) || item.Identity.Equals(identity, StringComparison.InvariantCultureIgnoreCase));
            }

            return _provider.Get(identity);
        }

        public int GetMaxRank()
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Max(i => i.Value.Rank);
            }

            return _provider.GetMaxRank();
        }
    }
}
