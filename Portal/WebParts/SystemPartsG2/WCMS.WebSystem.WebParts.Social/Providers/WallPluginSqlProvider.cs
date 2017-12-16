using System.Data;
using System.Data.SqlClient;
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
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.EventTypeId = DataHelper.GetId(r, "EventTypeId");
            item.FileName = DataHelper.Get(r, "FileName");
            item.TypeName = DataHelper.Get(r, "TypeName");

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
