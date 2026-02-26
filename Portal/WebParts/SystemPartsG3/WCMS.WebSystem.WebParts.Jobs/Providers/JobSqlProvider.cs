using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Jobs.Providers
{
    public class JobSqlProvider : GenericSqlDataProviderBase<Job>, IJobProvider
    {
        protected override string DeleteProcedure
        {
            get { throw new NotImplementedException(); }
        }

        protected override Job From(IDataReader r, Job source)
        {
            Job item = source ?? new Job();
            item.Id = DataUtil.GetId(r, "Id");
            item.Title = DataUtil.Get(r, "Title");
            item.Description = DataUtil.Get(r, "Description");

            return item;
        }

        protected override string TableName { get { return "Job"; } }


        protected override string IdColumn { get { return "Id"; } }



        protected override string SelectProcedure
        {
            get { return "Job_Get"; }
        }

        public IEnumerable<Job> GetList(string keyword)
        {
            List<Job> items = new List<Job>();

            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("Job");
            var parmList = new List<DbParameter>();
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " WHERE " + DbSyntax.QuoteIdentifier("Title") + " LIKE @Keyword OR " +
                    DbSyntax.QuoteIdentifier("Description") + " LIKE @Keyword";
                parmList.Add(DbHelper.CreateParameter("@Keyword", "%" + keyword + "%"));
            }

            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql, parmList.ToArray()))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }
    }
}
