using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Incident.Providers
{
    public class IncidentTypeProvider : GenericSqlDataProviderBase<IncidentType>, IIncidentTypeProvider
    {
        protected override string DeleteProcedure { get { return "IncidentType_Del"; } }
        protected override string SelectProcedure { get { return "IncidentType_Get"; } }

        protected override IncidentType From(IDataReader r, IncidentType source)
        {
            var item = source ?? new IncidentType();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.FollowStdSLA = DataUtil.GetInt32(r, "FollowStdSLA");
            item.Rank = DataUtil.GetInt32(r, WebColumns.Rank);
            item.InstanceId = DataUtil.GetInt32(r, "InstanceId");

            return item;
        }

        public override int Update(IncidentType item)
        {
            var obj = SqlHelper.ExecuteScalar("IncidentType_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@FollowStdSLA", item.FollowStdSLA),
                new SqlParameter("@InstanceId", item.InstanceId),
                new SqlParameter("@Rank", item.Rank));

            item.Id = DataUtil.GetId(obj);
            return item.Id;
        }
    }
}
