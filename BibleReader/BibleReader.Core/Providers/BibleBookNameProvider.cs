using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Data;
using WCMS.Common.Utilities;

namespace WCMS.BibleReader.Core.Providers
{
    public class BibleBookNameProvider : SqlDataProviderBase<BibleBookName>
    {
        protected override string SelectProcedure { get { return "BibleBookName_Get"; } }
        protected override string DeleteProcedure { get { return "BibleBookName_Del"; } }
        protected override string ConnectionString { get { return connectionString; } }
        
        private string connectionString;
        public BibleBookNameProvider()
        {
            connectionString = SqlHelper.GetConnectionString(BibleConstants.ConnectionString);
        }

        public List<BibleBookName> GetList(int bookNameCode)
        {
            List<BibleBookName> items = new List<BibleBookName>();
            using (var r = SqlHelper.ExecuteReader(connectionString, SelectProcedure,
                new SqlParameter("@BookNameCode", bookNameCode)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public BibleBookName Get(int bookNameCode, int bookCode)
        {
            using (var r = SqlHelper.ExecuteReader(connectionString, SelectProcedure,
                new SqlParameter("@BookNameCode", bookNameCode),
                new SqlParameter("@BookCode", bookCode)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        protected override BibleBookName From(IDataReader r)
        {
            BibleBookName item = new BibleBookName();
            item.Id = DataUtil.GetId(r, "Id");
            item.BookNameCode = DataUtil.GetId(r, "BookNameCode");
            item.BookCode = DataUtil.GetId(r, "BookCode");
            item.Name = DataHelper.Get(r, "Name");
            item.MaxChapter = DataUtil.GetInt32(r, "MaxChapter");
            item.ShortName = DataHelper.Get(r, "ShortName");

            return item;
        }

        public override int Update(BibleBookName item)
        {
            throw new NotImplementedException();
        }
    }
}
