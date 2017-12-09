using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    public class WebPartManager : StandardDataManager<WPart>, IWebPartProvider
    {
        protected IWebPartProvider _provider;

        public WebPartManager(IWebPartProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        #region IWebPartProvider Members

        public WPart Get(string identity)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return _cache.ObjectCache.Values.FirstOrDefault(item => item.Identity == identity);

            return _provider.Get(identity);
        }

        public IEnumerable<WPart> GetList(int active = -1)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return _cache.ObjectCache.Values.Where(i => active == -1 || i.Active == active);

            return _provider.GetList(active);
        }

        #endregion
    }
}
