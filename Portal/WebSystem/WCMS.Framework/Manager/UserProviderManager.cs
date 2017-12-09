using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WCMS.Framework.Core;
using WCMS.Framework.Security;

namespace WCMS.Framework.Manager
{
    public class UserProviderManager : StandardDataManager<UserProvider>, IUserProviderProvider
    {
        protected IUserProviderProvider _provider;

        public UserProviderManager(IUserProviderProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        public UserProvider Get(string name)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .FirstOrDefault(i => i.Name
                        .Equals(name, StringComparison.InvariantCultureIgnoreCase));
            }

            return _provider.Get(name);
        }
    }
}
