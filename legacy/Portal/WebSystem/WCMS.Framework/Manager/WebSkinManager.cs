using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    public class WebSkinManager : StandardDataManager<WebSkin>, IWebSkinProvider
    {
        protected IWebSkinProvider _provider;

        public WebSkinManager(IWebSkinProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        public IEnumerable<WebSkin> GetList(int objectId, int recordId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .Where(item => (objectId == -2 || item.ObjectId == objectId)
                        && (recordId==-2 || item.RecordId == recordId));
            }
             
            return _provider.GetList(objectId, recordId);
        }
    }
}
