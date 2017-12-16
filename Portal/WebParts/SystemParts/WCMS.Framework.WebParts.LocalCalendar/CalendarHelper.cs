using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public abstract class CalendarHelper
    {
        //public static DateTime ParseTicks(string dateTicks)
        //{
        //    long ticks = -1;
        //    long.TryParse(dateTicks, out ticks);
        //    return ticks > 0 ? new DateTime(ticks) : DateTime.Now;
        //}

        //public string BuildUrl(CalendarEvent eventItem, bool isAbsolute = false)
        //{
        //    var calendar = eventItem.Calendar;

        //    QueryParser query = new QueryParser(HttpContext.Current);
        //    query.Remove("Date");
        //    query.SetCmd("Event");

        //    query.BasePath = WebHelper.CombineAddress(WConfig.BaseAddress, query.BasePath);

        //    return query.BuildQuery();
        //}

        public static DateTime GetEventEndDate(CalendarEvent item, DateTime startDate)
        {
            var interval = item.EndDate - item.StartDate;
            return startDate.Add(interval);
        }

        public static string BuildEventTimeString(CalendarEvent item, DateTime startDate)
        {
            var endDate = CalendarHelper.GetEventEndDate(item, startDate);
            var durationString = DateTimeHelper.TimeSpanToString(endDate - startDate);

            StringBuilder sb = new StringBuilder();
            sb.Append(startDate.ToShortTimeString());

            if (startDate != endDate)
            {
                sb.AppendFormat(" - {0}", endDate.ToShortTimeString());
                sb.Append(startDate.Date != endDate.Date ? string.Format(", ends on {0}", endDate.ToString("dd MMMM")) : "");
                sb.Append(!string.IsNullOrEmpty(durationString) ? string.Format(" ({0})", durationString) : "");
            }

            return sb.ToString();
        }
    }
}
