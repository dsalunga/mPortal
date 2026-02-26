using System.Data;
using System.Data.Common;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Incident.Providers
{
    public class IncidentInstanceProvider : GenericSqlDataProviderBase<IncidentInstance>
    {
        protected override string DeleteProcedure { get { return "IncidentInstance_Del"; } }
        protected override string TableName { get { return "IncidentInstance"; } }

        protected override string IdColumn { get { return "Id"; } }


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
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("IncidentInstance") + " SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("IncidentPrefix") + " = @IncidentPrefix, " +
                    DbSyntax.QuoteIdentifier("BaseGroup") + " = @BaseGroup, " +
                    DbSyntax.QuoteIdentifier("SupportGroupPath") + " = @SupportGroupPath, " +
                    DbSyntax.QuoteIdentifier("SLAHighDuration") + " = @SLAHighDuration, " +
                    DbSyntax.QuoteIdentifier("SLALowDuration") + " = @SLALowDuration, " +
                    DbSyntax.QuoteIdentifier("SLANormalDuration") + " = @SLANormalDuration, " +
                    DbSyntax.QuoteIdentifier("SLAWarningPercentage") + " = @SLAWarningPercentage" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@IncidentPrefix", item.IncidentPrefix),
                    DbHelper.CreateParameter("@BaseGroup", item.BaseGroup),
                    DbHelper.CreateParameter("@SupportGroupPath", item.SupportGroupPath),
                    DbHelper.CreateParameter("@SLAHighDuration", item.SLAHighDuration),
                    DbHelper.CreateParameter("@SLALowDuration", item.SLALowDuration),
                    DbHelper.CreateParameter("@SLANormalDuration", item.SLANormalDuration),
                    DbHelper.CreateParameter("@SLAWarningPercentage", item.SLAWarningPercentage),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("IncidentInstance") + " (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("IncidentPrefix") + ", " +
                    DbSyntax.QuoteIdentifier("BaseGroup") + ", " +
                    DbSyntax.QuoteIdentifier("SupportGroupPath") + ", " +
                    DbSyntax.QuoteIdentifier("SLAHighDuration") + ", " +
                    DbSyntax.QuoteIdentifier("SLALowDuration") + ", " +
                    DbSyntax.QuoteIdentifier("SLANormalDuration") + ", " +
                    DbSyntax.QuoteIdentifier("SLAWarningPercentage") +
                    ") VALUES (@Name, @IncidentPrefix, @BaseGroup, @SupportGroupPath, @SLAHighDuration, @SLALowDuration, @SLANormalDuration, @SLAWarningPercentage)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@IncidentPrefix", item.IncidentPrefix),
                    DbHelper.CreateParameter("@BaseGroup", item.BaseGroup),
                    DbHelper.CreateParameter("@SupportGroupPath", item.SupportGroupPath),
                    DbHelper.CreateParameter("@SLAHighDuration", item.SLAHighDuration),
                    DbHelper.CreateParameter("@SLALowDuration", item.SLALowDuration),
                    DbHelper.CreateParameter("@SLANormalDuration", item.SLANormalDuration),
                    DbHelper.CreateParameter("@SLAWarningPercentage", item.SLAWarningPercentage)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }
    }
}
