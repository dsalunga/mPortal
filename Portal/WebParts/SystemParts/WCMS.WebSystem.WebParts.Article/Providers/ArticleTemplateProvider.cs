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
    public class ArticleTemplateProvider : GenericSqlDataProviderBase<ArticleTemplate>, IArticleTemplateProvider
    {
        protected override ArticleTemplate From(IDataReader r, ArticleTemplate source)
        {
            var item = source ?? new ArticleTemplate();
            item.Id = DataUtil.GetId(r, "Id");
            item.Name = r["Name"].ToString();
            item.Date = (DateTime)r["Date"];
            item.File = r["File"].ToString();
            item.ImageUrl = r["ImageUrl"].ToString();
            item.ListItemTemplate = r["ListItemTemplate"].ToString();
            item.ListTemplate = r["ListTemplate"].ToString();
            item.DetailsTemplate = r["DetailsTemplate"].ToString();
            item.DateFormat = DataUtil.Get(r, "DateFormat");

            return item;
        }

        public override int Update(ArticleTemplate item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("ArticleTemplate") + " SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("Date") + " = @Date, " +
                    DbSyntax.QuoteIdentifier("File") + " = @File, " +
                    DbSyntax.QuoteIdentifier("ImageUrl") + " = @ImageUrl, " +
                    DbSyntax.QuoteIdentifier("ListItemTemplate") + " = @ListItemTemplate, " +
                    DbSyntax.QuoteIdentifier("ListTemplate") + " = @ListTemplate, " +
                    DbSyntax.QuoteIdentifier("DetailsTemplate") + " = @DetailsTemplate, " +
                    DbSyntax.QuoteIdentifier("DateFormat") + " = @DateFormat" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@Date", item.Date),
                    DbHelper.CreateParameter("@File", item.File),
                    DbHelper.CreateParameter("@ImageUrl", item.ImageUrl),
                    DbHelper.CreateParameter("@ListItemTemplate", item.ListItemTemplate),
                    DbHelper.CreateParameter("@ListTemplate", item.ListTemplate),
                    DbHelper.CreateParameter("@DetailsTemplate", item.DetailsTemplate),
                    DbHelper.CreateParameter("@DateFormat", item.DateFormat),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("ArticleTemplate") + " (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("Date") + ", " +
                    DbSyntax.QuoteIdentifier("File") + ", " +
                    DbSyntax.QuoteIdentifier("ImageUrl") + ", " +
                    DbSyntax.QuoteIdentifier("ListItemTemplate") + ", " +
                    DbSyntax.QuoteIdentifier("ListTemplate") + ", " +
                    DbSyntax.QuoteIdentifier("DetailsTemplate") + ", " +
                    DbSyntax.QuoteIdentifier("DateFormat") +
                    ") VALUES (@Name, @Date, @File, @ImageUrl, @ListItemTemplate, @ListTemplate, @DetailsTemplate, @DateFormat)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@Date", item.Date),
                    DbHelper.CreateParameter("@File", item.File),
                    DbHelper.CreateParameter("@ImageUrl", item.ImageUrl),
                    DbHelper.CreateParameter("@ListItemTemplate", item.ListItemTemplate),
                    DbHelper.CreateParameter("@ListTemplate", item.ListTemplate),
                    DbHelper.CreateParameter("@DetailsTemplate", item.DetailsTemplate),
                    DbHelper.CreateParameter("@DateFormat", item.DateFormat)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        protected override string DeleteProcedure { get { return "ArticleTemplate_Del"; } }
        protected override string TableName { get { return "ArticleTemplate"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure { get { return "ArticleTemplate_Get"; } }
    }
}
