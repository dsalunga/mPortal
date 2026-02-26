using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using WCMS.Common.Utilities;
using WCMS.Common.Data;

namespace WCMS.BibleReader.Core.Providers
{
    public class BibleVersionProvider : SqlDataProviderBase<BibleVersion>
    {
        protected override string SelectProcedure { get { return "BibleVersion_Get"; } }
        protected override string DeleteProcedure { get { return "BibleVersion_Del"; } }
        protected override string ConnectionString { get { return connectionString; } }

        private string connectionString;
        public BibleVersionProvider()
        {
            connectionString = DbHelper.GetConnectionString(BibleConstants.ConnectionString);
        }

        protected override BibleVersion From(IDataReader r)
        {
            BibleVersion item = new BibleVersion();
            item.Id = DataUtil.GetId(r, "Id");
            item.Name = DataUtil.Get(r, "Name");
            item.BibleTableName = DataUtil.Get(r, "BibleTableName");
            item.BookNameCode = DataUtil.GetId(r, "BookNameCode");
            item.Active = DataUtil.GetInt32(r, "Active");
            item.ShortName = DataUtil.Get(r, "ShortName");
            item.OldAndNew = DataUtil.GetInt32(r, "OldAndNew");
            item.LanguageType = DataUtil.GetInt32(r, "TranslationType");
            item.TranslationType = DataUtil.GetInt32(r, "TranslationType");

            return item;
        }

        public List<BibleVersion> GetList(int langaugeType = -2, int translationType = -2)
        {
            List<BibleVersion> items = new List<BibleVersion>();

            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("BibleVersion") + " WHERE 1=1";
            var parmList = new List<DbParameter>();
            if (langaugeType != -2) { sql += " AND " + DbSyntax.QuoteIdentifier("LanguageType") + " = @LanguageType"; parmList.Add(DbHelper.CreateParameter("@LanguageType", langaugeType)); }
            if (translationType != -2) { sql += " AND " + DbSyntax.QuoteIdentifier("TranslationType") + " = @TranslationType"; parmList.Add(DbHelper.CreateParameter("@TranslationType", translationType)); }

            using (var r = DbHelper.ExecuteReader(connectionString, CommandType.Text, sql, parmList.ToArray()))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public override int Update(BibleVersion item)
        {
            throw new NotImplementedException();
        }
    }
}
