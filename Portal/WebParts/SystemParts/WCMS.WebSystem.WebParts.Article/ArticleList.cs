using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.Article.Providers;

namespace WCMS.WebSystem.WebParts.Article
{
    /// <summary>
    /// Configuration of an Article List
    /// </summary>
    public class ArticleList : WebObjectBase, ISelfManager
    {
        private const int ID = 42;
        private static IArticleListProvider _manager;

        static ArticleList()
        {
            _manager = WebObject.ResolveManager<ArticleList, IArticleListProvider>(WebObject.ResolveProvider<ArticleList, IArticleListProvider>());
        }

        public ArticleList()
        {
            ObjectId = -1;
            RecordId = -1;
            TemplateId = -1;
            SiteId = -1;
            FolderId = -1;
            CommentOn = -1;
        }

        #region Properties

        public int PageSize { get; set; }
        //public string DateFormatString { get; set; }
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int TemplateId { get; set; }
        public int FolderId { get; set; }
        public int SiteId { get; set; }
        public int CommentOn { get; set; }

        public ArticleTemplate Template
        {
            get
            {
                if (TemplateId > 0)
                    return ArticleTemplate.Get(TemplateId);

                return null;
            }
        }

        public IEnumerable<Article> Articles
        {
            get
            {
                if (FolderId > 0)
                {
                    var files = WebFile.Provider.GetList(FolderId);

                    return
                            from file in files
                            where file.ObjectId == Article.ID
                            select Article.Get(file.RecordId);
                }

                return from i in ArticleLink.GetList(ObjectId, RecordId)
                       select i.Article;
            }
        }

        public WebFolder Folder
        {
            get
            {
                if (FolderId > 0)
                    return WebFolder.Provider.Get(FolderId);

                return null;
            }
        }

        #endregion

        public Article GetArticle(int id)
        {
            if (FolderId > 0)
            {
                var file = WebFile.Provider.Get(FolderId, Article.ID, id);

                if (file != null)
                    return Article.Get(id);
            }
            else
            {
                var link = ArticleLink.Get(ObjectId, RecordId, id);
                if (link != null)
                    return link.Article;
            }

            return null;
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

        public static ArticleList Get(int objectId, int recordId)
        {
            return _manager.Get(objectId, recordId);
        }

        public static bool Delete(int id)
        {
            return _manager.Delete(id);
        }

        #endregion

        #region IWebObject Members


        public override int OBJECT_ID
        {
            get { return ID; }
        }

        #endregion
    }
}
