using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.Article.Providers;

namespace WCMS.WebSystem.WebParts.Article
{
    public class ArticleTemplate : NamedWebObject
    {
        private static IArticleTemplateProvider _manager;

        static ArticleTemplate()
        {
            _manager = WebObject.ResolveManager<ArticleTemplate, IArticleTemplateProvider>(WebObject.ResolveProvider<ArticleTemplate, IArticleTemplateProvider>());
        }

        public ArticleTemplate()
        {
            File = "";
            DateFormat = string.Empty;
        }

        [ObjectColumn]
        public DateTime Date { get; set; }

        [ObjectColumn]
        public string File { get; set; }

        [ObjectColumn]
        public string ImageUrl { get; set; }

        [ObjectColumn]
        public string ListItemTemplate { get; set; }

        [ObjectColumn]
        public string DetailsTemplate { get; set; }

        [ObjectColumn]
        public string ListTemplate { get; set; }

        [ObjectColumn]
        public string DateFormat { get; set; }

        public static ArticleTemplate Get(int id)
        {
            return _manager.Get(id);
        }

        public static IEnumerable<ArticleTemplate> GetList()
        {
            return _manager.GetList();
        }

        public int Update()
        {
            return _manager.Update(this);
        }

        public bool Delete()
        {
            return _manager.Delete(this.Id);
        }

        public static bool Delete(int id)
        {
            return _manager.Delete(id);
        }

        #region IWebObject Members


        public override int OBJECT_ID
        {
            get { return 44; }
        }

        #endregion
    }
}
