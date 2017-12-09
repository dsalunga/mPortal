using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    public class WebObjectSecurityPermissionManager : StandardDataManager<WebObjectSecurityPermission>, IWebObjectSecurityPermissionProvider
    {
        protected IWebObjectSecurityPermissionProvider _provider;

        public WebObjectSecurityPermissionManager(IWebObjectSecurityPermissionProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        #region IWebObjectSecurityPermissionProvider Members

        public IEnumerable<WebObjectSecurityPermission> GetList(int objectSecurityId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return _cache.ObjectCache.Values
                    .Where(i => objectSecurityId == -1 || i.ObjectSecurityId == objectSecurityId);

            return _provider.GetList(objectSecurityId);
        }

        #endregion
    }
}
