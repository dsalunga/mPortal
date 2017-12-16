using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.WebParts.Article.Providers;

namespace WCMS.WebSystem.WebParts.Article.Managers
{
    public class ArticleListManager : StandardDataManager<ArticleList>, IArticleListProvider
    {
        protected IArticleListProvider _provider;

        public ArticleListManager(IArticleListProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        #region IArticleListProvider Members

        public ArticleList Get(int objectId, int recordId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .FirstOrDefault(i => i.ObjectId == objectId && i.RecordId == recordId);
            }

            return _provider.Get(objectId, recordId);
        }

        #endregion
    }
}
