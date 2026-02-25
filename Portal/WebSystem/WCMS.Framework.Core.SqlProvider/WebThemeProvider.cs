using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using WCMS.Common.Utilities;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebThemeProvider : GenericSqlDataProviderBase<WebTheme>, IWebThemeProvider
    {
        protected override string TableName { get { return "WebTheme"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure { get { return "WebTheme_Get"; } }
        protected override string DeleteProcedure { get { return "WebTheme_Del"; } }

        protected override WebTheme From(IDataReader r, WebTheme source)
        {
            WebTheme item = source ?? new WebTheme();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.TemplateId = DataUtil.GetId(r, WebColumns.TemplateId);
            item.ParentId = DataUtil.GetId(r, WebColumns.ParentId);
            item.Identity = DataUtil.Get(r, WebColumns.Identity);
            item.SkinId = DataUtil.GetId(r, WebColumns.SkinId);

            return item;
        }

        public override int Update(WebTheme item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebTheme SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("TemplateId") + " = @TemplateId, " +
                    DbSyntax.QuoteIdentifier("ParentId") + " = @ParentId, " +
                    DbSyntax.QuoteIdentifier("Identity") + " = @Identity, " +
                    DbSyntax.QuoteIdentifier("SkinId") + " = @SkinId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@TemplateId", item.TemplateId),
                    DbHelper.CreateParameter("@ParentId", item.ParentId),
                    DbHelper.CreateParameter("@Identity", item.Identity),
                    DbHelper.CreateParameter("@SkinId", item.SkinId),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebTheme (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("TemplateId") + ", " +
                    DbSyntax.QuoteIdentifier("ParentId") + ", " +
                    DbSyntax.QuoteIdentifier("Identity") + ", " +
                    DbSyntax.QuoteIdentifier("SkinId") +
                    ") VALUES (@Name, @TemplateId, @ParentId, @Identity, @SkinId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@TemplateId", item.TemplateId),
                    DbHelper.CreateParameter("@ParentId", item.ParentId),
                    DbHelper.CreateParameter("@Identity", item.Identity),
                    DbHelper.CreateParameter("@SkinId", item.SkinId)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }
    }
}
