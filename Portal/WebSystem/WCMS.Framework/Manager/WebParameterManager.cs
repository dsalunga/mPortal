using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    public class WebParameterManager : StandardDataManager<WebParameter>, IWebParameterProvider
    {
        protected IWebParameterProvider _provider;

        public WebParameterManager(IWebParameterProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        #region IWebParameterProvider Members

        public IEnumerable<WebParameter> GetList(int objectId, int recordId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return _cache.ObjectCache.Values
                    .Where(i => (objectId == -2 || i.ObjectId == objectId)
                        && (recordId == -2 || i.RecordId == recordId));

            return _provider.GetList(objectId, recordId);
        }

        public WebParameter Get(int objectId, int recordId, string name)
        {
            if(_cache.CacheStatus == CacheStatus.Full)
                return _cache.ObjectCache.Values
                    .FirstOrDefault(i => (objectId == -2 || i.ObjectId == objectId)
                        && (recordId == -2 || i.RecordId == recordId)
                        && (i.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)));

            return _provider.Get(objectId, recordId, name);
        }

        #endregion
    }
}
