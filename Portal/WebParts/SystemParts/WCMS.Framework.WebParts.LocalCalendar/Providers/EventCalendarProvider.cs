using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public class EventCalendarProvider : IEventCalendarProvider
    {
        #region IDataProvider<CalendarItem> Members

        public bool Delete(int id)
        {
            var sql = "DELETE FROM " + DbSyntax.QuoteIdentifier("EventCalendar") +
                " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", id));

            return true;
        }

        public CalendarItem Get(int id)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("EventCalendar") +
                " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", id)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        private CalendarItem From(DbDataReader r)
        {
            CalendarItem item = new CalendarItem();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.SiteId = DataUtil.GetId(r, WebColumns.SiteId);

            return item;
        }

        public CalendarItem Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CalendarItem> GetList()
        {
            List<CalendarItem> items = new List<CalendarItem>();

            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("EventCalendar");
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<CalendarItem> GetList(int siteId = -2)
        {
            List<CalendarItem> items = new List<CalendarItem>();

            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("EventCalendar");
            var parms = new List<DbParameter>();
            if (siteId != -2)
            {
                sql += " WHERE " + DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId";
                parms.Add(DbHelper.CreateParameter("@SiteId", siteId));
            }

            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql, parms.ToArray()))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<CalendarItem> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public int Update(CalendarItem item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("EventCalendar") + " SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("SiteId") + " = @SiteId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@SiteId", item.SiteId),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("EventCalendar") + " (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("SiteId") +
                    ") VALUES (@Name, @SiteId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@SiteId", item.SiteId)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public CalendarItem Refresh(CalendarItem item)
        {
            throw new NotImplementedException();
        }
    }
}
