using System;
using System.Data;
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

            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("EventCalendarRecurrences");
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql))
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
            item.Id = DataUtil.GetId(r["RecurrenceId"]);
            item.Name = r["Name"].ToString();
            item.Rank = Convert.ToInt32(r["Rank"].ToString());

            return item;
        }
    }
}
