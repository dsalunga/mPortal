using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.Common;

using WCMS.Framework;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Article.Providers
{
    public class ArticleLinkProvider : GenericSqlDataProviderBase<ArticleLink>, IArticleLinkProvider
    {
        public ArticleLink Get(int objectId, int recordId, int articleId)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("ArticleLink") +
                " WHERE " + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId" +
                " AND " + DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId" +
                " AND " + DbSyntax.QuoteIdentifier("ArticleId") + " = @ArticleId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId),
                DbHelper.CreateParameter("@ArticleId", articleId)))
            {
                if (r.Read())
                    return this.From(r);
            }

            return null;
        }

        public IEnumerable<ArticleLink> GetList(int objectId = -2, int recordId = -2)
        {
            List<ArticleLink> items = new List<ArticleLink>();

            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("ArticleLink");
            var parms = new List<DbParameter>();
            var conditions = new List<string>();
            if (objectId != -2)
            {
                conditions.Add(DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId");
                parms.Add(DbHelper.CreateParameter("@ObjectId", objectId));
            }
            if (recordId != -2)
            {
                conditions.Add(DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId");
                parms.Add(DbHelper.CreateParameter("@RecordId", recordId));
            }
            if (conditions.Count > 0)
                sql += " WHERE " + string.Join(" AND ", conditions);

            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql, parms.ToArray()))
            {
                while (r.Read())
                    items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<ArticleLink> GetList(int articleId)
        {
            List<ArticleLink> items = new List<ArticleLink>();

            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("ArticleLink") +
                " WHERE " + DbSyntax.QuoteIdentifier("ArticleId") + " = @ArticleId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ArticleId", articleId)))
            {
                while (r.Read())
                    items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<ArticleLink> GetList(int objectId = -2, int recordId = -2, int siteId = -2)
        {
            List<ArticleLink> items = new List<ArticleLink>();

            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("ArticleLink");
            var parms = new List<DbParameter>();
            var conditions = new List<string>();
            if (objectId != -2)
            {
                conditions.Add(DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId");
                parms.Add(DbHelper.CreateParameter("@ObjectId", objectId));
            }
            if (recordId != -2)
            {
                conditions.Add(DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId");
                parms.Add(DbHelper.CreateParameter("@RecordId", recordId));
            }
            if (siteId != -2)
            {
                conditions.Add(DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId");
                parms.Add(DbHelper.CreateParameter("@SiteId", siteId));
            }
            if (conditions.Count > 0)
                sql += " WHERE " + string.Join(" AND ", conditions);

            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql, parms.ToArray()))
            {
                while (r.Read())
                    items.Add(this.From(r));
            }

            return items;
        }

        public override int Update(ArticleLink item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("ArticleLink") + " SET " +
                    DbSyntax.QuoteIdentifier("Rank") + " = @Rank, " +
                    DbSyntax.QuoteIdentifier("Style") + " = @Style, " +
                    DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId, " +
                    DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId, " +
                    DbSyntax.QuoteIdentifier("ArticleId") + " = @ArticleId, " +
                    DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId, " +
                    DbSyntax.QuoteIdentifier("CommentOn") + " = @CommentOn" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Rank", item.Rank),
                    DbHelper.CreateParameter("@Style", item.Style),
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@ArticleId", item.ArticleId),
                    DbHelper.CreateParameter("@SiteId", item.SiteId),
                    DbHelper.CreateParameter("@CommentOn", item.CommentOn),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("ArticleLink") + " (" +
                    DbSyntax.QuoteIdentifier("Rank") + ", " +
                    DbSyntax.QuoteIdentifier("Style") + ", " +
                    DbSyntax.QuoteIdentifier("ObjectId") + ", " +
                    DbSyntax.QuoteIdentifier("RecordId") + ", " +
                    DbSyntax.QuoteIdentifier("ArticleId") + ", " +
                    DbSyntax.QuoteIdentifier("SiteId") + ", " +
                    DbSyntax.QuoteIdentifier("CommentOn") +
                    ") VALUES (@Rank, @Style, @ObjectId, @RecordId, @ArticleId, @SiteId, @CommentOn)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Rank", item.Rank),
                    DbHelper.CreateParameter("@Style", item.Style),
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@ArticleId", item.ArticleId),
                    DbHelper.CreateParameter("@SiteId", item.SiteId),
                    DbHelper.CreateParameter("@CommentOn", item.CommentOn)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        protected override string DeleteProcedure { get { return "ArticleLink_Del"; } }
        protected override string TableName { get { return "ArticleLink"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure { get { return "ArticleLink_Get"; } }

        protected override ArticleLink From(IDataReader r, ArticleLink source)
        {
            ArticleLink item = source ?? new ArticleLink();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Rank = (int)r["Rank"];
            item.Style = r["Style"].ToString();
            item.ObjectId = DataUtil.GetId(r, WebColumns.ObjectId);
            item.RecordId = DataUtil.GetId(r, WebColumns.RecordId);
            item.ArticleId = DataUtil.GetId(r["ArticleId"]);
            item.SiteId = DataUtil.GetId(r, WebColumns.SiteId);
            item.CommentOn = DataUtil.GetInt32(r, "CommentOn");

            return item;
        }
    }
}
