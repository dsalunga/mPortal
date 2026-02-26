using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.EventCalendar.Providers
{
    public class CalendarCategoryProvider : IDataProvider<CalendarCategory>
    {
        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CalendarCategory> GetList()
        {
            List<CalendarCategory> items = new List<CalendarCategory>();

            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("EventCalendarCategories");
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public CalendarCategory Get(int categoryId)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("EventCalendarCategories") +
                " WHERE " + DbSyntax.QuoteIdentifier("CategoryId") + " = @CategoryId";
            using (DbDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@CategoryId", categoryId)))
            {
                if (r.Read())
                    return this.From(r);
            }

            return null;
        }

        public CalendarCategory From(DbDataReader r)
        {
            CalendarCategory item = new CalendarCategory();
            item.Id = DataUtil.GetId(r["CategoryId"]);
            item.Name = r["Name"].ToString();
            item.TemplateId = DataUtil.GetId(r["TemplateId"]);

            return item;
        }

        #region IDataProvider<CalendarCategory> Members

        public bool Delete(int id)
        {
            var sql = "DELETE FROM " + DbSyntax.QuoteIdentifier("EventCalendarCategories") +
                " WHERE " + DbSyntax.QuoteIdentifier("CategoryId") + " = @Id";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", id));

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
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("EventCalendarCategories") + " SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("TemplateId") + " = @TemplateId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("CategoryId") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@TemplateId", item.TemplateId),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("EventCalendarCategories") + " (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("TemplateId") +
                    ") VALUES (@Name, @TemplateId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("CategoryId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@TemplateId", item.TemplateId)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        #endregion


        public CalendarCategory Refresh(CalendarCategory item)
        {
            throw new NotImplementedException();
        }
    }
}
