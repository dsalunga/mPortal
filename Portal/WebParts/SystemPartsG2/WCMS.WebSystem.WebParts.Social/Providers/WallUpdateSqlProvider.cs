using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Social.Providers
{
    public class WallUpdateSqlProvider : GenericSqlDataProviderBase<WallUpdate>, IWallUpdateProvider
    {
        protected override string DeleteProcedure { get { return "WallUpdate_Del"; } }
        protected override string SelectProcedure { get { return "WallUpdate_Get"; } }

        protected override WallUpdate From(IDataReader r, WallUpdate source)
        {
            WallUpdate item = source ?? new WallUpdate();
            item.Id = DataHelper.GetId(r, WebColumns.Id);
            item.UpdateObjectId = DataHelper.GetId(r, "UpdateObjectId");
            item.UpdateRecordId = DataHelper.GetId(r, "UpdateRecordId");
            item.ByObjectId = DataHelper.GetId(r, "ByObjectId");
            item.ByRecordId = DataHelper.GetId(r, "ByRecordId");
            item.Content = DataHelper.Get(r, "Content");
            item.UpdateDate = DataHelper.GetDateTime(r, "UpdateDate");
            item.EventTypeId = DataHelper.GetInt32(r, "EventTypeId");

            return item;
        }

        public override int Update(WallUpdate item)
        {
            var obj = SqlHelper.ExecuteScalar("WallUpdate_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@UpdateObjectId", item.UpdateObjectId),
                new SqlParameter("@UpdateRecordId", item.UpdateRecordId),
                new SqlParameter("@ByObjectId", item.ByObjectId),
                new SqlParameter("@ByRecordId", item.ByRecordId),
                new SqlParameter("@Content", item.Content),
                new SqlParameter("@UpdateDate", item.UpdateDate),
                new SqlParameter("@EventTypeId", item.EventTypeId)
            );

            return UpdatePostProcess(item, obj);
        }

        public IEnumerable<WallUpdate> GetList(int updateObjectId = -2, int updateRecordId = -2, int byObjectId = -2, int byRecordId = -2, 
            int eventTypeId = -2, DateTime? updateDateStart = null, DateTime? updateDateEnd = null)
        {
            List<WallUpdate> items = new List<WallUpdate>();

            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@UpdateObjectId", updateObjectId),
                new SqlParameter("@UpdateRecordId", updateRecordId),
                new SqlParameter("@ByObjectId", byObjectId),
                new SqlParameter("@ByRecordId", byRecordId),
                new SqlParameter("@EventTypeId", eventTypeId),
                new SqlParameter("@UpdateDateStart", updateDateStart),
                new SqlParameter("@UpdateDateEnd", updateDateEnd)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }
    }
}
