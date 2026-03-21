using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Incident.Providers
{
    public class IncidentInstanceProvider : GenericSqlDataProviderBase<IncidentInstance>
    {
        protected override string DeleteProcedure { get { return "IncidentInstance_Del"; } }
        protected override string SelectProcedure { get { return "IncidentInstance_Get"; } }

        protected override IncidentInstance From(IDataReader r, IncidentInstance source)
        {
            var item = source ?? new IncidentInstance();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.Name = DataUtil.Get(r, WebColumns.Name);
            item.IncidentPrefix = DataUtil.Get(r, "IncidentPrefix");
            item.BaseGroup = DataUtil.Get(r, "BaseGroup");
            item.SupportGroupPath = DataUtil.Get(r, "SupportGroupPath");
            item.SLAHighDuration = DataUtil.GetInt32(r, "SLAHighDuration");
            item.SLALowDuration = DataUtil.GetInt32(r, "SLALowDuration");
            item.SLANormalDuration = DataUtil.GetInt32(r, "SLANormalDuration");
            item.SLAWarningPercentage = DataUtil.GetDouble(r, "SLAWarningPercentage");
            return item;
        }

        public override int Update(IncidentInstance item)
        {
            var obj = SqlHelper.ExecuteScalar("IncidentInstance_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@IncidentPrefix", item.IncidentPrefix),
                new SqlParameter("@BaseGroup", item.BaseGroup),
                new SqlParameter("@SupportGroupPath", item.SupportGroupPath),
                new SqlParameter("@SLAHighDuration", item.SLAHighDuration),
                new SqlParameter("@SLALowDuration", item.SLALowDuration),
                new SqlParameter("@SLANormalDuration", item.SLANormalDuration),
                new SqlParameter("@SLAWarningPercentage", item.SLAWarningPercentage));
            item.Id = DataUtil.GetId(obj);
            return item.Id;
        }
    }
}
