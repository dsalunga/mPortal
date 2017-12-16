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
    public class IncidentCategoryProvider : GenericSqlDataProviderBase<IncidentCategory>, IIncidentCategoryProvider
    {
        protected override string DeleteProcedure { get { return "IncidentCategory_Del"; } }
        protected override string SelectProcedure { get { return "IncidentCategory_Get"; } }

        protected override IncidentCategory From(IDataReader r, IncidentCategory source)
        {
            var item = source ?? new IncidentCategory();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.GroupId = DataUtil.GetId(r, WebColumns.GroupId);
            item.Description = DataUtil.Get(r, WebColumns.Description);
            item.Rank = DataUtil.GetInt32(r, WebColumns.Rank);
            item.InstanceId = DataUtil.GetInt32(r, "InstanceId");

            return item;
        }

        public override int Update(IncidentCategory item)
        {
            var obj = SqlHelper.ExecuteScalar("IncidentCategory_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@GroupId", item.GroupId),
                new SqlParameter("@Description", item.Description),
                new SqlParameter("@Rank", item.Rank),
                new SqlParameter("@InstanceId", item.InstanceId));

            item.Id = DataUtil.GetId(obj);
            return item.Id;
        }
    }
}
