using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WCMS.Framework.Core;
using WCMS.WebSystem.WebParts.Article.Providers;

namespace WCMS.WebSystem.WebParts.Article
{
    /// <summary>
    /// Article Column
    /// </summary>
    public class ArticleColumn
    {
        private static ArticleColumnProvider _provider;

        static ArticleColumn()
        {
            _provider = new ArticleColumnProvider();
        }

        public ArticleColumn()
        {
            ColumnId = -1;
            TemplateId = -1;
            Id = -1;
            IsSingle = 0;
        }

        [ObjectColumn(true)]
        public int ColumnId { get; set; }

        [ObjectColumn]
        public string Name { get; set; }

        [ObjectColumn]
        public int TemplateId { get; set; }

        [ObjectColumn]
        public int Id { get; set; }

        [ObjectColumn]
        public int IsSingle { get; set; }

        public int Update()
        {
            return _provider.Update(this);
        }

        public static ArticleColumn Get(int id)
        {
            return _provider.Get(id);
        }

        public static ArticleColumn Get(string name, int templateId, int isSingle)
        {
            return _provider.Get(name, templateId, isSingle);
        }

        public static List<ArticleColumn> GetList(int templateId, int isSingle)
        {
            return _provider.GetList(templateId, isSingle);
        }

        public static List<ArticleColumn> GetList(int templateId)
        {
            return _provider.GetList(templateId);
        }

        public static bool Delete(int id)
        {
            return _provider.Delete(id);
        }
    }
}
