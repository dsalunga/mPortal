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
    public class WebParameterSetProvider : GenericSqlDataProviderBase<WebParameterSet>, IWebParameterSetProvider
    {
        protected override string TableName { get { return "WebParameterSet"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure { get { return "WebParameterSet_Get"; } }
        protected override string DeleteProcedure { get { return "WebParameterSet_Del"; } }

        protected override WebParameterSet From(IDataReader r, WebParameterSet source)
        {
            WebParameterSet item = source ?? new WebParameterSet();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.SchemaParameterName = DataUtil.Get(r, "SchemaParameterName");

            return item;
        }

        public override int Update(WebParameterSet item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebParameterSet SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("SchemaParameterName") + " = @SchemaParameterName" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@SchemaParameterName", item.SchemaParameterName),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebParameterSet (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("SchemaParameterName") +
                    ") VALUES (@Name, @SchemaParameterName)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@SchemaParameterName", item.SchemaParameterName)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }
    }
}
