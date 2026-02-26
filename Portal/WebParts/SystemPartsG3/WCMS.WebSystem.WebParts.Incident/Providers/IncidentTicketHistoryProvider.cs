using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Incident.Providers
{
    public class IncidentTicketHistoryProvider : GenericSqlDataProviderBase<IncidentTicketHistory> , IIncidentTicketHistoryProvider
    {
        protected override string DeleteProcedure { get { return "IncidentTicketHistory_Del"; } }
        protected override string TableName { get { return "IncidentTicketHistory"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure { get { return "IncidentTicketHistory_Get"; } }

        protected override IncidentTicketHistory From(IDataReader r, IncidentTicketHistory source)
        {
            IncidentTicketHistory item = source ?? new IncidentTicketHistory();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.TicketId = DataUtil.GetId(r, "TicketId");
            item.UserId = DataUtil.GetId(r, WebColumns.UserId);
            item.Content = DataUtil.Get(r, "Content");
            item.DateCreated = DataUtil.GetDateTime(r, "DateCreated");
            item.Type = DataUtil.GetInt32(r, "Type");

            return item;
        }

        public override int Update(IncidentTicketHistory item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("IncidentTicketHistory") + " SET " +
                    DbSyntax.QuoteIdentifier("TicketId") + " = @TicketId, " +
                    DbSyntax.QuoteIdentifier("UserId") + " = @UserId, " +
                    DbSyntax.QuoteIdentifier("Content") + " = @Content, " +
                    DbSyntax.QuoteIdentifier("DateCreated") + " = @DateCreated, " +
                    DbSyntax.QuoteIdentifier("Type") + " = @Type" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@TicketId", item.TicketId),
                    DbHelper.CreateParameter("@UserId", item.UserId),
                    DbHelper.CreateParameter("@Content", item.Content),
                    DbHelper.CreateParameter("@DateCreated", item.DateCreated),
                    DbHelper.CreateParameter("@Type", item.Type),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("IncidentTicketHistory") + " (" +
                    DbSyntax.QuoteIdentifier("TicketId") + ", " +
                    DbSyntax.QuoteIdentifier("UserId") + ", " +
                    DbSyntax.QuoteIdentifier("Content") + ", " +
                    DbSyntax.QuoteIdentifier("DateCreated") + ", " +
                    DbSyntax.QuoteIdentifier("Type") +
                    ") VALUES (@TicketId, @UserId, @Content, @DateCreated, @Type)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@TicketId", item.TicketId),
                    DbHelper.CreateParameter("@UserId", item.UserId),
                    DbHelper.CreateParameter("@Content", item.Content),
                    DbHelper.CreateParameter("@DateCreated", item.DateCreated),
                    DbHelper.CreateParameter("@Type", item.Type)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        #region IIncidentTicketHistoryProvider Members

        public IEnumerable<IncidentTicketHistory> GetList(int ticketId = -2, int userId = -2, int type = -2)
        {
            List<IncidentTicketHistory> items = new List<IncidentTicketHistory>();

            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("IncidentTicketHistory") + " WHERE 1=1";
            var parmList = new List<DbParameter>();
            if (ticketId != -2) { sql += " AND " + DbSyntax.QuoteIdentifier("TicketId") + " = @TicketId"; parmList.Add(DbHelper.CreateParameter("@TicketId", ticketId)); }
            if (userId != -2) { sql += " AND " + DbSyntax.QuoteIdentifier("UserId") + " = @UserId"; parmList.Add(DbHelper.CreateParameter("@UserId", userId)); }
            if (type != -2) { sql += " AND " + DbSyntax.QuoteIdentifier("Type") + " = @Type"; parmList.Add(DbHelper.CreateParameter("@Type", type)); }

            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql, parmList.ToArray()))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        #endregion
    }
}
