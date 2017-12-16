using System;
using System.Collections.Generic;
using System.Linq;
using WCMS.Framework.Core;

namespace WCMS.Framework.Social.Providers
{
    public class WallUpdateManager : StandardDataManager<WallUpdate>, IWallUpdateProvider
    {
        protected IWallUpdateProvider _provider;

        public WallUpdateManager(IWallUpdateProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        public IEnumerable<WallUpdate> GetList(int updateObjectId = -2, int updateRecordId = -2, int byObjectId = -2, int byRecordId = -2,
            int eventTypeId = -2, DateTime? updateDateStart = null, DateTime? updateDateEnd = null)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return from i in _cache.ObjectCache.Values
                       where (updateObjectId == -2 || i.UpdateObjectId == updateObjectId)
                          && (updateRecordId == -2 || i.UpdateRecordId == updateRecordId)
                          && (byObjectId == -2 || i.ByObjectId == byObjectId)
                          && (byRecordId == -2 || i.ByRecordId == byRecordId)
                          && (eventTypeId == -2 || i.EventTypeId == eventTypeId)
                          && (updateDateStart == null || i.UpdateDate >= updateDateStart)
                          && (updateDateEnd == null || i.UpdateDate <= updateDateEnd)
                       select i;

            return _provider.GetList(updateObjectId, updateRecordId, byObjectId, byRecordId, eventTypeId, updateDateStart, updateDateEnd);
        }
    }
}
