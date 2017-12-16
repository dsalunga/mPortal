using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public class EventCalendarProvider : IEventCalendarProvider
    {
        #region IDataProvider<CalendarItem> Members

        public bool Delete(int id)
        {
            SqlHelper.ExecuteNonQuery("EventCalendar_Del",
                new SqlParameter("@Id", id));

            return true;
        }

        public CalendarItem Get(int id)
        {
            using (var r = SqlHelper.ExecuteReader("EventCalendar_Get",
                new SqlParameter("@Id", id)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        private CalendarItem From(SqlDataReader r)
        {
            CalendarItem item = new CalendarItem();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.SiteId = DataHelper.GetId(r, WebColumns.SiteId);

            return item;
        }

        public CalendarItem Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CalendarItem> GetList()
        {
            List<CalendarItem> items = new List<CalendarItem>();

            using (var r = SqlHelper.ExecuteReader("EventCalendar_Get"))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<CalendarItem> GetList(int siteId = -2)
        {
            List<CalendarItem> items = new List<CalendarItem>();

            using (var r = SqlHelper.ExecuteReader("EventCalendar_Get",
                    new SqlParameter("@SiteId", siteId)
                ))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<CalendarItem> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public int Update(CalendarItem item)
        {
            var obj = SqlHelper.ExecuteScalar("EventCalendar_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@SiteId", item.SiteId)
            );

            item.Id = DataHelper.GetId(obj);
            return item.Id;
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public CalendarItem Refresh(CalendarItem item)
        {
            throw new NotImplementedException();
        }
    }
}
