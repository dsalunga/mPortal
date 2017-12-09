using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core.Manager
{
    public class WebPageManager : StandardDataManager<WPage>, IWebPageProvider
    {
        protected IWebPageProvider _provider;

        public WebPageManager(IWebPageProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        public IEnumerable<WPage> GetList(int siteId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return from i in _cache.ObjectCache.Values
                       where i.SiteId == siteId
                       orderby i.ParentId, i.Rank
                       select i;

            return _provider.GetList(siteId);
        }

        public IEnumerable<WPage> GetList(int siteId, int parentId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return from i in _cache.ObjectCache.Values
                       where
                           (siteId == -1 || i.SiteId == siteId) &&
                           (parentId == -2 || i.ParentId == parentId)
                       orderby i.ParentId, i.Rank
                       select i;

            return _provider.GetList(siteId, parentId);
        }

        public int GetCount(int siteId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return _cache.ObjectCache.Values
                    .Count(item => item.SiteId == siteId);

            return _provider.GetCount(siteId);
        }

        public int GetMaxRank(int siteId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return _cache.ObjectCache.Max(i => i.Value.Rank);

            return _provider.GetMaxRank(siteId);
        }

        public WPage Get(int siteId, int parentId, string identity)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return _cache.ObjectCache.Values
                    .FirstOrDefault(i => i.SiteId == siteId && i.ParentId == parentId && i.Identity.Equals(identity, StringComparison.InvariantCultureIgnoreCase));

            return _provider.Get(siteId, parentId, identity);
        }
    }
}
