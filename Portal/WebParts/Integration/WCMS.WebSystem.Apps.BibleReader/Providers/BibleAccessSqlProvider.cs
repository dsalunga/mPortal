using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.BibleReader.Providers
{
    public class BibleAccessSqlProvider : GenericSqlDataProviderBase<BibleAccess>, IBibleAccessProvider
    {
        protected override string DeleteProcedure { get { return "BibleReaderAccess_Del"; } }

        protected override BibleAccess From(IDataReader r, BibleAccess source)
        {
            var item = source ?? new BibleAccess();

            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.UserId = DataUtil.GetId(r, WebColumns.UserId);
            item.AppAccessCount = DataUtil.GetInt32(r, "AppAccessCount");
            item.LastAccessed = DataUtil.GetDateTime(r, "LastAccessed");

            return item;
        }

        protected override string TableName { get { return "BibleReaderAccess"; } }


        protected override string IdColumn { get { return "Id"; } }



        protected override string SelectProcedure { get { return "BibleReaderAccess_Get"; } }

        public override int Update(BibleAccess item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE BibleReaderAccess SET " +
                    DbSyntax.QuoteIdentifier("UserId") + " = @UserId, " +
                    DbSyntax.QuoteIdentifier("AppAccessCount") + " = @AppAccessCount, " +
                    DbSyntax.QuoteIdentifier("LastAccessed") + " = @LastAccessed" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@UserId", item.UserId),
                    DbHelper.CreateParameter("@AppAccessCount", item.AppAccessCount),
                    DbHelper.CreateParameter("@LastAccessed", item.LastAccessed),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO BibleReaderAccess (" +
                    DbSyntax.QuoteIdentifier("UserId") + ", " +
                    DbSyntax.QuoteIdentifier("AppAccessCount") + ", " +
                    DbSyntax.QuoteIdentifier("LastAccessed") +
                    ") VALUES (@UserId, @AppAccessCount, @LastAccessed)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@UserId", item.UserId),
                    DbHelper.CreateParameter("@AppAccessCount", item.AppAccessCount),
                    DbHelper.CreateParameter("@LastAccessed", item.LastAccessed)
                };
                var o = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                return UpdatePostProcess(item, o);
            }

            return UpdatePostProcess(item, item.Id);
        }

        public new BibleAccess Get(int userId)
        {
            var sql = "SELECT * FROM BibleReaderAccess WHERE " + DbSyntax.QuoteIdentifier("UserId") + " = @UserId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@UserId", userId)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }
    }
}
