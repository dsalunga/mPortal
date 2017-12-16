using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.EventCalendar.Providers
{
    public class CalendarRecurrenceProvider
    {
        public IEnumerable<CalendarRecurrence> GetList()
        {
            List<CalendarRecurrence> items = new List<CalendarRecurrence>();

            using (DbDataReader r = SqlHelper.ExecuteReader("EventCalendarRecurrences_Get"))
            {
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        items.Add(this.From(r));
                    }
                }
            }

            return items;
        }

        public CalendarRecurrence From(DbDataReader r)
        {
            CalendarRecurrence item = new CalendarRecurrence();
            item.Id = DataHelper.GetId(r["RecurrenceId"]);
            item.Name = r["Name"].ToString();
            item.Rank = Convert.ToInt32(r["Rank"].ToString());

            return item;
        }
    }
}
