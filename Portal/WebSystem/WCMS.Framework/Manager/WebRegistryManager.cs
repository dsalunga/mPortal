using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    public class WebRegistryManager : StandardDataManager<WebRegistry>, IWebRegistryProvider
    {
        protected IWebRegistryProvider _provider;

        public WebRegistryManager(IWebRegistryProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        #region IWebRegistryProvider Members

        public bool Delete(string key)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                // Try remove from cache
                WebRegistry item = _cache.ObjectCache.Values.FirstOrDefault(i => i.Key == key);
                if (item != null)
                    _cache.Remove(item.Id);
            }

            return _provider.Delete(key);
        }

        public WebRegistry Get(string key)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                WebRegistry item = _cache.ObjectCache.Values
                    .FirstOrDefault(i => i.Key == key);

                if (item != null) 
                    return item;
            }

            return _provider.Get(key);
        }

        public WebRegistry Get(string key, int parentId = -2)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                WebRegistry item = _cache.ObjectCache.Values
                    .FirstOrDefault(i => 
                        (parentId == -2 || i.ParentId == parentId) && 
                        (key == null || i.Key == key));

                if (item != null) 
                    return item;
            }

            return _provider.Get(key, parentId);
        }

        public IEnumerable<WebRegistry> GetList(int parentId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return from i in _cache.ObjectCache.Values
                        where parentId == -2 || i.ParentId == parentId
                        orderby i.ParentId, i.Key
                        select i;
            }

            return _provider.GetList(parentId);
        }

        #endregion
    }
}
