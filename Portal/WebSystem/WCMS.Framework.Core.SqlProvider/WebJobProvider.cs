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
            DbHelper.ExecuteNonQuery("WebJob_Del",
                DbHelper.CreateParameter("@Id", id));

            return true;
        }

        public WebJob Get(int id)
        {
            using (var r = DbHelper.ExecuteReader("WebJob_Get",
                DbHelper.CreateParameter("@Id", id)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public WebJob Get(string name)
        {
            using (var r = DbHelper.ExecuteReader("WebJob_Get",
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

            using (var r = DbHelper.ExecuteReader("WebJob_Get"))
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
            object obj = DbHelper.ExecuteScalar("WebJob_Set",
                DbHelper.CreateParameter("@Id", item.Id),
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
            );

            item.Id = DataUtil.GetId(obj);
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
