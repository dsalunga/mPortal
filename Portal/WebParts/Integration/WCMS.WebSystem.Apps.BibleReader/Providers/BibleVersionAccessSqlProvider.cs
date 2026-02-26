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
    public class BibleVersionAccessSqlProvider : GenericSqlDataProviderBase<BibleVersionAccess>, IBibleVersionAccessProvider
    {
        protected override string DeleteProcedure { get { return "BibleReaderVersionAccess_Del"; } }

        protected override BibleVersionAccess From(IDataReader r, BibleVersionAccess source)
        {
            var item = source ?? new BibleVersionAccess();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.BibleAccessId = DataUtil.GetId(r, "BibleAccessId");
            item.BibleVersionId = DataUtil.GetId(r, "BibleVersionId");
            item.VersionAccessCount = DataUtil.GetInt32(r, "VersionAccessCount");
            item.BibleVersionName = DataUtil.Get(r, "BibleVersionName");
            item.LastAccessed = DataUtil.GetDateTime(r, "LastAccessed");

            return item;
        }

        protected override string TableName { get { return "BibleReaderVersionAccess"; } }


        protected override string IdColumn { get { return "Id"; } }



        protected override string SelectProcedure { get { return "BibleReaderVersionAccess_Get"; } }

        public override int Update(BibleVersionAccess item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("BibleReaderVersionAccess") + " SET " +
                    DbSyntax.QuoteIdentifier("BibleAccessId") + " = @BibleAccessId, " +
                    DbSyntax.QuoteIdentifier("BibleVersionId") + " = @BibleVersionId, " +
                    DbSyntax.QuoteIdentifier("BibleVersionName") + " = @BibleVersionName, " +
                    DbSyntax.QuoteIdentifier("VersionAccessCount") + " = @VersionAccessCount, " +
                    DbSyntax.QuoteIdentifier("LastAccessed") + " = @LastAccessed" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@BibleAccessId", item.BibleAccessId),
                    DbHelper.CreateParameter("@BibleVersionId", item.BibleVersionId),
                    DbHelper.CreateParameter("@BibleVersionName", item.BibleVersionName),
                    DbHelper.CreateParameter("@VersionAccessCount", item.VersionAccessCount),
                    DbHelper.CreateParameter("@LastAccessed", item.LastAccessed),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("BibleReaderVersionAccess") + " (" +
                    DbSyntax.QuoteIdentifier("BibleAccessId") + ", " +
                    DbSyntax.QuoteIdentifier("BibleVersionId") + ", " +
                    DbSyntax.QuoteIdentifier("BibleVersionName") + ", " +
                    DbSyntax.QuoteIdentifier("VersionAccessCount") + ", " +
                    DbSyntax.QuoteIdentifier("LastAccessed") +
                    ") VALUES (@BibleAccessId, @BibleVersionId, @BibleVersionName, @VersionAccessCount, @LastAccessed)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@BibleAccessId", item.BibleAccessId),
                    DbHelper.CreateParameter("@BibleVersionId", item.BibleVersionId),
                    DbHelper.CreateParameter("@BibleVersionName", item.BibleVersionName),
                    DbHelper.CreateParameter("@VersionAccessCount", item.VersionAccessCount),
                    DbHelper.CreateParameter("@LastAccessed", item.LastAccessed)
                };
                var o = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                return UpdatePostProcess(item, o);
            }

            return item.Id;
        }

        public IEnumerable<BibleVersionAccess> GetList(int accessId)
        {
            List<BibleVersionAccess> items = new List<BibleVersionAccess>();

            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("BibleReaderVersionAccess") + " WHERE " + DbSyntax.QuoteIdentifier("BibleAccessId") + " = @BibleAccessId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@BibleAccessId", accessId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }
    }
}
