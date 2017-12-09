using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

using WCMS.Framework.Diagnostics;

namespace WCMS.Framework.Core.SqlProvider.Diagnostics
{
    public class EventLogProvider : GenericSqlDataProviderBase<EventLog>, IEventLogProvider
    {
        public bool Delete(DateTime eventDate)
        {
            SqlHelper.ExecuteNonQuery("EventLog_Del",
                new SqlParameter("@EventDate", eventDate));

            return true;
        }

        protected override EventLog From(IDataReader r, EventLog source)
        {
            var item = source ?? new EventLog();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.UserId = DataHelper.GetId(r, WebColumns.UserId);
            item.EventDate = DataHelper.GetDateTime(r, "EventDate");
            item.Content = DataHelper.Get(r, "Content");
            item.EventName = DataHelper.Get(r, "EventName");
            item.IPAddress = DataHelper.Get(r, "IPAddress");

            return item;
        }

        public IEnumerable<EventLog> GetList(DateTime eventDate, int userId = -1)
        {
            var items = new List<EventLog>();
            using (var r = SqlHelper.ExecuteReader("EventLog_Get",
                new SqlParameter("@EventDate", eventDate),
                new SqlParameter("@UserId", userId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public override int Update(EventLog item)
        {
            var obj = SqlHelper.ExecuteScalar("EventLog_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@EventDate", item.EventDate),
                new SqlParameter("@Content", item.Content),
                new SqlParameter("@UserId", item.UserId),
                new SqlParameter("@EventName", item.EventName),
                new SqlParameter("@IPAddress", item.IPAddress));

            item.Id = DataHelper.GetId(obj);
            return item.Id;
        }

        protected override string SelectProcedure
        {
            get { return "EventLog_Get"; }
        }

        protected override string DeleteProcedure
        {
            get { return "EventLog_Del"; }
        }
    }
}
