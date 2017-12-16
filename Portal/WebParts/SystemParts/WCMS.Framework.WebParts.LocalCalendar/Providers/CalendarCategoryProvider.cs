using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.EventCalendar.Providers
{
    public class CalendarCategoryProvider : IDataProvider<CalendarCategory>
    {
        private const string SQL_GET = "EventCalendarCategories_Get";

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CalendarCategory> GetList()
        {
            List<CalendarCategory> items = new List<CalendarCategory>();

            using (DbDataReader r = SqlHelper.ExecuteReader(SQL_GET))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public CalendarCategory Get(int categoryId)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader(SQL_GET,
                new SqlParameter("@CategoryId", categoryId)))
            {
                if (r.Read())
                    return this.From(r);
            }

            return null;
        }

        public CalendarCategory From(DbDataReader r)
        {
            CalendarCategory item = new CalendarCategory();
            item.Id = DataHelper.GetId(r["CategoryId"]);
            item.Name = r["Name"].ToString();
            item.TemplateId = DataHelper.GetId(r["TemplateId"]);

            return item;
        }

        #region IDataProvider<CalendarCategory> Members

        public bool Delete(int id)
        {
            SqlHelper.ExecuteNonQuery("EventCalendarCategory_Del",
                new SqlParameter("@Id", id));

            return true;
        }

        public CalendarCategory Get(params Framework.Core.QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public IEnumerable<CalendarCategory> GetList(params Framework.Core.QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int Update(CalendarCategory item)
        {
            var obj = SqlHelper.ExecuteScalar("EventCalendarCategory_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@TemplateId", item.TemplateId));

            item.Id = DataHelper.GetId(obj);
            return item.Id;
        }

        #endregion


        public CalendarCategory Refresh(CalendarCategory item)
        {
            throw new NotImplementedException();
        }
    }
}
