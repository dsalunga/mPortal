using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.WebParts.Article.Providers;

namespace WCMS.WebSystem.WebParts.Article
{
    public class ArticleLink : WebObjectBase
    {
        private static IArticleLinkProvider _manager;

        static ArticleLink()
        {
            _manager = WebObject.ResolveManager<ArticleLink, IArticleLinkProvider>(WebObject.ResolveProvider<ArticleLink, IArticleLinkProvider>());
        }

        public ArticleLink()
        {
            ObjectId = -1;
            RecordId = -1;
            ArticleId = -1;
            SiteId = -1;
            CommentOn = -1;

            Style = "";
        }

        [ObjectColumn]
        public int Rank { get; set; }

        [ObjectColumn]
        public string Style { get; set; }

        [ObjectColumn]
        public int ObjectId { get; set; } // Item

        [ObjectColumn]
        public int RecordId { get; set; } // Item

        [ObjectColumn]
        public int ArticleId { get; set; } // Article

        [ObjectColumn]
        public int SiteId { get; set; }

        [ObjectColumn]
        public int CommentOn { get; set; }

        public Article Article
        {
            get { return Article.Get(ArticleId); }
        }

        public int Update()
        {
            return _manager.Update(this);
        }

        public bool Delete()
        {
            return _manager.Delete(this.Id);
        }

        #region Static Methods

        public static ArticleLink Get(int objectId, int recordId, int articleId)
        {
            return _manager.Get(objectId, recordId, articleId);
        }

        public static IEnumerable<ArticleLink> GetList(int objectId, int recordId)
        {
            return _manager.GetList(objectId, recordId);
        }

        public static IEnumerable<ArticleLink> GetList(int articleId)
        {
            return _manager.GetList(articleId);
        }

        public static IEnumerable<ArticleLink> GetList(int objectId, int recordId, int siteId)
        {
            return _manager.GetList(objectId, recordId, siteId);
        }

        public static bool Delete(int id)
        {
            return _manager.Delete(id);
        }

        #endregion

        #region IWebObject Members


        public override int OBJECT_ID
        {
            get { return WebObjects.ArticleLocation; }
        }

        #endregion
    }
}
