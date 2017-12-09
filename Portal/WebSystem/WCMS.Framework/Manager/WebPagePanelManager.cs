using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    class WebPagePanelManager : StandardDataManager<WebPagePanel>, IWebPagePanelProvider
    {
        private IWebPagePanelProvider _provider;

        public WebPagePanelManager(IWebPagePanelProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        #region IWebPagePanelProvider Members

        public IEnumerable<WebPagePanel> GetList(int pageId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .Where(i => pageId == -1 || i.PageId == pageId);
            }

            return _provider.GetList(pageId);
        }

        public WebPagePanel Get(int templatePanelId, int pageId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .FirstOrDefault(i =>
                        (templatePanelId == -1 || i.TemplatePanelId == templatePanelId) &&
                        (pageId == -1 || i.PageId == pageId));
            }

            return _provider.Get(templatePanelId, pageId);
        }

        #endregion
    }
}
