using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.EventCalendar.Providers
{
    public class CalendarLocationProvider : GenericSqlDataProviderBase<CalendarLocation>
    {
        protected override string DeleteProcedure { get { return "EventCalendarLocation_Del"; } }
        protected override string SelectProcedure { get { return "EventCalendarLocations_Get"; } }
        protected override string IdParameter { get { return "@LocationId"; } }

        protected override CalendarLocation From(IDataReader r, CalendarLocation source)
        {
            CalendarLocation item = source ?? new CalendarLocation();
            item.Id = DataHelper.GetId(r, "LocationId");
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.Bookable = DataHelper.GetInt32(r, "Bookable");

            return item;
        }

        public override int Update(CalendarLocation item)
        {
            var obj = SqlHelper.ExecuteScalar("EventCalendarLocations_Set",
                new SqlParameter("@LocationId", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@Bookable", item.Bookable));

            item.Id = DataHelper.GetId(obj);
            return item.Id;
        }
    }
}
