using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    public class WebUserManager : StandardDataManager<WebUser>, IWebUserProvider
    {
        protected IWebUserProvider _provider;

        public WebUserManager(IWebUserProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        #region IWebUserProvider Members

        public WebUser Get(string userName)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .FirstOrDefault(i => i.UserName
                        .Equals(userName, StringComparison.InvariantCultureIgnoreCase));
            }

            return _provider.Get(userName);
        }

        public WebUser GetByEmailId(string emailId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                string emailIdWithAt = emailId + "@";

                return _cache.ObjectCache.Values
                    .FirstOrDefault(i => i.Email.StartsWith(emailIdWithAt, StringComparison.InvariantCultureIgnoreCase));
            }

            return _provider.GetByEmailId(emailId);
        }

        public WebUser GetByEmail(string email)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .FirstOrDefault(i => i.Email
                        .Equals(email, StringComparison.InvariantCultureIgnoreCase));
            }

            return _provider.GetByEmail(email);
        }

        public IEnumerable<WebUser> GetList(int active)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .Where(i => active == -1 || i.Status == active);
            }

            return _provider.GetList(active);
        }

        public bool Delete(string userName)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                var item = _cache.ObjectCache
                    .FirstOrDefault(i => i.Value.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));

                if (item.Value != null)
                    _cache.ObjectCache.Remove(item.Key);
            }

            return _provider.Delete(userName);
        }

        #endregion
    }
}
