using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    public class WebGroupManager : StandardDataManager<WebGroup>, IWebGroupProvider
    {
        protected IWebGroupProvider _provider;

        public WebGroupManager(IWebGroupProvider provider)
            : base(provider)
        {
            _provider = provider;
        }


        #region IWebGroupProvider Members

        public WebGroup Get(string name)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return _cache.ObjectCache.Values.FirstOrDefault(i => i.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

            return _provider.Get(name);
        }

        public WebGroup Get(int parentId, string name)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .FirstOrDefault(i =>
                        (parentId == -2 || i.ParentId == parentId) && i.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            }

            return _provider.Get(parentId, name);
        }

        public IEnumerable<WebGroup> GetList(int parentId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values.Where(i => i.ParentId == parentId)
                    .OrderBy(i => i.ParentId)
                    .ThenBy(i => i.Name);
            }

            return _provider.GetList(parentId);
        }

        #endregion
    }
}
