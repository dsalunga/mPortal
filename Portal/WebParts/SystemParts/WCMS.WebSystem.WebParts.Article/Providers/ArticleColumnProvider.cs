using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using WCMS.Framework;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;
using WCMS.Framework.Core.Interfaces;

namespace WCMS.WebSystem.WebParts.Article.Providers
{
    public class ArticleColumnProvider
    {
        private static IDataProvider _provider;

        static ArticleColumnProvider()
        {
            _provider = WebObject.ResolveProvider(typeof(ArticleColumn));
        }

        public ArticleColumn Get(int id)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("ArticleColumn_Get",
                new SqlParameter("@ColumnId", id)
            ))
            {
                if (r.Read())
                {
                    return this.From(r);
                }
            }

            return null;
        }

        public ArticleColumn Get(string name, int templateId, int isSingle)
        {
            return _provider.Get<ArticleColumn>(
                new QueryFilterElement("Name", name),
                new QueryFilterElement("TemplateId", templateId),
                new QueryFilterElement("IsSingle", isSingle)
            );
        }

        public int Update(ArticleColumn item)
        {
            return _provider.Update<ArticleColumn>(item);
        }

        public List<ArticleColumn> GetList(int templateId, int isSingle)
        {
            return _provider.GetList<ArticleColumn>(
                new QueryFilterElement("TemplateId", templateId),
                new QueryFilterElement("IsSingle", isSingle)
            );
        }

        public List<ArticleColumn> GetList(int templateId)
        {
            List<ArticleColumn> items = new List<ArticleColumn>();

            using (DbDataReader r = SqlHelper.ExecuteReader("ArticleColumn_Get",
                new SqlParameter("@TemplateId", templateId)
            ))
            {
                while(r.Read())
                {
                    items.Add(this.From(r));
                }
            }

            return items;
        }

        private ArticleColumn From(DbDataReader r)
        {
            ArticleColumn item = new ArticleColumn();
            item.ColumnId = DataHelper.GetId(r, "ColumnId");
            item.Name = r["Name"].ToString();
            item.TemplateId = DataHelper.GetId(r["TemplateId"]);
            item.Id = DataHelper.GetId(r["Id"]);
            item.IsSingle = (int)r["IsSingle"];

            return item;
        }

        public bool Delete(int id)
        {
            return _provider.Delete<ArticleColumn>(id);
        }
    }
}
