using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.EventCalendar.Providers
{
    public class CalendarTemplateProvider : IDataProvider<CalendarTemplate>
    {
        public CalendarTemplate Get(int templateId)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("EventCalendarTemplates") +
                " WHERE " + DbSyntax.QuoteIdentifier("TemplateId") + " = @TemplateId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@TemplateId", templateId)))
            {
                if (r.Read())
                    return this.From(r);
            }

            return null;
        }

        public IEnumerable<CalendarTemplate> GetList()
        {
            var items = new List<CalendarTemplate>();

            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("EventCalendarTemplates");
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (r.Read())
                    items.Add(this.From(r));
            }

            return items;
        }

        public int Update(CalendarTemplate item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("EventCalendarTemplates") + " SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("ForeColor") + " = @ForeColor, " +
                    DbSyntax.QuoteIdentifier("BackColor") + " = @BackColor, " +
                    DbSyntax.QuoteIdentifier("ReminderHtml") + " = @ReminderHtml, " +
                    DbSyntax.QuoteIdentifier("SmsContent") + " = @SmsContent" +
                    " WHERE " + DbSyntax.QuoteIdentifier("TemplateId") + " = @TemplateId";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@ForeColor", item.ForeColor),
                    DbHelper.CreateParameter("@BackColor", item.BackColor),
                    DbHelper.CreateParameter("@ReminderHtml", item.ReminderHtml),
                    DbHelper.CreateParameter("@SmsContent", item.SmsContent),
                    DbHelper.CreateParameter("@TemplateId", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("EventCalendarTemplates") + " (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("ForeColor") + ", " +
                    DbSyntax.QuoteIdentifier("BackColor") + ", " +
                    DbSyntax.QuoteIdentifier("ReminderHtml") + ", " +
                    DbSyntax.QuoteIdentifier("SmsContent") +
                    ") VALUES (@Name, @ForeColor, @BackColor, @ReminderHtml, @SmsContent)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("TemplateId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@ForeColor", item.ForeColor),
                    DbHelper.CreateParameter("@BackColor", item.BackColor),
                    DbHelper.CreateParameter("@ReminderHtml", item.ReminderHtml),
                    DbHelper.CreateParameter("@SmsContent", item.SmsContent)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        private CalendarTemplate From(DbDataReader r)
        {
            CalendarTemplate item = new CalendarTemplate();
            item.Id = DataUtil.GetId(r["TemplateId"]);
            item.Name = r["Name"].ToString();
            item.BackColor = r["BackColor"].ToString();
            item.ForeColor = r["ForeColor"].ToString();
            item.ReminderHtml = r["ReminderHtml"].ToString();
            item.SmsContent = DataUtil.Get(r, "SmsContent");

            return item;
        }

        #region IDataProvider<CalendarTemplate> Members

        public bool Delete(int id)
        {
            var sql = "DELETE FROM " + DbSyntax.QuoteIdentifier("EventCalendarTemplates") +
                " WHERE " + DbSyntax.QuoteIdentifier("TemplateId") + " = @Id";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", id));

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
