using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    public class WebPartControlManager : StandardDataManager<WebPartControl>, IWebPartControlProvider
    {
        protected IWebPartControlProvider _provider;

        public WebPartControlManager(IWebPartControlProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        #region IWebPartControlProvider Members

        public WebPartControl Get(int partId, string identity)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values.FirstOrDefault(item =>
                    (partId == -1 || item.PartId == partId) &&
                    (identity == null || item.Identity.Equals(identity, StringComparison.InvariantCultureIgnoreCase)));
            }

            return _provider.Get(partId, identity);
        }

        public IEnumerable<WebPartControl> GetList(int partId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return _cache.ObjectCache.Values
                    .Where(item => partId == -1 || item.PartId == partId);

            return _provider.GetList(partId);
        }

        public IEnumerable<WebPartControl> GetListByParentId(int parentId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return _cache.ObjectCache.Values
                    .Where(item => parentId == -1 || item.ParentId == parentId);

            return _provider.GetListByParentId(parentId);
        }

        #endregion
    }
}
