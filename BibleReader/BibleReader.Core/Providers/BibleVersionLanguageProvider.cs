using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;
using WCMS.Common.Data;

namespace WCMS.BibleReader.Core.Providers
{
    public class BibleVersionLanguageProvider : SqlDataProviderBase<BibleVersionLanguage>
    {
        protected override string SelectProcedure { get { return "BibleVersionLanguage_Get"; } }
        protected override string DeleteProcedure { get { return "BibleVersionLanguage_Del"; } }
        protected override string ConnectionString { get { return connectionString; } }

        private string connectionString;
        public BibleVersionLanguageProvider()
        {
            connectionString = SqlHelper.GetConnectionString(BibleConstants.ConnectionString);
        }

        protected override BibleVersionLanguage From(IDataReader r)
        {
            var item = new BibleVersionLanguage();
            item.Id = DataUtil.GetId(r, "Id");
            item.Name = DataHelper.Get(r, "Name");
            //item.BibleTableName = DataHelper.Get(r, "BibleTableName");
            //item.BookNameCode = DataHelper.GetId(r, "BookNameCode");
            //item.Active = DataHelper.GetInt32(r, "Active");
            //item.ShortName = DataHelper.Get(r, "ShortName");
            //item.OldAndNew = DataHelper.GetInt32(r, "OldAndNew");

            return item;
        }

        public override int Update(BibleVersionLanguage item)
        {
            throw new NotImplementedException();
        }
    }
}
