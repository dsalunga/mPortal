using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.EventCalendar.Providers
{
    public class CalendarEventProvider
    {
        public CalendarEvent Get(int eventId)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("EventCalendarEvents") +
                " WHERE " + DbSyntax.QuoteIdentifier("EventId") + " = @EventId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@EventId", eventId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public IEnumerable<CalendarEvent> GetList(int calendarId = -1)
        {
            List<CalendarEvent> items = new List<CalendarEvent>();
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("EventCalendarEvents");
            var parms = new List<DbParameter>();
            if (calendarId != -1)
            {
                sql += " WHERE " + DbSyntax.QuoteIdentifier("CalendarId") + " = @CalendarId";
                parms.Add(DbHelper.CreateParameter("@CalendarId", calendarId));
            }
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql, parms.ToArray()))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<CalendarEvent> GetList(DateTime startDateFrom, DateTime startDateTo, int calendarId = -1)
        {
            List<CalendarEvent> items = new List<CalendarEvent>();
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("EventCalendarEvents") +
                " WHERE " + DbSyntax.QuoteIdentifier("StartDate") + " >= @StartDateFrom" +
                " AND " + DbSyntax.QuoteIdentifier("StartDate") + " <= @StartDateTo";
            var parms = new List<DbParameter>();
            parms.Add(DbHelper.CreateParameter("@StartDateFrom", startDateFrom));
            parms.Add(DbHelper.CreateParameter("@StartDateTo", startDateTo));
            if (calendarId != -1)
            {
                sql += " AND " + DbSyntax.QuoteIdentifier("CalendarId") + " = @CalendarId";
                parms.Add(DbHelper.CreateParameter("@CalendarId", calendarId));
            }
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql, parms.ToArray()))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<CalendarEvent> GetList(int selectType, DateTime startDateFrom, DateTime repeatUntil, int calendarId = -1)
        {
            List<CalendarEvent> items = new List<CalendarEvent>();
            var parms = new List<DbParameter>();
            parms.Add(DbHelper.CreateParameter("@StartDateFrom", startDateFrom));
            parms.Add(DbHelper.CreateParameter("@RepeatUntil", repeatUntil));

            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("EventCalendarEvents") + " WHERE ";
            if (selectType == EventSelectTypes.SELECT_FOR_MONTH_DISPLAY)
            {
                sql += DbSyntax.QuoteIdentifier("StartDate") + " <= @RepeatUntil AND " +
                    DbSyntax.QuoteIdentifier("RepeatUntil") + " >= @StartDateFrom";
            }
            else
            {
                sql += DbSyntax.QuoteIdentifier("StartDate") + " >= @StartDateFrom AND " +
                    DbSyntax.QuoteIdentifier("StartDate") + " <= @RepeatUntil";
            }

            if (calendarId != -1)
            {
                sql += " AND " + DbSyntax.QuoteIdentifier("CalendarId") + " = @CalendarId";
                parms.Add(DbHelper.CreateParameter("@CalendarId", calendarId));
            }

            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql, parms.ToArray()))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public int Update(CalendarEvent item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("EventCalendarEvents") + " SET " +
                    DbSyntax.QuoteIdentifier("Subject") + " = @Subject, " +
                    DbSyntax.QuoteIdentifier("Message") + " = @Message, " +
                    DbSyntax.QuoteIdentifier("Location") + " = @Location, " +
                    DbSyntax.QuoteIdentifier("CategoryId") + " = @CategoryId, " +
                    DbSyntax.QuoteIdentifier("RecurrenceId") + " = @RecurrenceId, " +
                    DbSyntax.QuoteIdentifier("EndDate") + " = @EndDate, " +
                    DbSyntax.QuoteIdentifier("ReminderBefore") + " = @ReminderBefore, " +
                    DbSyntax.QuoteIdentifier("ReminderTo") + " = @ReminderTo, " +
                    DbSyntax.QuoteIdentifier("RepeatUntil") + " = @RepeatUntil, " +
                    DbSyntax.QuoteIdentifier("StartDate") + " = @StartDate, " +
                    DbSyntax.QuoteIdentifier("LocationId") + " = @LocationId, " +
                    DbSyntax.QuoteIdentifier("Weekdays") + " = @Weekdays, " +
                    DbSyntax.QuoteIdentifier("LastReminderSent") + " = @LastReminderSent, " +
                    DbSyntax.QuoteIdentifier("BookLocation") + " = @BookLocation, " +
                    DbSyntax.QuoteIdentifier("CalendarId") + " = @CalendarId, " +
                    DbSyntax.QuoteIdentifier("TemplateId") + " = @TemplateId, " +
                    DbSyntax.QuoteIdentifier("SendReminderVia") + " = @SendReminderVia" +
                    " WHERE " + DbSyntax.QuoteIdentifier("EventId") + " = @EventId";
                parms = new[] {
                    DbHelper.CreateParameter("@Subject", item.Subject),
                    DbHelper.CreateParameter("@Message", item.Message),
                    DbHelper.CreateParameter("@Location", item.LocationString),
                    DbHelper.CreateParameter("@CategoryId", item.CategoryId),
                    DbHelper.CreateParameter("@RecurrenceId", item.RecurrenceId),
                    DbHelper.CreateParameter("@EndDate", item.EndDate),
                    DbHelper.CreateParameter("@ReminderBefore", item.ReminderBefore),
                    DbHelper.CreateParameter("@ReminderTo", item.ReminderTo),
                    DbHelper.CreateParameter("@RepeatUntil", item.RepeatUntil),
                    DbHelper.CreateParameter("@StartDate", item.StartDate),
                    DbHelper.CreateParameter("@LocationId", item.LocationId),
                    DbHelper.CreateParameter("@Weekdays", item.Weekdays),
                    DbHelper.CreateParameter("@LastReminderSent", item.LastReminderSent),
                    DbHelper.CreateParameter("@BookLocation", item.BookLocation),
                    DbHelper.CreateParameter("@CalendarId", item.CalendarId),
                    DbHelper.CreateParameter("@TemplateId", item.TemplateId),
                    DbHelper.CreateParameter("@SendReminderVia", item.SendReminderVia),
                    DbHelper.CreateParameter("@EventId", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("EventCalendarEvents") + " (" +
                    DbSyntax.QuoteIdentifier("Subject") + ", " +
                    DbSyntax.QuoteIdentifier("Message") + ", " +
                    DbSyntax.QuoteIdentifier("Location") + ", " +
                    DbSyntax.QuoteIdentifier("CategoryId") + ", " +
                    DbSyntax.QuoteIdentifier("RecurrenceId") + ", " +
                    DbSyntax.QuoteIdentifier("EndDate") + ", " +
                    DbSyntax.QuoteIdentifier("ReminderBefore") + ", " +
                    DbSyntax.QuoteIdentifier("ReminderTo") + ", " +
                    DbSyntax.QuoteIdentifier("RepeatUntil") + ", " +
                    DbSyntax.QuoteIdentifier("StartDate") + ", " +
                    DbSyntax.QuoteIdentifier("LocationId") + ", " +
                    DbSyntax.QuoteIdentifier("Weekdays") + ", " +
                    DbSyntax.QuoteIdentifier("LastReminderSent") + ", " +
                    DbSyntax.QuoteIdentifier("BookLocation") + ", " +
                    DbSyntax.QuoteIdentifier("CalendarId") + ", " +
                    DbSyntax.QuoteIdentifier("TemplateId") + ", " +
                    DbSyntax.QuoteIdentifier("SendReminderVia") +
                    ") VALUES (@Subject, @Message, @Location, @CategoryId, @RecurrenceId, @EndDate, @ReminderBefore, @ReminderTo, @RepeatUntil, @StartDate, @LocationId, @Weekdays, @LastReminderSent, @BookLocation, @CalendarId, @TemplateId, @SendReminderVia)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("EventId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Subject", item.Subject),
                    DbHelper.CreateParameter("@Message", item.Message),
                    DbHelper.CreateParameter("@Location", item.LocationString),
                    DbHelper.CreateParameter("@CategoryId", item.CategoryId),
                    DbHelper.CreateParameter("@RecurrenceId", item.RecurrenceId),
                    DbHelper.CreateParameter("@EndDate", item.EndDate),
                    DbHelper.CreateParameter("@ReminderBefore", item.ReminderBefore),
                    DbHelper.CreateParameter("@ReminderTo", item.ReminderTo),
                    DbHelper.CreateParameter("@RepeatUntil", item.RepeatUntil),
                    DbHelper.CreateParameter("@StartDate", item.StartDate),
                    DbHelper.CreateParameter("@LocationId", item.LocationId),
                    DbHelper.CreateParameter("@Weekdays", item.Weekdays),
                    DbHelper.CreateParameter("@LastReminderSent", item.LastReminderSent),
                    DbHelper.CreateParameter("@BookLocation", item.BookLocation),
                    DbHelper.CreateParameter("@CalendarId", item.CalendarId),
                    DbHelper.CreateParameter("@TemplateId", item.TemplateId),
                    DbHelper.CreateParameter("@SendReminderVia", item.SendReminderVia)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        public bool Delete(int eventId)
        {
            var sql = "DELETE FROM " + DbSyntax.QuoteIdentifier("EventCalendarEvents") +
                " WHERE " + DbSyntax.QuoteIdentifier("EventId") + " = @EventId";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@EventId", eventId));

            return true;
        }

        public CalendarEvent From(DbDataReader r)
        {
            CalendarEvent item = new CalendarEvent();
            item.Id = DataUtil.GetId(r["EventId"]);
            item.Subject = r["Subject"].ToString();
            item.Message = r["Message"].ToString();
            item.LocationString = r["Location"].ToString();
            item.CategoryId = DataUtil.GetId(r["CategoryId"]);
            item.RecurrenceId = DataUtil.GetId(r["RecurrenceId"]);
            item.EndDate = (DateTime)r["EndDate"];
            item.ReminderBefore = Convert.ToInt32(r["ReminderBefore"].ToString());
            item.ReminderTo = r["ReminderTo"].ToString();
            item.RepeatUntil = (DateTime)r["RepeatUntil"];
            item.StartDate = (DateTime)r["StartDate"];
            item.LocationId = DataUtil.GetId(r["LocationId"]);
            item.Weekdays = Convert.ToInt32(r["Weekdays"].ToString());
            item.LastReminderSent = (DateTime)r["LastReminderSent"];
            item.BookLocation = Convert.ToInt32(r["BookLocation"].ToString());
            item.CalendarId = DataUtil.GetId(r, "CalendarId");
            item.TemplateId = DataUtil.GetId(r, "TemplateId");
            item.SendReminderVia = DataUtil.GetInt32(r, "SendReminderVia");

            return item;
        }
    }
}
