using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;

using System.Globalization;
using System.Text;

namespace WCMS.Common.Utilities
{
    public abstract class DateTimeHelper : TimeUtil
    {

    }

    /// <summary>
    /// Summary description for DateTimeHelper
    /// </summary>
    public abstract class TimeUtil
    {
        public static string[] GetMonthNames()
        {
            return CultureInfo.InvariantCulture.DateTimeFormat.MonthNames;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="month">An integer value from 1 to 13 (empty)</param>
        /// <returns></returns>
        public static string GetMonthName(int month = -1)
        {
            if (month <= 0 || month >= 14)
                month = DateTime.Now.Month;

            return CultureInfo.InvariantCulture.DateTimeFormat.MonthNames[month - 1];
        }

        public static string ToCompactDateTime(DateTime date)
        {
            if (date.TimeOfDay.Ticks == 0) return date.ToShortDateString();
            else return date.ToString();
        }

        public static string ToCompactDateTime(DateTime date, string dateFormat, string dateTimeFormat)
        {
            if (date.TimeOfDay.Ticks == 0) return date.ToString(dateFormat);
            else return date.ToString(dateTimeFormat);
        }

        public static string ToCompactTime(DateTime date)
        {
            return date.ToString("HH:mm");
        }

        public static string ToCompactTime(string dateString)
        {
            return DataHelper.GetDateTime(dateString).ToString("HH:mm");
        }

        public static DateTime ParseTicks(string dateTicks)
        {
            long ticks = -1;
            if (!string.IsNullOrEmpty(dateTicks))
                long.TryParse(dateTicks, out ticks);

            return ticks > 0 ? new DateTime(ticks) : DateTime.Now;
        }

        /// <summary>
        /// Considers only if the refDate is less than or equal to dateToCheck and within range of checkInterval
        /// </summary>
        /// <param name="refDate">Normally the current date</param>
        /// <param name="dateToCheck"></param>
        /// <param name="checkInterval"></param>
        /// <returns></returns>
        public static bool IsOccurring(DateTime refDate, DateTime dateToCheck, TimeSpan checkInterval)
        {
            if (dateToCheck >= refDate)
            {
                TimeSpan delta = dateToCheck.Subtract(refDate);

                //if (delta.Ticks < 0)
                //    delta = delta.Negate();

                if (checkInterval >= delta)
                    return true;
            }

            return false;
        }

        public static bool IsOccurring(DateTime refDate, DateTime dateToCheck, int checkIntervalSeconds)
        {
            return IsOccurring(refDate, dateToCheck, new TimeSpan(0, 0, checkIntervalSeconds));
        }

        public static bool IsWithin(DateTime dateFrom, DateTime dateTo, DateTime dateToCheck)
        {
            return dateToCheck >= dateFrom && dateToCheck <= dateTo;
        }

        public static DateTime GetNextWeekdayDate(DateTime refDate, DateTime dateToCheck, int weekdays)
        {
            int weekday = WeekdaysEnum.GetWeekday(refDate);
            int currWeekday = weekday;
            DateTime currRefDate = refDate;

            do
            {
                if ((weekdays & WeekdaysEnum.GetWeekday(currRefDate)) > 0)
                    return currRefDate.Date.Add(dateToCheck.TimeOfDay);
                else
                    currRefDate = currRefDate.AddDays(1);

                currWeekday = WeekdaysEnum.GetWeekday(currRefDate);
            }
            while (currWeekday != weekday);

            throw new Exception("Invalid Weekdays");
        }

        public static string TimeIntervalToString(int number, TimeInterval unit)
        {
            switch (unit)
            {
                case TimeInterval.Second:
                    return number + (number > 1 ? " seconds" : " second");

                case TimeInterval.Minute:
                    return number + (number > 1 ? " minutes" : " minute");

                case TimeInterval.Hour:
                    return number + (number > 1 ? " hours" : " hour");
            }

            return string.Empty;
        }

        public static string TimeSpanToString(TimeSpan ts, bool includeSeconds = false, bool shortNames = false)
        {
            StringBuilder sb = new StringBuilder();

            if (ts.Days > 0)
                sb.AppendFormat("{0} {1}, ", ts.Days, GetIntervalText(TimeInterval.Day, ts.Days, shortNames));

            if (ts.Hours > 0)
                sb.AppendFormat("{0} {1}, ", ts.Hours, GetIntervalText(TimeInterval.Hour, ts.Hours, shortNames));

            if (ts.Minutes > 0)
                sb.AppendFormat("{0} {1}, ", ts.Minutes, GetIntervalText(TimeInterval.Minute, +ts.Minutes, shortNames));

            if (includeSeconds && ts.Seconds > 0)
                sb.AppendFormat("{0} {1}, ", ts.Seconds, GetIntervalText(TimeInterval.Second, +ts.Seconds, shortNames));

            return sb.ToString().TrimEnd(',', ' ');
        }

        public static string GetIntervalText(TimeInterval intervalUnit, int interval, bool shortNames = false)
        {
            switch (intervalUnit)
            {
                case TimeInterval.Second:
                    return interval > 1 || interval == 0 ? shortNames ? "secs" : "seconds" : shortNames ? "sec" : "second";

                case TimeInterval.Minute:
                    return interval > 1 || interval == 0 ? shortNames ? "mins" : "minutes" : shortNames ? "min" : "minute";

                case TimeInterval.Hour:
                    return interval > 1 || interval == 0 ? shortNames ? "hrs" : "hours" : shortNames ? "hr" : "hour";

                case TimeInterval.Day:
                    return interval > 1 || interval == 0 ? "days" : "day";
            }

            return "";
        }

        public static string ToISOString(DateTime dateTime)
        {
            return dateTime == null ? "" : dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }
    }

    public enum TimeInterval
    {
        Second,
        Minute,
        Hour,
        Day
    }

    #region Constants and Enumerations

    public class WeekdaysEnum
    {
        private static Dictionary<int, string> _kv;

        public static Dictionary<int, string> KeyPairs
        {
            get { return _kv; }
        }

        static WeekdaysEnum()
        {
            _kv = new Dictionary<int, string>
                      {
                          {0, "None"},
                          {1, "Sunday"},
                          {2, "Monday"},
                          {4, "Tuesday"},
                          {8, "Wednesday"},
                          {16, "Thursday"},
                          {32, "Friday"},
                          {64, "Saturday"}
                      };
        }

        public static int GetWeekday(DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Sunday: return Sunday;
                case DayOfWeek.Monday: return Monday;
                case DayOfWeek.Tuesday: return Tuesday;
                case DayOfWeek.Wednesday: return Wednesday;
                case DayOfWeek.Thursday: return Thursday;
                case DayOfWeek.Friday: return Friday;
                case DayOfWeek.Saturday: return Saturday;
            }

            return Sunday;
        }

        public static string GetName(int i)
        {
            if (_kv.ContainsKey(i))
                return _kv[i];

            return string.Empty;

            //switch (i)
            //{
            //    case Sunday:
            //        return "Sunday";

            //    case Monday:
            //        return "Monday";

            //    case Tuesday:
            //        return "Tuesday";

            //    case Wednesday:
            //        return "Wednesday";

            //    case Thursday:
            //        return "Thursday";

            //    case Friday:
            //        return "Friday";

            //    case Saturday:
            //        return "Saturday";
            //}

            //return null;
        }

        public static string ToShortString(int i)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var kv in _kv)
            {
                if (kv.Key > 0 && ((i & kv.Key) > 0))
                {
                    sb.Append(kv.Value.Substring(0, 2) + " ");
                    i -= kv.Key;
                }
            }

            return sb.ToString().Trim();
        }

        public const int None = 0;
        public const int Sunday = 1;
        public const int Monday = 2;
        public const int Tuesday = 4;
        public const int Wednesday = 8;
        public const int Thursday = 16;
        public const int Friday = 32;
        public const int Saturday = 64;
    }

    #endregion
}