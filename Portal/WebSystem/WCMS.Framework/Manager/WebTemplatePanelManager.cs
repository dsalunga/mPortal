using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    class WebTemplatePanelManager : StandardDataManager<WebTemplatePanel>, IWebTemplatePanelProvider
    {
        private IWebTemplatePanelProvider _provider;

        public WebTemplatePanelManager(IWebTemplatePanelProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        #region IWebTemplatePanelProvider Members

        public IEnumerable<WebTemplatePanel> GetList(int objectId, int recordId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return from i in _cache.ObjectCache.Values
                       where (recordId == -2 || i.RecordId == recordId)
                          && (objectId == -2 || i.ObjectId == objectId)
                       orderby i.Rank, i.Id
                       select i;
            }

            return from i in _provider.GetList(objectId, recordId)
                   orderby i.Rank, i.Id
                   select i;
        }

        #endregion
    }
}
