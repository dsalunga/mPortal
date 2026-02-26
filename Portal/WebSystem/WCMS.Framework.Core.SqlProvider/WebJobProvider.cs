using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

using WCMS.Framework;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebJobProvider : IWebJobProvider
    {
        #region IDataProvider<WebJob> Members

        public bool Delete(int id)
        {
            var sql = "DELETE FROM WebJob WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", id));

            return true;
        }

        public WebJob Get(int id)
        {
            var sql = "SELECT * FROM WebJob WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Id", id)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public WebJob Get(string name)
        {
            var sql = "SELECT * FROM WebJob WHERE " + DbSyntax.QuoteIdentifier("Name") + " = @Name";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Name", name)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        private WebJob From(DbDataReader r)
        {
            WebJob item = new WebJob();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.RecurrenceId = DataUtil.GetInt32(r, "RecurrenceId");
            item.Weekdays = DataUtil.GetInt32(r, "Weekdays");
            item.OccursEvery = DataUtil.GetInt32(r, "OccursEvery");
            item.ExecutionStartDate = DataUtil.GetDateTime(r, "ExecutionStartDate");
            item.ExecutionEndDate = DataUtil.GetDateTime(r, "ExecutionEndDate");
            item.ExecutionStatus = DataUtil.GetInt32(r, "ExecutionStatus");
            item.ExecutionMessage = DataUtil.Get(r, "ExecutionMessage");
            item.Enabled = DataUtil.GetInt32(r, "Enabled");
            item.TypeName = DataUtil.Get(r, "TypeName");
            item.StartDate = DataUtil.GetDateTime(r, "StartDate");
            item.Description = DataUtil.Get(r, WebColumns.Description);

            return item;
        }

        public WebJob Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebJob> GetList()
        {
            List<WebJob> items = new List<WebJob>();

            var sql = "SELECT * FROM WebJob";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IEnumerable<WebJob> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        public int Update(WebJob item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE WebJob SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("RecurrenceId") + " = @RecurrenceId, " +
                    DbSyntax.QuoteIdentifier("Weekdays") + " = @Weekdays, " +
                    DbSyntax.QuoteIdentifier("OccursEvery") + " = @OccursEvery, " +
                    DbSyntax.QuoteIdentifier("ExecutionStartDate") + " = @ExecutionStartDate, " +
                    DbSyntax.QuoteIdentifier("ExecutionEndDate") + " = @ExecutionEndDate, " +
                    DbSyntax.QuoteIdentifier("ExecutionStatus") + " = @ExecutionStatus, " +
                    DbSyntax.QuoteIdentifier("ExecutionMessage") + " = @ExecutionMessage, " +
                    DbSyntax.QuoteIdentifier("Enabled") + " = @Enabled, " +
                    DbSyntax.QuoteIdentifier("TypeName") + " = @TypeName, " +
                    DbSyntax.QuoteIdentifier("StartDate") + " = @StartDate, " +
                    DbSyntax.QuoteIdentifier("Description") + " = @Description" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@RecurrenceId", item.RecurrenceId),
                    DbHelper.CreateParameter("@Weekdays", item.Weekdays),
                    DbHelper.CreateParameter("@OccursEvery", item.OccursEvery),
                    DbHelper.CreateParameter("@ExecutionStartDate", item.ExecutionStartDate),
                    DbHelper.CreateParameter("@ExecutionEndDate", item.ExecutionEndDate),
                    DbHelper.CreateParameter("@ExecutionStatus", item.ExecutionStatus),
                    DbHelper.CreateParameter("@ExecutionMessage", item.ExecutionMessage),
                    DbHelper.CreateParameter("@Enabled", item.Enabled),
                    DbHelper.CreateParameter("@TypeName", item.TypeName),
                    DbHelper.CreateParameter("@StartDate", item.StartDate),
                    DbHelper.CreateParameter("@Description", item.Description),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO WebJob (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("RecurrenceId") + ", " +
                    DbSyntax.QuoteIdentifier("Weekdays") + ", " +
                    DbSyntax.QuoteIdentifier("OccursEvery") + ", " +
                    DbSyntax.QuoteIdentifier("ExecutionStartDate") + ", " +
                    DbSyntax.QuoteIdentifier("ExecutionEndDate") + ", " +
                    DbSyntax.QuoteIdentifier("ExecutionStatus") + ", " +
                    DbSyntax.QuoteIdentifier("ExecutionMessage") + ", " +
                    DbSyntax.QuoteIdentifier("Enabled") + ", " +
                    DbSyntax.QuoteIdentifier("TypeName") + ", " +
                    DbSyntax.QuoteIdentifier("StartDate") + ", " +
                    DbSyntax.QuoteIdentifier("Description") +
                    ") VALUES (@Name, @RecurrenceId, @Weekdays, @OccursEvery, @ExecutionStartDate, @ExecutionEndDate, @ExecutionStatus, @ExecutionMessage, @Enabled, @TypeName, @StartDate, @Description)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@RecurrenceId", item.RecurrenceId),
                    DbHelper.CreateParameter("@Weekdays", item.Weekdays),
                    DbHelper.CreateParameter("@OccursEvery", item.OccursEvery),
                    DbHelper.CreateParameter("@ExecutionStartDate", item.ExecutionStartDate),
                    DbHelper.CreateParameter("@ExecutionEndDate", item.ExecutionEndDate),
                    DbHelper.CreateParameter("@ExecutionStatus", item.ExecutionStatus),
                    DbHelper.CreateParameter("@ExecutionMessage", item.ExecutionMessage),
                    DbHelper.CreateParameter("@Enabled", item.Enabled),
                    DbHelper.CreateParameter("@TypeName", item.TypeName),
                    DbHelper.CreateParameter("@StartDate", item.StartDate),
                    DbHelper.CreateParameter("@Description", item.Description)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public WebJob Refresh(WebJob item)
        {
            throw new NotImplementedException();
        }
    }
}
