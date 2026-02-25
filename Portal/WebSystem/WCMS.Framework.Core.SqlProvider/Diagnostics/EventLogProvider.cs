using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;

using WCMS.Framework.Diagnostics;

namespace WCMS.Framework.Core.SqlProvider.Diagnostics
{
    public class EventLogProvider : GenericSqlDataProviderBase<EventLog>, IEventLogProvider
    {
        public bool Delete(DateTime eventDate)
        {
            var sql = "DELETE FROM EventLog WHERE " + DbSyntax.QuoteIdentifier("EventDate") + " = @EventDate";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@EventDate", eventDate));

            return true;
        }

        protected override EventLog From(IDataReader r, EventLog source)
        {
            var item = source ?? new EventLog();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.UserId = DataUtil.GetId(r, WebColumns.UserId);
            item.EventDate = DataUtil.GetDateTime(r, "EventDate");
            item.Content = DataUtil.Get(r, "Content");
            item.EventName = DataUtil.Get(r, "EventName");
            item.IPAddress = DataUtil.Get(r, "IPAddress");

            return item;
        }

        public IEnumerable<EventLog> GetList(DateTime eventDate, int userId = -1)
        {
            var items = new List<EventLog>();
            var sql = "SELECT * FROM EventLog WHERE " + DbSyntax.QuoteIdentifier("EventDate") + " = @EventDate AND " + DbSyntax.QuoteIdentifier("UserId") + " = @UserId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@EventDate", eventDate),
                DbHelper.CreateParameter("@UserId", userId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public override int Update(EventLog item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE EventLog SET " +
                    DbSyntax.QuoteIdentifier("EventDate") + " = @EventDate, " +
                    DbSyntax.QuoteIdentifier("Content") + " = @Content, " +
                    DbSyntax.QuoteIdentifier("UserId") + " = @UserId, " +
                    DbSyntax.QuoteIdentifier("EventName") + " = @EventName, " +
                    DbSyntax.QuoteIdentifier("IPAddress") + " = @IPAddress" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@EventDate", item.EventDate),
                    DbHelper.CreateParameter("@Content", item.Content),
                    DbHelper.CreateParameter("@UserId", item.UserId),
                    DbHelper.CreateParameter("@EventName", item.EventName),
                    DbHelper.CreateParameter("@IPAddress", item.IPAddress),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO EventLog (" +
                    DbSyntax.QuoteIdentifier("EventDate") + ", " +
                    DbSyntax.QuoteIdentifier("Content") + ", " +
                    DbSyntax.QuoteIdentifier("UserId") + ", " +
                    DbSyntax.QuoteIdentifier("EventName") + ", " +
                    DbSyntax.QuoteIdentifier("IPAddress") +
                    ") VALUES (@EventDate, @Content, @UserId, @EventName, @IPAddress)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@EventDate", item.EventDate),
                    DbHelper.CreateParameter("@Content", item.Content),
                    DbHelper.CreateParameter("@UserId", item.UserId),
                    DbHelper.CreateParameter("@EventName", item.EventName),
                    DbHelper.CreateParameter("@IPAddress", item.IPAddress)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        protected override string TableName { get { return "EventLog"; } }


        protected override string IdColumn { get { return "Id"; } }



        protected override string SelectProcedure
        {
            get { return "EventLog_Get"; }
        }

        protected override string DeleteProcedure
        {
            get { return "EventLog_Del"; }
        }
    }
}
