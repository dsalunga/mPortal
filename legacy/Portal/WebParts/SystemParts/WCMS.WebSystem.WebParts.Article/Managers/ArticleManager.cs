using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.WebParts.Article.Providers;

namespace WCMS.WebSystem.WebParts.Article.Managers
{
    public class ArticleManager : StandardDataManager<Article>, IArticleProvider
    {
        protected IArticleProvider _provider;

        public ArticleManager(IArticleProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        #region IArticleProvider Members

        public IEnumerable<Article> GetList(int siteId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .Where(i => siteId == -2 || i.SiteId == siteId);
            }

            return _provider.GetList(siteId);
        }

        #endregion
    }
}
