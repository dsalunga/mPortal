using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;

using WCMS.Framework.Diagnostics;

namespace WCMS.Framework.Core.SqlProvider.Diagnostics
{
    public class EventLogProvider : GenericSqlDataProviderBase<EventLog>, IEventLogProvider
    {
        public bool Delete(DateTime eventDate)
        {
            DbHelper.ExecuteNonQuery("EventLog_Del",
                DbHelper.CreateParameter("@EventDate", eventDate));

            return true;
        }

        protected override EventLog From(IDataReader r, EventLog source)
        {
            var item = source ?? new EventLog();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.UserId = DataUtil.GetId(r, WebColumns.UserId);
            item.EventDate = DataUtil.GetDateTime(r, "EventDate");
            item.Content = DataUtil.Get(r, "Content");
            item.EventName = DataUtil.Get(r, "EventName");
            item.IPAddress = DataUtil.Get(r, "IPAddress");

            return item;
        }

        public IEnumerable<EventLog> GetList(DateTime eventDate, int userId = -1)
        {
            var items = new List<EventLog>();
            using (var r = DbHelper.ExecuteReader("EventLog_Get",
                DbHelper.CreateParameter("@EventDate", eventDate),
                DbHelper.CreateParameter("@UserId", userId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public override int Update(EventLog item)
        {
            var obj = DbHelper.ExecuteScalar("EventLog_Set",
                DbHelper.CreateParameter("@Id", item.Id),
                DbHelper.CreateParameter("@EventDate", item.EventDate),
                DbHelper.CreateParameter("@Content", item.Content),
                DbHelper.CreateParameter("@UserId", item.UserId),
                DbHelper.CreateParameter("@EventName", item.EventName),
                DbHelper.CreateParameter("@IPAddress", item.IPAddress));

            item.Id = DataUtil.GetId(obj);
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
