using System;
using System.Collections.Generic;
using WCMS.Framework.Core;

namespace WCMS.Framework.Social.Providers
{
    public interface IWallUpdateProvider : IDataProvider<WallUpdate>
    {
        IEnumerable<WallUpdate> GetList(int updateObjectId = -2, int updateRecordId = -2, int byObjectId = -2, int byRecordId = -2,
            int eventTypeId = -2, DateTime? updateDateStart = null, DateTime? updateDateEnd = null);
    }
}
