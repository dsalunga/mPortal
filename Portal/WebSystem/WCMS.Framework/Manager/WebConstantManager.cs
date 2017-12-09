using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Manager
{
    public class WebConstantManager : StandardDataManager<WebConstant>, IWebConstantProvider
    {
        protected IWebConstantProvider _provider;

        public WebConstantManager(IWebConstantProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        public IEnumerable<WebConstant> GetList(string category)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return _cache.ObjectCache.Values
                    .Where(i => i.Category.Equals(category, StringComparison.InvariantCultureIgnoreCase));

            return _provider.GetList(category);
        }

        public IEnumerable<WebConstant> GetList(int objectId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return _cache.ObjectCache.Values
                    .Where(i => objectId == -2 || i.ObjectId == objectId);

            return _provider.GetList(objectId);
        }
    }
}
