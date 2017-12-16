using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.EventCalendar.Providers
{
    public class CalendarEventProvider
    {
        private const string SQL_GET = "EventCalendarEvents_Get";
        private const string SQL_SET = "EventCalendarEvents_Set";
        private const string SQL_DEL = "EventCalendarEvents_Del";

        public CalendarEvent Get(int eventId)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader(SQL_GET,
                new SqlParameter("@EventId", eventId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public IEnumerable<CalendarEvent> GetList(int calendarId = -1)
        {
            List<CalendarEvent> items = new List<CalendarEvent>();
            using (DbDataReader r = SqlHelper.ExecuteReader(SQL_GET,
                new SqlParameter("@CalendarId", calendarId)))
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
            using (DbDataReader r = SqlHelper.ExecuteReader(SQL_GET,
                new SqlParameter("@CalendarId", calendarId),
                new SqlParameter("@StartDateFrom", startDateFrom),
                new SqlParameter("@StartDateTo", startDateTo)
                ))
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
            using (DbDataReader r = SqlHelper.ExecuteReader(SQL_GET,
                new SqlParameter("@StartDateFrom", startDateFrom),
                new SqlParameter("@RepeatUntil", repeatUntil),
                new SqlParameter("@SelectType", selectType),
                new SqlParameter("@CalendarId", calendarId)
                ))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public int Update(CalendarEvent item)
        {
            object o = SqlHelper.ExecuteScalar(SQL_SET,
                new SqlParameter("@EventId", item.Id),
                new SqlParameter("@Subject", item.Subject),
                new SqlParameter("@Message", item.Message),
                new SqlParameter("@Location", item.LocationString),
                new SqlParameter("@CategoryId", item.CategoryId),
                new SqlParameter("@RecurrenceId", item.RecurrenceId),
                new SqlParameter("@EndDate", item.EndDate),
                new SqlParameter("@ReminderBefore", item.ReminderBefore),
                new SqlParameter("@ReminderTo", item.ReminderTo),
                new SqlParameter("@RepeatUntil", item.RepeatUntil),
                new SqlParameter("@StartDate", item.StartDate),
                new SqlParameter("@LocationId", item.LocationId),
                new SqlParameter("@Weekdays", item.Weekdays),
                new SqlParameter("@LastReminderSent", item.LastReminderSent),
                new SqlParameter("@BookLocation", item.BookLocation),
                new SqlParameter("@CalendarId", item.CalendarId),
                new SqlParameter("@TemplateId", item.TemplateId),
                new SqlParameter("@SendReminderVia", item.SendReminderVia)
            );

            item.Id = DataHelper.GetId(o);
            return item.Id;
        }

        public bool Delete(int eventId)
        {
            SqlHelper.ExecuteNonQuery(SQL_DEL,
                new SqlParameter("@EventId", eventId)
            );

            return true;
        }

        public CalendarEvent From(DbDataReader r)
        {
            CalendarEvent item = new CalendarEvent();
            item.Id = DataHelper.GetId(r["EventId"]);
            item.Subject = r["Subject"].ToString();
            item.Message = r["Message"].ToString();
            item.LocationString = r["Location"].ToString();
            item.CategoryId = DataHelper.GetId(r["CategoryId"]);
            item.RecurrenceId = DataHelper.GetId(r["RecurrenceId"]);
            item.EndDate = (DateTime)r["EndDate"];
            item.ReminderBefore = Convert.ToInt32(r["ReminderBefore"].ToString());
            item.ReminderTo = r["ReminderTo"].ToString();
            item.RepeatUntil = (DateTime)r["RepeatUntil"];
            item.StartDate = (DateTime)r["StartDate"];
            item.LocationId = DataHelper.GetId(r["LocationId"]);
            item.Weekdays = Convert.ToInt32(r["Weekdays"].ToString());
            item.LastReminderSent = (DateTime)r["LastReminderSent"];
            item.BookLocation = Convert.ToInt32(r["BookLocation"].ToString());
            item.CalendarId = DataHelper.GetId(r, "CalendarId");
            item.TemplateId = DataHelper.GetId(r, "TemplateId");
            item.SendReminderVia = DataHelper.GetInt32(r, "SendReminderVia");

            return item;
        }
    }
}
