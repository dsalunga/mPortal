using System.Data;
using System.Data.Common;
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

        protected override string TableName { get { return "WallPlugin"; } }


        protected override string IdColumn { get { return "Id"; } }



        protected override string SelectProcedure { get { return "WallPlugin_Get"; } }

        public WallPlugin GetByEventType(int typeId)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("WallPlugin") + " WHERE " + DbSyntax.QuoteIdentifier("EventTypeId") + " = @EventTypeId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@EventTypeId", typeId)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }
    }
}
