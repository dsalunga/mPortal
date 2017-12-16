using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Net;
using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.EventCalendar.Providers;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public class CalendarEvent : IWebObject
    {
        public const int ID = 28;

        private static CalendarEventProvider _provider;

        static CalendarEvent()
        {
            _provider = new CalendarEventProvider();
        }

        public CalendarEvent()
        {
            Id = -1;
            CategoryId = -1;
            RecurrenceId = -1;
            LocationId = -1;
            LastReminderSent = ((DateTime)SqlDateTime.MinValue).AddYears(1);
            ReminderBefore = 0;
            BookLocation = 1;
            CalendarId = -1;
            TemplateId = -1;
            SendReminderVia = MessageSendVia.EmailAndSms;
        }

        public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string LocationString { get; set; }
        public int CalendarId { get; set; }
        public int TemplateId { get; set; }
        public int SendReminderVia { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime LastReminderSent { get; set; }
        public int CategoryId { get; set; }
        public DateTime RepeatUntil { get; set; }
        public string ReminderTo { get; set; }
        public int ReminderBefore { get; set; }
        public int RecurrenceId { get; set; }
        public int LocationId { get; set; }
        public int Weekdays { get; set; }
        public int BookLocation { get; set; }

        public CalendarCategory Category
        {
            get { return CalendarCategory.Get(CategoryId); }
        }

        public string CategoryName
        {
            get { return Category.Name; }
        }

        public string FinalLocation
        {
            get
            {
                if (LocationId > 0)
                    return CalendarLocation.Get(LocationId).Name;
                return LocationString;
            }
        }

        public string Recurrence
        {
            get { return RecurrenceType.GetName(RecurrenceId); }
        }

        public CalendarLocation Location
        {
            get
            {
                if (LocationId > 0)
                    return CalendarLocation.Get(LocationId);

                return null;
            }
        }

        public CalendarTemplate Template
        {
            get
            {
                if (TemplateId > 0)
                {
                    return CalendarTemplate.Get(TemplateId);
                }
                else if (CategoryId > 0)
                {
                    var category = Category;
                    if (category != null)
                        return category.Template;
                }

                return null;
            }
        }

        public CalendarItem Calendar
        {
            get
            {
                if (CalendarId > 0)
                    return CalendarItem.Provider.Get(CalendarId);

                return null;
            }
        }

        public DateTime GetNextReminder()
        {
            return GetNextReminder(DateTime.Now);
        }

        public DateTime GetNextReminder(DateTime refDateNow)
        {
            if (this.ReminderBefore < 0) return new DateTime(0);

            DateTime nextReminder = GetNextOccurence(refDateNow).AddMinutes(this.ReminderBefore * -1);
            return nextReminder >= refDateNow ? nextReminder : new DateTime(0);
        }

        public DateTime GetNextOccurence()
        {
            return GetNextOccurence(DateTime.Now);
        }

        public bool IsRecurring
        {
            get { return RecurrenceId > 0; }
        }

        public DateTime GetNextOccurence(DateTime refDateNow)
        {
            if (this.StartDate >= refDateNow)
                return this.StartDate;

            switch (this.RecurrenceId)
            {
                case RecurrenceType.None:
                    break;

                case RecurrenceType.Daily:
                    {
                        DateTime nextOccurrence = refDateNow.Date.Add(this.StartDate.TimeOfDay);
                        if (refDateNow > nextOccurrence)
                            nextOccurrence = nextOccurrence.AddDays(1);

                        if (nextOccurrence <= this.RepeatUntil)
                            return nextOccurrence; // Today's occurence

                        //else if (nextOccurrence.AddDays(1) <= this.RepeatUntil)
                        //    return nextOccurrence.AddDays(1); // Tomorrow's occurence

                        break;
                    }

                case RecurrenceType.Weekly:
                    {
                        if (this.Weekdays > 0)
                        {
                            DateTime nextOccurrence = refDateNow.Date.Add(this.StartDate.TimeOfDay);
                            if (refDateNow > nextOccurrence)
                                nextOccurrence = nextOccurrence.AddDays(1);

                            while (nextOccurrence <= this.RepeatUntil)
                            {
                                if ((this.Weekdays & WeekdaysEnum.GetWeekday(nextOccurrence)) > 0)
                                    return nextOccurrence;

                                nextOccurrence = nextOccurrence.AddDays(1);
                            }
                        }

                        break;
                    }

                case RecurrenceType.Monthly:
                    {
                        DateTime nextOccurrence = (new DateTime(refDateNow.Year, refDateNow.Month, 1)
                            .AddDays(this.StartDate.Day - 1))
                            .Add(this.StartDate.TimeOfDay);
                        if (refDateNow > nextOccurrence)
                            nextOccurrence = nextOccurrence.AddMonths(1);

                        if (this.RepeatUntil >= nextOccurrence)
                            return nextOccurrence;

                        //nextOccurrence = nextOccurrence.AddMonths(1); 

                        /*(new DateTime(startDate.Year, startDate.Month + 1, 1)
                                              .AddDays(startDate.Day - 1)
                                             ).Add(startDate.TimeOfDay); */

                        //if (nextOccurrence >= this.RepeatUntil)
                        //    return nextOccurrence;

                        break;
                    }

                case RecurrenceType.Yearly:
                    {
                        DateTime nextOccurrence = (new DateTime(refDateNow.Year, this.StartDate.Month, 1)
                            .AddDays(this.StartDate.Day - 1))
                            .Add(this.StartDate.TimeOfDay);

                        if (refDateNow > nextOccurrence)
                            nextOccurrence = nextOccurrence.AddYears(1);

                        if (nextOccurrence <= this.RepeatUntil)
                            return nextOccurrence;

                        //nextOccurrence = nextOccurrence.AddYears(1);
                        //if (nextOccurrence >= this.RepeatUntil)
                        //    return nextOccurrence;

                        break;
                    }
            }

            return WConstants.DateTimeMinValue; // new DateTime(1900, 1, 1);
        }

        public TimeSpan Length
        {
            get
            {
                if (EndDate > StartDate)
                    return new TimeSpan(EndDate.Subtract(StartDate).Ticks);
                else
                    return new TimeSpan();
            }
        }


        public int Update()
        {
            return _provider.Update(this);
        }


        public static CalendarEvent Get(int eventId)
        {
            return _provider.Get(eventId);
        }

        public static IEnumerable<CalendarEvent> GetList(int calendarId = -1)
        {
            return _provider.GetList(calendarId);
        }

        public static IEnumerable<CalendarEvent> GetList(DateTime startDateFrom, DateTime startDateTo, int calendarId = -1)
        {
            return _provider.GetList(startDateFrom, startDateTo, calendarId);
        }

        public static IEnumerable<CalendarEvent> GetCalendarEvents(DateTime month, int calendarId = -1)
        {
            DateTime startDateFrom = new DateTime(month.Year, month.Month, 1);
            startDateFrom = startDateFrom.AddMonths(-1);

            DateTime repeatUntil = new DateTime(month.Year, month.Month, 1);
            repeatUntil = repeatUntil.AddMonths(2).AddDays(-1);

            return _provider.GetList(EventSelectTypes.SELECT_FOR_MONTH_DISPLAY, startDateFrom, repeatUntil, calendarId);
        }

        public static IEnumerable<CalendarEvent> GetReminders(DateTime currentDateTime, int minuteMargin, int calendarId = -1)
        {
            // Event occurence selection range for fetching reminders (max future time to check)
            int maxMinutes = WebRegistry.SelectNode("/Apps/EventCalendar/Reminder.MaxMinutes").ValueInt32;

            DateTime currentPlusMax = currentDateTime.AddMinutes(maxMinutes);
            DateTime currDate = currentDateTime;
            DateTime currDateMarginPlus = currDate.AddMinutes(minuteMargin);
            DateTime currDateMarginMinus = currDate.AddMinutes(minuteMargin * -1);
            var events = _provider.GetList(EventSelectTypes.SELECT_REMINDERS, currDateMarginMinus, currentPlusMax, calendarId);

            return from evnt in events
                   where
                       // Check last sent date
                       !(currDateMarginPlus > evnt.LastReminderSent
                           && currDateMarginMinus < evnt.LastReminderSent)

                       //&& ev.LastReminderSent < currDate.AddMinutes(ev.ReminderBefore*-1))
                       //&& (ev.LastReminderSent < currDate.AddMinutes(ev.ReminderBefore*-1))

                       &&
                       (
                           (evnt.RecurrenceId < 1 && IsWithinReminderMargin(currDate, evnt, minuteMargin))
                           || (evnt.RepeatUntil.Date >= currDate
                               && IsWithinReminderMargin(currDate, evnt, minuteMargin))
                       )
                   orderby evnt.ReminderBefore
                   select evnt;
        }

        private static bool IsWithinReminderMargin(DateTime currDate, CalendarEvent evnt, int minuteMargin)
        {
            // Validate if event is still active...(?)

            DateTime eventDate = currDate.AddMinutes(evnt.ReminderBefore);
            DateTime reminderPlus = eventDate.AddMinutes(minuteMargin);
            DateTime reminderMinus = eventDate.AddMinutes(minuteMargin * -1);
            DateTime eventReminderStart = evnt.StartDate.AddMinutes(evnt.ReminderBefore * -1); // NotRepeating, check start of reminder
            DateTime startDate;

            switch (evnt.RecurrenceId)
            {
                case RecurrenceType.None:
                    startDate = evnt.StartDate;

                    return //(evnt.LastReminderSent < eventReminderStart || 
                            (startDate >= reminderMinus && startDate <= reminderPlus);

                case RecurrenceType.Daily:
                    startDate = eventDate.Date.Add(evnt.StartDate.TimeOfDay);
                    return (startDate >= reminderMinus && startDate <= reminderPlus);

                case RecurrenceType.Weekly:
                    startDate = eventDate.Date.Add(evnt.StartDate.TimeOfDay);
                    return ((evnt.Weekdays & WeekdaysEnum.GetWeekday(eventDate)) > 0)
                        && (startDate >= reminderMinus && startDate <= reminderPlus);

                case RecurrenceType.Monthly:
                    startDate = (new DateTime(eventDate.Year, eventDate.Month, 1)
                                                .AddDays(evnt.StartDate.Day - 1)
                                         ).Add(evnt.StartDate.TimeOfDay);
                    return (startDate >= reminderMinus && startDate <= reminderPlus);

                case RecurrenceType.Yearly:
                    startDate = new DateTime(eventDate.Year, evnt.StartDate.Month, evnt.StartDate.Day)
                                            .Add(evnt.StartDate.TimeOfDay);
                    return (startDate >= reminderMinus && startDate <= reminderPlus);
            }

            return false;
        }

        public static bool Delete(int eventId)
        {
            return _provider.Delete(eventId);
        }

        public static IEnumerable<CalendarEvent> GetDayEventsFromSets(IEnumerable<CalendarEvent> events, DateTime date)
        {
            date = date.Date;

            return (from ev in events
                    where
                        date >= ev.StartDate.Date
                        &&
                        ev.GetNextOccurence(date).Date == date
                    orderby ev.StartDate.TimeOfDay
                    select ev);
        }

        #region IWebObject Members


        public int OBJECT_ID
        {
            get { return ID; }
        }

        #endregion
    }
}
