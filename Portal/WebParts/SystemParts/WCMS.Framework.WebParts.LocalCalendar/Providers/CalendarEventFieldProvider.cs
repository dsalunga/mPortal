using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.EventCalendar.Providers
{
    public class CalendarEventFieldProvider
    {
        private static IDataProvider _provider;

        static CalendarEventFieldProvider()
        {
            _provider = WebObject.ResolveProvider(typeof(CalendarEventField));
        }

        public CalendarEventField Get(int id)
        {
            return _provider.Get<CalendarEventField>(id);
        }

        public IEnumerable<CalendarEventField> GetList()
        {
            return _provider.GetList<CalendarEventField>();
        }

        public int Update(CalendarEventField item)
        {
            return _provider.Update<CalendarEventField>(item);
        }

        public bool Delete(int id)
        {
            return _provider.Delete<CalendarEventField>(id);
        }
    }
}
