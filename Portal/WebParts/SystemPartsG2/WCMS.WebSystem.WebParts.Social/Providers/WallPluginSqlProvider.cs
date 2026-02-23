using System.Data;
using Microsoft.Data.SqlClient;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Social.Providers
{
    class WallPluginSqlProvider : GenericSqlDataProviderBase<WallPlugin>, IWallPluginProvider
    {
        protected override string DeleteProcedure { get { return ""; } }

        protected override WallPlugin From(IDataReader r, WallPlugin source)
        {
            WallPlugin item = source ?? new WallPlugin();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.EventTypeId = DataUtil.GetId(r, "EventTypeId");
            item.FileName = DataUtil.Get(r, "FileName");
            item.TypeName = DataUtil.Get(r, "TypeName");

            return item;
        }

        protected override string SelectProcedure { get { return "WallPlugin_Get"; } }

        public WallPlugin GetByEventType(int typeId)
        {
            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@EventTypeId", typeId)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }
    }
}
