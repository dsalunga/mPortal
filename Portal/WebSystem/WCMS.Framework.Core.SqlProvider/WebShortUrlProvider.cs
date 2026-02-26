using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using WCMS.Common.Utilities;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebShortUrlProvider : GenericSqlDataProviderBase<WebShortUrl>, IWebShortUrlProvider
    {
        protected override string TableName { get { return "WebShortUrl"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure
        {
            get { return "WebShortUrl_Get"; }
        }

        protected override string DeleteProcedure
        {
            get { return "WebShortUrl_Del"; }
        }

        protected override WebShortUrl From(IDataReader r, WebShortUrl source)
        {
            var item = source ?? new WebShortUrl();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.PageId = DataUtil.GetId(r, WebColumns.PageId);
            item.PageUrl = DataUtil.Get(r, "PageUrl");

            return item;
        }

        public override int Update(WebShortUrl item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebShortUrl SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("PageId") + " = @PageId, " +
                    DbSyntax.QuoteIdentifier("PageUrl") + " = @PageUrl" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@PageId", item.PageId),
                    DbHelper.CreateParameter("@PageUrl", item.PageUrl),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebShortUrl (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("PageId") + ", " +
                    DbSyntax.QuoteIdentifier("PageUrl") +
                    ") VALUES (@Name, @PageId, @PageUrl)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@PageId", item.PageId),
                    DbHelper.CreateParameter("@PageUrl", item.PageUrl)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                return UpdatePostProcess(item, obj);
            }

            return UpdatePostProcess(item, item.Id);
        }

        public WebShortUrl Get(string name)
        {
            var sql = "SELECT * FROM WebShortUrl WHERE " + DbSyntax.QuoteIdentifier("Name") + " = @Name";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Name", name)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public WebShortUrl GetByPageId(int pageId)
        {
            var sql = "SELECT * FROM WebShortUrl WHERE " + DbSyntax.QuoteIdentifier("PageId") + " = @PageId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@PageId", pageId)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }
    }
}
