using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    public class WebPartControlTemplateManager : StandardDataManager<WebPartControlTemplate>, IWebPartControlTemplateProvider
    {
        protected IWebPartControlTemplateProvider _provider;

        public WebPartControlTemplateManager(IWebPartControlTemplateProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        #region IWebPartControlTemplateProvider Members

        public WebPartControlTemplate Get(int partControlId, string identity)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .FirstOrDefault(item => (partControlId == -1 || item.PartControlId == partControlId) && 
                        (identity == null || item.Identity.Equals(identity, StringComparison.InvariantCultureIgnoreCase)));
            }

            return _provider.Get(partControlId, identity);
        }

        public IEnumerable<WebPartControlTemplate> GetList(int partControlId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .Where(item => partControlId == -1 || item.PartControlId == partControlId);
            }

            return _provider.GetList(partControlId);
        }

        #endregion
    }
}
