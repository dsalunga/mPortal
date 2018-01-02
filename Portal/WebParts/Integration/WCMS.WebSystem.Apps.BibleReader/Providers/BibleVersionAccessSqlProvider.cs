using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.BibleReader.Providers
{
    public class BibleVersionAccessSqlProvider : GenericSqlDataProviderBase<BibleVersionAccess>, IBibleVersionAccessProvider
    {
        protected override string DeleteProcedure { get { return "BibleReaderVersionAccess_Del"; } }

        protected override BibleVersionAccess From(IDataReader r, BibleVersionAccess source)
        {
            var item = source ?? new BibleVersionAccess();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.BibleAccessId = DataUtil.GetId(r, "BibleAccessId");
            item.BibleVersionId = DataUtil.GetId(r, "BibleVersionId");
            item.VersionAccessCount = DataUtil.GetInt32(r, "VersionAccessCount");
            item.BibleVersionName = DataHelper.Get(r, "BibleVersionName");
            item.LastAccessed = DataUtil.GetDateTime(r, "LastAccessed");

            return item;
        }

        protected override string SelectProcedure { get { return "BibleReaderVersionAccess_Get"; } }

        public override int Update(BibleVersionAccess item)
        {
            var obj = SqlHelper.ExecuteScalar("BibleReaderVersionAccess_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@BibleAccessId", item.BibleAccessId),
                new SqlParameter("@BibleVersionId", item.BibleVersionId),
                new SqlParameter("@BibleVersionName", item.BibleVersionName),
                new SqlParameter("@VersionAccessCount", item.VersionAccessCount),
                new SqlParameter("@LastAccessed", item.LastAccessed));

            return UpdatePostProcess(item, obj);
        }

        public IEnumerable<BibleVersionAccess> GetList(int accessId)
        {
            List<BibleVersionAccess> items = new List<BibleVersionAccess>();

            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@BibleAccessId", accessId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }
    }
}
