using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.Menu.Providers;

namespace WCMS.WebSystem.WebParts.Menu.Managers
{
    public class MenuObjectManager : StandardDataManager<MenuObject>, IMenuObjectProvider
    {
        protected IMenuObjectProvider _provider;

        public MenuObjectManager(IMenuObjectProvider provider)
            : base(provider)
        {
            _provider = provider;
        }


        public MenuObject Get(int objectId, int recordId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .FirstOrDefault(i => i.ObjectId == objectId && i.RecordId == recordId);
            }

            return _provider.Get(objectId, recordId);
        }
    }
}
