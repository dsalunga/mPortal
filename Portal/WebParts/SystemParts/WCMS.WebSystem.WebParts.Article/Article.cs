using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.Article.Providers;

namespace WCMS.WebSystem.WebParts.Article
{
    public class Article : WebObjectBase
    {
        /// <summary>
        /// Value: ArticleId
        /// </summary>
        public const string ArticleKey = "ArticleId";
        public const string ArticleIdentity = "Article";

        /// <summary>
        /// Value: Any
        /// </summary>
        public const string ArticleRSSQueryKey = "Id";

        public const int ID = 40;

        public Article()
        {
            UserId = -1;
            SiteId = -1;
            ModifiedUserId = -1;
            DateModified = DateTime.Now;
        }

        private static IArticleProvider _manager;

        static Article()
        {
            _manager = WebObject.ResolveManager<Article, IArticleProvider>(WebObject.ResolveProvider<Article, IArticleProvider>());
        }

        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public int SiteId { get; set; }
        public int Active { get; set; }
        public int UserId { get; set; }
        public DateTime DateModified { get; set; }
        public int ModifiedUserId { get; set; }
        public string Tags { get; set; }

        public WebUser CreatedBy
        {
            get { return WebUser.Get(UserId); }
        }

        public int Update()
        {
            return _manager.Update(this);
        }

        public bool Delete()
        {
            return _manager.Delete(this.Id);
        }

        public static Article Get(int articleId)
        {
            return _manager.Get(articleId);
        }

        public static IEnumerable<Article> GetList(int siteId)
        {
            return _manager.GetList(siteId);
        }

        public static IEnumerable<Article> GetList(int objectId, int recordId)
        {
            return (from i in ArticleLink.GetList(objectId, recordId)
                    select i.Article);
        }

        public static bool Delete(int id)
        {
            // Delete all links to this item
            var links = ArticleLink.GetList(id);
            foreach (var link in links)
            {
                link.Delete();
            }

            // Delete the item
            return _manager.Delete(id);
        }

        public override int OBJECT_ID
        {
            get { return ID; }
        }
    }
}
