using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    public class WebObjectHeaderManager : StandardDataManager<WebObjectHeader>, IWebObjectHeaderProvider
    {
        protected IWebObjectHeaderProvider _provider;

        public WebObjectHeaderManager(IWebObjectHeaderProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        #region IWebObjectHeaderProvider Members

        public WebObjectHeader Get(int objectId, int recordId, int textResourceId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .FirstOrDefault(i =>
                        (objectId == -1 || i.ObjectId == objectId) &&
                        (recordId == -1 || i.RecordId == recordId) &&
                        (textResourceId == -1 || i.TextResourceId == textResourceId));
            }

            return _provider.Get(objectId, recordId, textResourceId);
        }

        public IEnumerable<WebObjectHeader> GetList(int objectId, int recordId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .Where(i =>
                        (objectId == -1 || i.ObjectId == objectId) &&
                        (recordId == -1 || i.RecordId == recordId));
                //.ToList();
            }

            return _provider.GetList(objectId, recordId);
        }

        public IEnumerable<WebObjectHeader> GetList(int textResourceId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return _cache.ObjectCache.Values
                    .Where(i =>
                    (textResourceId == -1 || i.TextResourceId == textResourceId));

            return _provider.GetList(textResourceId);
        }

        #endregion
    }
}
