using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.Manager
{
    class WebTemplateManager : StandardDataManager<WebTemplate>, IWebTemplateProvider
    {
        private IWebTemplateProvider _provider;

        public WebTemplateManager(IWebTemplateProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        public IEnumerable<WebTemplate> GetList(int themeId = -2)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
                return _cache
                    .ObjectCache
                    .Values
                    .Where(i => themeId == -2 || i.ThemeId == themeId);

            return _provider.GetList(themeId);
        }
    }
}
