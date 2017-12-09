using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    public class WebSubscriptionManager : StandardDataManager<WebSubscription>, IWebSubscriptionProvider
    {
        protected IWebSubscriptionProvider _provider;

        public WebSubscriptionManager(IWebSubscriptionProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        public IEnumerable<WebSubscription> GetList(int objectId, int recordId, int partId, int pageId, int allow)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .Where(i => (objectId == -1 || i.ObjectId == objectId)
                        && (recordId == -1 || i.RecordId == recordId)
                        && (partId == -1 || i.PartId == partId)
                        && (pageId == -1 || i.PageId == pageId)
                        && (allow == -1 || i.Allow == allow)
                );
            }

            return _provider.GetList(objectId, recordId, partId, pageId, allow);
        }
    }
}
