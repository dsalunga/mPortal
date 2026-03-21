using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Content.Providers
{
    public class WebObjectContentManager : StandardDataManager<WebObjectContent>, IWebObjectContentProvider
    {
        protected IWebObjectContentProvider _provider;

        public WebObjectContentManager(IWebObjectContentProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        #region IWebObjectContentProvider Members

        public WebObjectContent GetByObjectId(int objectId, int recordId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values.FirstOrDefault(i =>
                        (objectId == -1 || i.ObjectId == objectId) &&
                        (recordId == -1 || i.RecordId == recordId));
            }

            return _provider.GetByObjectId(objectId, recordId);
        }

        public IEnumerable<WebObjectContent> GetList(int objectId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .Where(i => objectId == -1 || i.ObjectId == objectId);
            }

            return _provider.GetList(objectId);
        }

        #endregion
    }
}
