using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

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

        protected override string SelectProcedure
        {
            get { return "Job_Get"; }
        }

        public IEnumerable<Job> GetList(string keyword)
        {
            List<Job> items = new List<Job>();

            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@Keyword", keyword)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;


            //SqlHelper.ExecuteReader(CommandType.Text, "SELECT * FROM Job");
            //SqlHelper.ExecuteReader("Job_Get",
            //    new SqlParameter("@param", 123),
            //    new SqlParameter();
        }
    }
}
