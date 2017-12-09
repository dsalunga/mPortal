using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Manager
{
    public class WebShareManager : StandardDataManager<WebShare>, IWebShareProvider
    {
        protected IWebShareProvider _provider;

        public WebShareManager(IWebShareProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        public IEnumerable<WebShare> GetList(int objectId, int recordId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return _cache.ObjectCache.Values
                    .Where(i => (objectId == -2 || i.ObjectId == objectId) && (recordId == 2 || i.RecordId == objectId));

            return _provider.GetList(objectId, recordId);
        }


        public WebShare Get(int objectId, int recordId, int shareObjectId, int shareRecordId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return _cache.ObjectCache.Values
                    .FirstOrDefault(i => i.ObjectId == objectId && i.RecordId == recordId
                        && i.ShareObjectId == shareObjectId && i.ShareRecordId == shareRecordId);

            return _provider.Get(objectId, recordId, shareObjectId, shareRecordId);
        }
    }
}
