using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    public class WebObjectSecurityManager : StandardDataManager<WebObjectSecurity>, IWebObjectSecurityProvider
    {
        protected IWebObjectSecurityProvider _provider;

        public WebObjectSecurityManager(IWebObjectSecurityProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        #region IWebObjectSecurityProvider Members

        public WebObjectSecurity Get(int objectId, int recordId, int securityObjectId, int securityRecordId, int public2)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values.FirstOrDefault(i =>
                    i.ObjectId == objectId &&
                    i.RecordId == recordId &&
                    i.SecurityObjectId == securityObjectId &&
                    i.SecurityRecordId == securityRecordId &&
                    (public2 == -1 || i.Public == public2));
            }

            return _provider.Get(objectId, recordId, securityObjectId, securityRecordId, public2);
        }

        public IEnumerable<WebObjectSecurity> GetList(int objectId, int recordId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values.Where(i =>
                    (objectId == -1 || i.ObjectId == objectId) &&
                    (recordId == -1 || i.RecordId == recordId));
            }

            return _provider.GetList(objectId, recordId);
        }

        public IEnumerable<WebObjectSecurity> GetList(int objectId, int recordId, int securityObjectId, int securityRecordId,
            int public2)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values.Where(i =>
                    (objectId == -1 || i.ObjectId == objectId) &&
                    (recordId == -1 || i.RecordId == recordId) &&
                    (securityObjectId == -1 || i.SecurityObjectId == securityObjectId) &&
                    (securityRecordId == -1 || i.SecurityRecordId == securityRecordId) &&
                    (public2 == -1 || i.Public == public2));
            }

            return _provider.GetList(objectId, recordId, securityObjectId, securityRecordId, public2);
        }

        #endregion
    }
}
