using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.EventCalendar.Providers
{
    public class CalendarLocationProvider : GenericSqlDataProviderBase<CalendarLocation>
    {
        protected override string DeleteProcedure { get { return "EventCalendarLocation_Del"; } }
        protected override string TableName { get { return "EventCalendarLocations"; } }

        protected override string IdColumn { get { return "LocationId"; } }


        protected override string SelectProcedure { get { return "EventCalendarLocations_Get"; } }
        protected override string IdParameter { get { return "@LocationId"; } }

        protected override CalendarLocation From(IDataReader r, CalendarLocation source)
        {
            CalendarLocation item = source ?? new CalendarLocation();
            item.Id = DataUtil.GetId(r, "LocationId");
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.Bookable = DataUtil.GetInt32(r, "Bookable");

            return item;
        }

        public override int Update(CalendarLocation item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("EventCalendarLocations") + " SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("Bookable") + " = @Bookable" +
                    " WHERE " + DbSyntax.QuoteIdentifier("LocationId") + " = @LocationId";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@Bookable", item.Bookable),
                    DbHelper.CreateParameter("@LocationId", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("EventCalendarLocations") + " (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("Bookable") +
                    ") VALUES (@Name, @Bookable)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("LocationId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@Bookable", item.Bookable)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }
    }
}
