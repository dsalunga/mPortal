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
    public class IncidentCategoryProvider : GenericSqlDataProviderBase<IncidentCategory>, IIncidentCategoryProvider
    {
        protected override string DeleteProcedure { get { return "IncidentCategory_Del"; } }
        protected override string TableName { get { return "IncidentCategory"; } }

        protected override string IdColumn { get { return "Id"; } }


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
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("IncidentCategory") + " SET " +
                    DbSyntax.QuoteIdentifier("Name") + " = @Name, " +
                    DbSyntax.QuoteIdentifier("GroupId") + " = @GroupId, " +
                    DbSyntax.QuoteIdentifier("Description") + " = @Description, " +
                    DbSyntax.QuoteIdentifier("Rank") + " = @Rank, " +
                    DbSyntax.QuoteIdentifier("InstanceId") + " = @InstanceId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@GroupId", item.GroupId),
                    DbHelper.CreateParameter("@Description", item.Description),
                    DbHelper.CreateParameter("@Rank", item.Rank),
                    DbHelper.CreateParameter("@InstanceId", item.InstanceId),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("IncidentCategory") + " (" +
                    DbSyntax.QuoteIdentifier("Name") + ", " +
                    DbSyntax.QuoteIdentifier("GroupId") + ", " +
                    DbSyntax.QuoteIdentifier("Description") + ", " +
                    DbSyntax.QuoteIdentifier("Rank") + ", " +
                    DbSyntax.QuoteIdentifier("InstanceId") +
                    ") VALUES (@Name, @GroupId, @Description, @Rank, @InstanceId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@Name", item.Name),
                    DbHelper.CreateParameter("@GroupId", item.GroupId),
                    DbHelper.CreateParameter("@Description", item.Description),
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
