using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Common;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Article.Providers
{
    public class ArticleListProvider : GenericSqlDataProviderBase<ArticleList>, IArticleListProvider
    {
        public ArticleList Get(int objectId, int recordId)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("ArticleList") +
                " WHERE " + DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId" +
                " AND " + DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@ObjectId", objectId),
                DbHelper.CreateParameter("@RecordId", recordId)))
            {
                if (r.Read())
                    return this.From(r);
            }

            return null;
        }

        public override int Update(ArticleList item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("ArticleList") + " SET " +
                    DbSyntax.QuoteIdentifier("ObjectId") + " = @ObjectId, " +
                    DbSyntax.QuoteIdentifier("RecordId") + " = @RecordId, " +
                    DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId, " +
                    DbSyntax.QuoteIdentifier("TemplateId") + " = @TemplateId, " +
                    DbSyntax.QuoteIdentifier("PageSize") + " = @PageSize, " +
                    DbSyntax.QuoteIdentifier("FolderId") + " = @FolderId, " +
                    DbSyntax.QuoteIdentifier("CommentOn") + " = @CommentOn" +
                    " WHERE " + DbSyntax.QuoteIdentifier("ListId") + " = @ListId";
                parms = new[] {
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@SiteId", item.SiteId),
                    DbHelper.CreateParameter("@TemplateId", item.TemplateId),
                    DbHelper.CreateParameter("@PageSize", item.PageSize),
                    DbHelper.CreateParameter("@FolderId", item.FolderId),
                    DbHelper.CreateParameter("@CommentOn", item.CommentOn),
                    DbHelper.CreateParameter("@ListId", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("ArticleList") + " (" +
                    DbSyntax.QuoteIdentifier("ObjectId") + ", " +
                    DbSyntax.QuoteIdentifier("RecordId") + ", " +
                    DbSyntax.QuoteIdentifier("SiteId") + ", " +
                    DbSyntax.QuoteIdentifier("TemplateId") + ", " +
                    DbSyntax.QuoteIdentifier("PageSize") + ", " +
                    DbSyntax.QuoteIdentifier("FolderId") + ", " +
                    DbSyntax.QuoteIdentifier("CommentOn") +
                    ") VALUES (@ObjectId, @RecordId, @SiteId, @TemplateId, @PageSize, @FolderId, @CommentOn)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("ListId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@ObjectId", item.ObjectId),
                    DbHelper.CreateParameter("@RecordId", item.RecordId),
                    DbHelper.CreateParameter("@SiteId", item.SiteId),
                    DbHelper.CreateParameter("@TemplateId", item.TemplateId),
                    DbHelper.CreateParameter("@PageSize", item.PageSize),
                    DbHelper.CreateParameter("@FolderId", item.FolderId),
                    DbHelper.CreateParameter("@CommentOn", item.CommentOn)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        protected override ArticleList From(IDataReader r, ArticleList source)
        {
            ArticleList item = source ?? new ArticleList();
            item.Id = DataUtil.GetId(r["ListId"]);
            item.PageSize = (int)r["PageSize"];
            //item.DateFormatString = r["DateFormatString"].ToString();
            item.ObjectId = DataUtil.GetId(r["ObjectId"]);
            item.RecordId = DataUtil.GetId(r["RecordId"]);
            item.TemplateId = DataUtil.GetId(r, WebColumns.TemplateId);
            item.SiteId = DataUtil.GetId(r, WebColumns.SiteId);
            item.FolderId = DataUtil.GetId(r, WebColumns.FolderId);
            item.CommentOn = DataUtil.GetInt32(r, "CommentOn");

            return item;
        }

        protected override string DeleteProcedure { get { return "ArticleList_Del"; } }
        protected override string TableName { get { return "ArticleList"; } }

        protected override string IdColumn { get { return "ListId"; } }


        protected override string SelectProcedure { get { return "ArticleList_Get"; } }
    }
}
