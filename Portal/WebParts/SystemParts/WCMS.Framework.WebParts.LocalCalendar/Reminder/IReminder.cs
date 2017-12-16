using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public interface IReminder
    {
        bool Send();
        CalendarEvent Event { get; set; }
    }
}
