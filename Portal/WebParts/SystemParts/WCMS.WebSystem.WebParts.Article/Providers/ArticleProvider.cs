using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Article.Providers
{
    public class ArticleProvider : GenericSqlDataProviderBase<Article>, IArticleProvider
    {
        public IEnumerable<Article> GetList(int siteId = -2)
        {
            var items = new List<Article>();
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("Articles");
            var parms = new List<DbParameter>();
            if (siteId != -2)
            {
                sql += " WHERE " + DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId";
                parms.Add(DbHelper.CreateParameter("@SiteId", siteId));
            }
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql, parms.ToArray()))
            {
                while (r.Read())
                    items.Add(this.From(r));
            }

            return items;
        }

        public override int Update(Article item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("Articles") + " SET " +
                    DbSyntax.QuoteIdentifier("Title") + " = @Title, " +
                    DbSyntax.QuoteIdentifier("Image") + " = @Image, " +
                    DbSyntax.QuoteIdentifier("Description") + " = @Description, " +
                    DbSyntax.QuoteIdentifier("Date") + " = @Date, " +
                    DbSyntax.QuoteIdentifier("Content") + " = @Content, " +
                    DbSyntax.QuoteIdentifier("Author") + " = @Author, " +
                    DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId, " +
                    DbSyntax.QuoteIdentifier("Active") + " = @Active, " +
                    DbSyntax.QuoteIdentifier("DateModified") + " = @DateModified, " +
                    DbSyntax.QuoteIdentifier("UserId") + " = @UserId, " +
                    DbSyntax.QuoteIdentifier("ModifiedUserId") + " = @ModifiedUserId, " +
                    DbSyntax.QuoteIdentifier("Tags") + " = @Tags" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Title", item.Title),
                    DbHelper.CreateParameter("@Image", item.Image),
                    DbHelper.CreateParameter("@Description", item.Description),
                    DbHelper.CreateParameter("@Date", item.Date),
                    DbHelper.CreateParameter("@Content", item.Content),
                    DbHelper.CreateParameter("@Author", item.Author),
                    DbHelper.CreateParameter("@SiteId", item.SiteId),
                    DbHelper.CreateParameter("@Active", item.Active),
                    DbHelper.CreateParameter("@DateModified", item.DateModified),
                    DbHelper.CreateParameter("@UserId", item.UserId),
                    DbHelper.CreateParameter("@ModifiedUserId", item.ModifiedUserId),
                    DbHelper.CreateParameter("@Tags", item.Tags),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("Articles") + " (" +
                    DbSyntax.QuoteIdentifier("Title") + ", " +
                    DbSyntax.QuoteIdentifier("Image") + ", " +
                    DbSyntax.QuoteIdentifier("Description") + ", " +
                    DbSyntax.QuoteIdentifier("Date") + ", " +
                    DbSyntax.QuoteIdentifier("Content") + ", " +
                    DbSyntax.QuoteIdentifier("Author") + ", " +
                    DbSyntax.QuoteIdentifier("SiteId") + ", " +
                    DbSyntax.QuoteIdentifier("Active") + ", " +
                    DbSyntax.QuoteIdentifier("DateModified") + ", " +
                    DbSyntax.QuoteIdentifier("UserId") + ", " +
                    DbSyntax.QuoteIdentifier("ModifiedUserId") + ", " +
                    DbSyntax.QuoteIdentifier("Tags") +
                    ") VALUES (@Title, @Image, @Description, @Date, @Content, @Author, @SiteId, @Active, @DateModified, @UserId, @ModifiedUserId, @Tags)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Title", item.Title),
                    DbHelper.CreateParameter("@Image", item.Image),
                    DbHelper.CreateParameter("@Description", item.Description),
                    DbHelper.CreateParameter("@Date", item.Date),
                    DbHelper.CreateParameter("@Content", item.Content),
                    DbHelper.CreateParameter("@Author", item.Author),
                    DbHelper.CreateParameter("@SiteId", item.SiteId),
                    DbHelper.CreateParameter("@Active", item.Active),
                    DbHelper.CreateParameter("@DateModified", item.DateModified),
                    DbHelper.CreateParameter("@UserId", item.UserId),
                    DbHelper.CreateParameter("@ModifiedUserId", item.ModifiedUserId),
                    DbHelper.CreateParameter("@Tags", item.Tags)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        protected override Article From(IDataReader r, Article source)
        {
            Article item = source ?? new Article();
            item.Id = DataUtil.GetId(r["Id"]);
            item.Title = r["Title"].ToString();
            item.Image = r["Image"].ToString();
            item.Description = r["Description"].ToString();
            item.Date = (DateTime)r["Date"];
            item.Content = r["Content"].ToString();
            item.Author = r["Author"].ToString();
            item.SiteId = DataUtil.GetId(r[WebColumns.SiteId]);
            item.Active = Convert.ToInt32(r["Active"].ToString());
            item.DateModified = (DateTime)r["DateModified"];
            item.UserId = DataUtil.GetId(r["UserId"]);
            item.ModifiedUserId = DataUtil.GetId(r["ModifiedUserId"]);
            item.Tags = r["Tags"].ToString();

            return item;
        }

        protected override string DeleteProcedure { get { return "Articles_Del"; } }
        protected override string TableName { get { return "Articles"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure { get { return "Articles_Get"; } }
    }
}
