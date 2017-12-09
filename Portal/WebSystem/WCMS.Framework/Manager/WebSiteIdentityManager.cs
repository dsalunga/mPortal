using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    public class WebSiteIdentityManager : StandardDataManager<WebSiteIdentity>, IWebSiteIdentityProvider
    {
        protected IWebSiteIdentityProvider _provider;

        public WebSiteIdentityManager(IWebSiteIdentityProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        #region IWebSiteIdentityProvider Members


        public IEnumerable<WebSiteIdentity> GetList(int siteId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return _cache
                    .ObjectCache
                    .Values
                    .Where(i => siteId == -1 || i.SiteId == siteId);

            return _provider.GetList(siteId);
        }

        #endregion
    }
}
