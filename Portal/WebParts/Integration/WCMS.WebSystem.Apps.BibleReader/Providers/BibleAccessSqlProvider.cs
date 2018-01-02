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
    public class BibleAccessSqlProvider : GenericSqlDataProviderBase<BibleAccess>, IBibleAccessProvider
    {
        protected override string DeleteProcedure { get { return "BibleReaderAccess_Del"; } }

        protected override BibleAccess From(IDataReader r, BibleAccess source)
        {
            var item = source ?? new BibleAccess();

            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.UserId = DataUtil.GetId(r, WebColumns.UserId);
            item.AppAccessCount = DataUtil.GetInt32(r, "AppAccessCount");
            item.LastAccessed = DataUtil.GetDateTime(r, "LastAccessed");

            return item;
        }

        protected override string SelectProcedure { get { return "BibleReaderAccess_Get"; } }

        public override int Update(BibleAccess item)
        {
            var o = SqlHelper.ExecuteScalar("BibleReaderAccess_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@UserId", item.UserId),
                new SqlParameter("@AppAccessCount", item.AppAccessCount),
                new SqlParameter("@LastAccessed", item.LastAccessed));

            return UpdatePostProcess(item, o);
        }

        public new BibleAccess Get(int userId)
        {
            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@UserId", userId)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }
    }
}
