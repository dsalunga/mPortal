using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public interface IEventCalendarProvider : IDataProvider<CalendarItem>
    {
        IEnumerable<CalendarItem> GetList(int siteId = -2);
    }
}
