using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.WebSystem.WebParts.EventCalendar.Providers;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public class CalendarRecurrence
    {
        private static CalendarRecurrenceProvider _provider = new CalendarRecurrenceProvider();

        private int _recurrenceId = -1;
        public int Id
        {
            get { return _recurrenceId; }
            set { _recurrenceId = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _rank = 0;
        public int Rank
        {
            get { return _rank; }
            set { _rank = value; }
        }

        public static IEnumerable<CalendarRecurrence> GetList()
        {
            return _provider.GetList();
        }
    }
}
