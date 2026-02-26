using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Incident.Providers
{
    public class IncidentTypeProvider : GenericSqlDataProviderBase<IncidentType>, IIncidentTypeProvider
    {
        protected override string DeleteProcedure { get { return "IncidentType_Del"; } }
        protected override string TableName { get { return "IncidentType"; } }

        protected override string IdColumn { get { return "Id"; } }


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
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("IncidentType") + " SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("FollowStdSLA") + " = @FollowStdSLA, " +
                    DbSyntax.QuoteIdentifier("Rank") + " = @Rank, " +
                    DbSyntax.QuoteIdentifier("InstanceId") + " = @InstanceId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@FollowStdSLA", item.FollowStdSLA),
                    DbHelper.CreateParameter("@Rank", item.Rank),
                    DbHelper.CreateParameter("@InstanceId", item.InstanceId),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("IncidentType") + " (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("FollowStdSLA") + ", " +
                    DbSyntax.QuoteIdentifier("Rank") + ", " +
                    DbSyntax.QuoteIdentifier("InstanceId") +
                    ") VALUES (@Name, @FollowStdSLA, @Rank, @InstanceId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@FollowStdSLA", item.FollowStdSLA),
                    DbHelper.CreateParameter("@Rank", item.Rank),
                    DbHelper.CreateParameter("@InstanceId", item.InstanceId)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }
    }
}
