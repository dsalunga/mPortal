using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.WebParts.Article.Providers;

namespace WCMS.WebSystem.WebParts.Article.Managers
{
    public class ArticleLinkManager : StandardDataManager<ArticleLink>, IArticleLinkProvider
    {
        protected IArticleLinkProvider _provider;

        public ArticleLinkManager(IArticleLinkProvider provider)
            : base(provider)
        {
            _provider = provider;
        }

        #region IArticleLinkProvider Members

        public ArticleLink Get(int objectId, int recordId, int articleId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .FirstOrDefault(i => i.ObjectId == objectId && i.RecordId == recordId && i.ArticleId == articleId);
            }

            return _provider.Get(objectId, recordId, articleId);
        }

        public IEnumerable<ArticleLink> GetList(int objectId = -2, int recordId = -2)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .Where(i => (objectId == -2 || i.ObjectId == objectId) && (recordId == -2 || i.RecordId == recordId));
            }

            return _provider.GetList(objectId, recordId);
        }

        public IEnumerable<ArticleLink> GetList(int articleId)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .Where(i => articleId == -2 || i.ArticleId == articleId);
            }

            return _provider.GetList(articleId);
        }

        public IEnumerable<ArticleLink> GetList(int objectId = -2, int recordId = -2, int siteId = -2)
        {
            if (_cache.CacheStatus == CacheStatus.Full)
            {
                return _cache.ObjectCache.Values
                    .Where(i =>
                        (objectId == -2 || i.ObjectId == objectId) &&
                        (recordId == -2 || i.RecordId == recordId) &&
                        (siteId == -2 || i.SiteId == siteId));
            }

            return _provider.GetList(objectId, recordId);
        }

        #endregion
    }
}
