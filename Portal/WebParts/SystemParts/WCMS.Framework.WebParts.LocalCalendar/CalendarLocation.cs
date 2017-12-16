using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.EventCalendar.Providers;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public class CalendarLocation : NamedWebObject, ISelfManager
    {
        private static CalendarLocationProvider _provider;

        static CalendarLocation()
        {
            _provider = new CalendarLocationProvider();
        }

        public CalendarLocation()
        {
            IsBookable = true;
        }

        public int Bookable { get; set; }

        public bool IsBookable
        {
            get { return Bookable == 1; }
            set { Bookable = value ? 1 : 0; }
        }

        public bool IsAvailable(DateTime fromDate, DateTime toDate, CalendarEvent currEvent)
        {
            if (fromDate == WConstants.DateTimeMinValue || toDate == WConstants.DateTimeMinValue)
                return true;

            var events = CalendarEvent.GetList(fromDate, toDate);
            var now = DateTime.Now;

            events = (from e in events
                      where e.LocationId > 0 && e.LocationId == this.Id &&
                            e.Id != currEvent.Id &&
                            e.BookLocation == 1 && 
                            DateTimeHelper.IsWithin(fromDate, toDate, e.GetNextOccurence(currEvent.StartDate > now ? currEvent.StartDate : now)) &&
                            e.Location.IsBookable
                      select e).ToList();

            return events.Count() == 0;
        }

        public static IEnumerable<CalendarLocation> GetList()
        {
            return _provider.GetList();
        }

        public static CalendarLocation Get(int locationId)
        {
            return _provider.Get(locationId);
        }

        public override int OBJECT_ID
        {
            get { return 36; }
        }

        public static CalendarLocationProvider Provider
        {
            get { return _provider; }
        }

        #region ISelfManager Members

        public bool Delete()
        {
            return _provider.Delete(Id);
        }

        public int Update()
        {
            return _provider.Update(this);
        }

        #endregion
    }
}
