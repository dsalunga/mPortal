using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    class WebPageElementManager : StandardDataManager<WebPageElement>, IWebPageElementProvider
    {
        protected IWebPageElementProvider _provider;

        public WebPageElementManager(IWebPageElementProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        #region IWebPageElementProvider Members

        public int GetCount(int recordId, int objectId, int templatePanelId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .Count(i =>
                        (recordId == -1 || i.RecordId == recordId) &&
                        (objectId == -1 || i.ObjectId == objectId) &&
                        (templatePanelId == -1 || i.TemplatePanelId == templatePanelId));
            }

            return _provider.GetCount(recordId, objectId, templatePanelId);
        }

        public int GetMaxRank(int recordId, int objectId, int templatePanelId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                var items = _cache.ObjectCache.Values
                    .Where(i => i.RecordId == recordId && i.ObjectId == objectId && i.TemplatePanelId == templatePanelId);

                return items.Count() > 0 ? items.Max(i => i.Rank) : 0;
            }

            return _provider.GetMaxRank(recordId, objectId, templatePanelId);
        }

        public IEnumerable<WebPageElement> GetList(int recordId, int objectId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return from i in _cache.ObjectCache.Values
                       orderby i.Rank
                       where
                           (recordId == -1 || i.RecordId == recordId) &&
                           (objectId == -1 || i.ObjectId == objectId)
                       select i;
            }

            return _provider.GetList(recordId, objectId);
        }

        public IEnumerable<WebPageElement> GetList(int recordId, int objectId, int templatePanelId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return from i in _cache.ObjectCache.Values
                       orderby i.Rank
                       where
                           (recordId == -1 || i.RecordId == recordId) &&
                           (objectId == -1 || i.ObjectId == objectId) &&
                           (templatePanelId == -1 || i.TemplatePanelId == templatePanelId)
                       select i;
            }

            return _provider.GetList(recordId, objectId, templatePanelId);
        }

        #endregion
    }
}
