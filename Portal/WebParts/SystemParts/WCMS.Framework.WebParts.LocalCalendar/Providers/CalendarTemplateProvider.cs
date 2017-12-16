using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.EventCalendar.Providers
{
    public class CalendarTemplateProvider : IDataProvider<CalendarTemplate>
    {
        public const string GET_SQL = "EventCalendarTemplates_Get";
        public const string SET_SQL = "EventCalendarTemplates_Set";
        
        public CalendarTemplate Get(int templateId)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader(GET_SQL,
                new SqlParameter("@TemplateId", templateId)))
            {
                if (r.Read())
                    return this.From(r);
            }

            return null;
        }

        public IEnumerable<CalendarTemplate> GetList()
        {
            var items = new List<CalendarTemplate>();

            using (DbDataReader r = SqlHelper.ExecuteReader(GET_SQL))
            {
                while (r.Read())
                    items.Add(this.From(r));
            }

            return items;
        }

        public int Update(CalendarTemplate item)
        {
            object o = SqlHelper.ExecuteReader(SET_SQL,
                new SqlParameter("@TemplateId", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@ForeColor", item.ForeColor),
                new SqlParameter("@BackColor", item.BackColor),
                new SqlParameter("@ReminderHtml", item.ReminderHtml),
                new SqlParameter("@SmsContent", item.SmsContent)
            );

            item.Id = DataHelper.GetId(o);
            return item.Id;
        }

        private CalendarTemplate From(DbDataReader r)
        {
            CalendarTemplate item = new CalendarTemplate();
            item.Id = DataHelper.GetId(r["TemplateId"]);
            item.Name = r["Name"].ToString();
            item.BackColor = r["BackColor"].ToString();
            item.ForeColor = r["ForeColor"].ToString();
            item.ReminderHtml = r["ReminderHtml"].ToString();
            item.SmsContent = DataHelper.Get(r, "SmsContent");

            return item;
        }

        #region IDataProvider<CalendarTemplate> Members

        public bool Delete(int id)
        {
            SqlHelper.ExecuteNonQuery("EventCalendarTemplate_Del",
                new SqlParameter("@Id", id));

            return true;
        }

        public CalendarTemplate Get(params Framework.Core.QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public IEnumerable<CalendarTemplate> GetList(params Framework.Core.QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public CalendarTemplate Refresh(CalendarTemplate item)
        {
            throw new NotImplementedException();
        }
    }
}
