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

namespace WCMS.WebSystem.WebParts.Article.Providers
{
    public class ArticleLinkProvider : GenericSqlDataProviderBase<ArticleLink>, IArticleLinkProvider
    {
        public ArticleLink Get(int objectId, int recordId, int articleId)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("ArticleLink_Get",
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@ArticleId", articleId)))
            {
                if (r.Read())
                    return this.From(r);
            }

            return null;
        }

        public IEnumerable<ArticleLink> GetList(int objectId = -2, int recordId = -2)
        {
            List<ArticleLink> items = new List<ArticleLink>();

            using (DbDataReader r = SqlHelper.ExecuteReader("ArticleLink_Get",
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId)
            ))
            {
                while (r.Read())
                    items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<ArticleLink> GetList(int articleId)
        {
            List<ArticleLink> items = new List<ArticleLink>();

            using (DbDataReader r = SqlHelper.ExecuteReader("ArticleLink_Get", 
                new SqlParameter("@ArticleId", articleId)))
            {
                while (r.Read())
                    items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<ArticleLink> GetList(int objectId = -2, int recordId = -2, int siteId = -2)
        {
            List<ArticleLink> items = new List<ArticleLink>();

            using (DbDataReader r = SqlHelper.ExecuteReader("ArticleLink_Get",
                new SqlParameter("@ObjectId", objectId),
                new SqlParameter("@RecordId", recordId),
                new SqlParameter("@SiteId", siteId)))
            {
                while (r.Read())
                    items.Add(this.From(r));
            }

            return items;
        }

        public override int Update(ArticleLink item)
        {
            object o = SqlHelper.ExecuteScalar("ArticleLink_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Rank", item.Rank),
                new SqlParameter("@Style", item.Style),
                new SqlParameter("@ObjectId", item.ObjectId),
                new SqlParameter("@RecordId", item.RecordId),
                new SqlParameter("@ArticleId", item.ArticleId),
                new SqlParameter("@SiteId", item.SiteId),
                new SqlParameter("@CommentOn", item.CommentOn)
            );

            item.Id = DataHelper.GetId(o);
            return item.Id;
        }

        public override bool Delete(int id)
        {
            SqlHelper.ExecuteNonQuery("ArticleLink_Del",
                new SqlParameter("@Id", id));

            return true;
        }

        protected override string DeleteProcedure { get { return "ArticleLink_Del"; } }
        protected override string SelectProcedure { get { return "ArticleLink_Get"; } }

        protected override ArticleLink From(IDataReader r, ArticleLink source)
        {
            ArticleLink item = source ?? new ArticleLink();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.Rank = (int)r["Rank"];
            item.Style = r["Style"].ToString();
            item.ObjectId = DataHelper.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataHelper.GetId(r, WebColumns.RecordId);
            item.ArticleId = DataHelper.GetId(r["ArticleId"]);
            item.SiteId = DataHelper.GetId(r, WebColumns.SiteId);
            item.CommentOn = DataHelper.GetInt32(r, "CommentOn");

            return item;
        }
    }
}
