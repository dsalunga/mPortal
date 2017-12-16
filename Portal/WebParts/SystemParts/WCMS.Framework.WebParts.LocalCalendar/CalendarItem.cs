using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public class CalendarItem : ParameterizedWebObject, ISelfManager
    {
        private static IEventCalendarProvider _provider;

        public int SiteId { get; set; }

        static CalendarItem()
        {
            _provider = new EventCalendarProvider();
        }

        public CalendarItem()
        {
            SiteId = -1;
        }

        public static IEventCalendarProvider Provider
        {
            get { return _provider; }
        }

        #region ISelfManager Members

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        #endregion

        public override int OBJECT_ID
        {
            get { return 105; }
        }
    }
}
