using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Social.Providers
{
    public class WallUpdateSqlProvider : GenericSqlDataProviderBase<WallUpdate>, IWallUpdateProvider
    {
        protected override string DeleteProcedure { get { return "WallUpdate_Del"; } }
        protected override string TableName { get { return "WallUpdate"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure { get { return "WallUpdate_Get"; } }

        protected override WallUpdate From(IDataReader r, WallUpdate source)
        {
            WallUpdate item = source ?? new WallUpdate();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.UpdateObjectId = DataUtil.GetId(r, "UpdateObjectId");
            item.UpdateRecordId = DataUtil.GetId(r, "UpdateRecordId");
            item.ByObjectId = DataUtil.GetId(r, "ByObjectId");
            item.ByRecordId = DataUtil.GetId(r, "ByRecordId");
            item.Content = DataUtil.Get(r, "Content");
            item.UpdateDate = DataUtil.GetDateTime(r, "UpdateDate");
            item.EventTypeId = DataUtil.GetInt32(r, "EventTypeId");

            return item;
        }

        public override int Update(WallUpdate item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("WallUpdate") + " SET " +
                    DbSyntax.QuoteIdentifier("UpdateObjectId") + " = @UpdateObjectId, " +
                    DbSyntax.QuoteIdentifier("UpdateRecordId") + " = @UpdateRecordId, " +
                    DbSyntax.QuoteIdentifier("ByObjectId") + " = @ByObjectId, " +
                    DbSyntax.QuoteIdentifier("ByRecordId") + " = @ByRecordId, " +
                    DbSyntax.QuoteIdentifier("Content") + " = @Content, " +
                    DbSyntax.QuoteIdentifier("UpdateDate") + " = @UpdateDate, " +
                    DbSyntax.QuoteIdentifier("EventTypeId") + " = @EventTypeId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@UpdateObjectId", item.UpdateObjectId),
                    DbHelper.CreateParameter("@UpdateRecordId", item.UpdateRecordId),
                    DbHelper.CreateParameter("@ByObjectId", item.ByObjectId),
                    DbHelper.CreateParameter("@ByRecordId", item.ByRecordId),
                    DbHelper.CreateParameter("@Content", item.Content),
                    DbHelper.CreateParameter("@UpdateDate", item.UpdateDate),
                    DbHelper.CreateParameter("@EventTypeId", item.EventTypeId),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("WallUpdate") + " (" +
                    DbSyntax.QuoteIdentifier("UpdateObjectId") + ", " +
                    DbSyntax.QuoteIdentifier("UpdateRecordId") + ", " +
                    DbSyntax.QuoteIdentifier("ByObjectId") + ", " +
                    DbSyntax.QuoteIdentifier("ByRecordId") + ", " +
                    DbSyntax.QuoteIdentifier("Content") + ", " +
                    DbSyntax.QuoteIdentifier("UpdateDate") + ", " +
                    DbSyntax.QuoteIdentifier("EventTypeId") +
                    ") VALUES (@UpdateObjectId, @UpdateRecordId, @ByObjectId, @ByRecordId, @Content, @UpdateDate, @EventTypeId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@UpdateObjectId", item.UpdateObjectId),
                    DbHelper.CreateParameter("@UpdateRecordId", item.UpdateRecordId),
                    DbHelper.CreateParameter("@ByObjectId", item.ByObjectId),
                    DbHelper.CreateParameter("@ByRecordId", item.ByRecordId),
                    DbHelper.CreateParameter("@Content", item.Content),
                    DbHelper.CreateParameter("@UpdateDate", item.UpdateDate),
                    DbHelper.CreateParameter("@EventTypeId", item.EventTypeId)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        public IEnumerable<WallUpdate> GetList(int updateObjectId = -2, int updateRecordId = -2, int byObjectId = -2, int byRecordId = -2, 
            int eventTypeId = -2, DateTime? updateDateStart = null, DateTime? updateDateEnd = null)
        {
            List<WallUpdate> items = new List<WallUpdate>();

            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("WallUpdate") + " WHERE 1=1";
            var parmList = new List<DbParameter>();
            if (updateObjectId != -2) { sql += " AND " + DbSyntax.QuoteIdentifier("UpdateObjectId") + " = @UpdateObjectId"; parmList.Add(DbHelper.CreateParameter("@UpdateObjectId", updateObjectId)); }
            if (updateRecordId != -2) { sql += " AND " + DbSyntax.QuoteIdentifier("UpdateRecordId") + " = @UpdateRecordId"; parmList.Add(DbHelper.CreateParameter("@UpdateRecordId", updateRecordId)); }
            if (byObjectId != -2) { sql += " AND " + DbSyntax.QuoteIdentifier("ByObjectId") + " = @ByObjectId"; parmList.Add(DbHelper.CreateParameter("@ByObjectId", byObjectId)); }
            if (byRecordId != -2) { sql += " AND " + DbSyntax.QuoteIdentifier("ByRecordId") + " = @ByRecordId"; parmList.Add(DbHelper.CreateParameter("@ByRecordId", byRecordId)); }
            if (eventTypeId != -2) { sql += " AND " + DbSyntax.QuoteIdentifier("EventTypeId") + " = @EventTypeId"; parmList.Add(DbHelper.CreateParameter("@EventTypeId", eventTypeId)); }
            if (updateDateStart != null) { sql += " AND " + DbSyntax.QuoteIdentifier("UpdateDate") + " >= @UpdateDateStart"; parmList.Add(DbHelper.CreateParameter("@UpdateDateStart", updateDateStart)); }
            if (updateDateEnd != null) { sql += " AND " + DbSyntax.QuoteIdentifier("UpdateDate") + " <= @UpdateDateEnd"; parmList.Add(DbHelper.CreateParameter("@UpdateDateEnd", updateDateEnd)); }

            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql, parmList.ToArray()))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }
    }
}
