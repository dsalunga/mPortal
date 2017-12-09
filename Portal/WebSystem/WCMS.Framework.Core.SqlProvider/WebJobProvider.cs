using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            SqlHelper.ExecuteNonQuery("WebJob_Del",
                new SqlParameter("@Id", id));

            return true;
        }

        public WebJob Get(int id)
        {
            using (var r = SqlHelper.ExecuteReader("WebJob_Get",
                new SqlParameter("@Id", id)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public WebJob Get(string name)
        {
            using (var r = SqlHelper.ExecuteReader("WebJob_Get",
                new SqlParameter("@Name", name)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        private WebJob From(SqlDataReader r)
        {
            WebJob item = new WebJob();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.RecurrenceId = DataHelper.GetInt32(r, "RecurrenceId");
            item.Weekdays = DataHelper.GetInt32(r, "Weekdays");
            item.OccursEvery = DataHelper.GetInt32(r, "OccursEvery");
            item.ExecutionStartDate = DataHelper.GetDateTime(r, "ExecutionStartDate");
            item.ExecutionEndDate = DataHelper.GetDateTime(r, "ExecutionEndDate");
            item.ExecutionStatus = DataHelper.GetInt32(r, "ExecutionStatus");
            item.ExecutionMessage = DataHelper.Get(r, "ExecutionMessage");
            item.Enabled = DataHelper.GetInt32(r, "Enabled");
            item.TypeName = DataHelper.Get(r, "TypeName");
            item.StartDate = DataHelper.GetDateTime(r, "StartDate");
            item.Description = DataHelper.Get(r, WebColumns.Description);

            return item;
        }

        public WebJob Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebJob> GetList()
        {
            List<WebJob> items = new List<WebJob>();

            using (var r = SqlHelper.ExecuteReader("WebJob_Get"))
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
            object obj = SqlHelper.ExecuteScalar("WebJob_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@RecurrenceId", item.RecurrenceId),
                new SqlParameter("@Weekdays", item.Weekdays),
                new SqlParameter("@OccursEvery", item.OccursEvery),
                new SqlParameter("@ExecutionStartDate", item.ExecutionStartDate),
                new SqlParameter("@ExecutionEndDate", item.ExecutionEndDate),
                new SqlParameter("@ExecutionStatus", item.ExecutionStatus),
                new SqlParameter("@ExecutionMessage", item.ExecutionMessage),
                new SqlParameter("@Enabled", item.Enabled),
                new SqlParameter("@TypeName", item.TypeName),
                new SqlParameter("@StartDate", item.StartDate),
                new SqlParameter("@Description", item.Description)
            );

            item.Id = DataHelper.GetId(obj);
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
