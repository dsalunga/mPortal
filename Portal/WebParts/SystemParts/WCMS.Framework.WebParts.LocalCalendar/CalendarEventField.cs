using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.EventCalendar.Providers;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public class CalendarEventField : INameWebObject
    {
        private static CalendarEventFieldProvider _provider;

        static CalendarEventField()
        {
            _provider = new CalendarEventFieldProvider();
        }

        public CalendarEventField()
        {
            Id = WConstants.NULL_ID;
        }

        [ObjectColumn(IsPrimaryKey=true)]
        public int Id { get; set; }

        [ObjectColumn]
        public string Name { get; set; }

        [ObjectColumn]
        public string FieldString { get; set; }

        public int OBJECT_ID
        {
            get
            {
                return WConstants.NULL_ID;
            }
        }

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }


        public static CalendarEventField Get(int id)
        {
            return _provider.Get(id);
        }

        public static IEnumerable<CalendarEventField> GetList()
        {
            return _provider.GetList();
        }
    }
}
