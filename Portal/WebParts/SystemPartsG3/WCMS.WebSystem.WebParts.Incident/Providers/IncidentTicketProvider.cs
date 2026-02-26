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
    public class IncidentTicketProvider : GenericSqlDataProviderBase<IncidentTicket>, IIncidentTicketProvider
    {
        protected override string DeleteProcedure { get { return "IncidentTicket_Del"; } }
        protected override string TableName { get { return "IncidentTicket"; } }

        protected override string IdColumn { get { return "Id"; } }


        protected override string SelectProcedure { get { return "IncidentTicket_Get"; } }

        protected override IncidentTicket From(IDataReader r, IncidentTicket source)
        {
            var item = source ?? new IncidentTicket();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.UserId = DataUtil.GetId(r, WebColumns.UserId);
            item.DateCreated = DataUtil.GetDateTime(r, "DateCreated");
            item.CategoryId = DataUtil.GetId(r, "CategoryId");
            item.Description = DataUtil.Get(r, WebColumns.Description);
            item.Urgency = DataUtil.GetInt32(r, "Urgency");
            item.Status = DataUtil.GetInt32(r, "Status");
            item.AssignedGroupId = DataUtil.GetId(r, "AssignedGroupId");
            item.AssignedUserId = DataUtil.GetId(r, "AssignedUserId");
            item.TicketGuid = DataUtil.Get(r, "TicketGuid");
            item.DateClosed = DataUtil.GetDateTime(r, "DateClosed");
            item.SubmitterId = DataUtil.GetId(r, "SubmitterId");
            item.TypeId = DataUtil.GetId(r, "TypeId");
            item.ETA = DataUtil.GetDateTime(r, "ETA");
            item.NotifyAlso = DataUtil.Get(r, "NotifyAlso");
            item.InstanceId = DataUtil.GetInt32(r, "InstanceId");

            return item;
        }

        public override int Update(IncidentTicket item)
        {
            string sql;
            DbParameter[] parms;

            if (item.Id > 0)
            {
                sql = "UPDATE " + DbSyntax.QuoteIdentifier("IncidentTicket") + " SET " +
                    DbSyntax.QuoteIdentifier("UserId") + " = @UserId, " +
                    DbSyntax.QuoteIdentifier("DateCreated") + " = @DateCreated, " +
                    DbSyntax.QuoteIdentifier("CategoryId") + " = @CategoryId, " +
                    DbSyntax.QuoteIdentifier("Description") + " = @Description, " +
                    DbSyntax.QuoteIdentifier("Urgency") + " = @Urgency, " +
                    DbSyntax.QuoteIdentifier("Status") + " = @Status, " +
                    DbSyntax.QuoteIdentifier("AssignedGroupId") + " = @AssignedGroupId, " +
                    DbSyntax.QuoteIdentifier("AssignedUserId") + " = @AssignedUserId, " +
                    DbSyntax.QuoteIdentifier("TicketGuid") + " = @TicketGuid, " +
                    DbSyntax.QuoteIdentifier("DateClosed") + " = @DateClosed, " +
                    DbSyntax.QuoteIdentifier("SubmitterId") + " = @SubmitterId, " +
                    DbSyntax.QuoteIdentifier("TypeId") + " = @TypeId, " +
                    DbSyntax.QuoteIdentifier("ETA") + " = @ETA, " +
                    DbSyntax.QuoteIdentifier("NotifyAlso") + " = @NotifyAlso, " +
                    DbSyntax.QuoteIdentifier("InstanceId") + " = @InstanceId" +
                    " WHERE " + DbSyntax.QuoteIdentifier("Id") + " = @Id";
                parms = new[] {
                    DbHelper.CreateParameter("@UserId", item.UserId),
                    DbHelper.CreateParameter("@DateCreated", item.DateCreated),
                    DbHelper.CreateParameter("@CategoryId", item.CategoryId),
                    DbHelper.CreateParameter("@Description", item.Description),
                    DbHelper.CreateParameter("@Urgency", item.Urgency),
                    DbHelper.CreateParameter("@Status", item.Status),
                    DbHelper.CreateParameter("@AssignedGroupId", item.AssignedGroupId),
                    DbHelper.CreateParameter("@AssignedUserId", item.AssignedUserId),
                    DbHelper.CreateParameter("@TicketGuid", item.TicketGuid),
                    DbHelper.CreateParameter("@DateClosed", item.DateClosed),
                    DbHelper.CreateParameter("@SubmitterId", item.SubmitterId),
                    DbHelper.CreateParameter("@TypeId", item.TypeId),
                    DbHelper.CreateParameter("@ETA", item.ETA),
                    DbHelper.CreateParameter("@NotifyAlso", item.NotifyAlso),
                    DbHelper.CreateParameter("@InstanceId", item.InstanceId),
                    DbHelper.CreateParameter("@Id", item.Id)
                };
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
            }
            else
            {
                sql = "INSERT INTO " + DbSyntax.QuoteIdentifier("IncidentTicket") + " (" +
                    DbSyntax.QuoteIdentifier("UserId") + ", " +
                    DbSyntax.QuoteIdentifier("DateCreated") + ", " +
                    DbSyntax.QuoteIdentifier("CategoryId") + ", " +
                    DbSyntax.QuoteIdentifier("Description") + ", " +
                    DbSyntax.QuoteIdentifier("Urgency") + ", " +
                    DbSyntax.QuoteIdentifier("Status") + ", " +
                    DbSyntax.QuoteIdentifier("AssignedGroupId") + ", " +
                    DbSyntax.QuoteIdentifier("AssignedUserId") + ", " +
                    DbSyntax.QuoteIdentifier("TicketGuid") + ", " +
                    DbSyntax.QuoteIdentifier("DateClosed") + ", " +
                    DbSyntax.QuoteIdentifier("SubmitterId") + ", " +
                    DbSyntax.QuoteIdentifier("TypeId") + ", " +
                    DbSyntax.QuoteIdentifier("ETA") + ", " +
                    DbSyntax.QuoteIdentifier("NotifyAlso") + ", " +
                    DbSyntax.QuoteIdentifier("InstanceId") +
                    ") VALUES (@UserId, @DateCreated, @CategoryId, @Description, @Urgency, @Status, @AssignedGroupId, @AssignedUserId, @TicketGuid, @DateClosed, @SubmitterId, @TypeId, @ETA, @NotifyAlso, @InstanceId)";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("Id");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new[] {
                    DbHelper.CreateParameter("@UserId", item.UserId),
                    DbHelper.CreateParameter("@DateCreated", item.DateCreated),
                    DbHelper.CreateParameter("@CategoryId", item.CategoryId),
                    DbHelper.CreateParameter("@Description", item.Description),
                    DbHelper.CreateParameter("@Urgency", item.Urgency),
                    DbHelper.CreateParameter("@Status", item.Status),
                    DbHelper.CreateParameter("@AssignedGroupId", item.AssignedGroupId),
                    DbHelper.CreateParameter("@AssignedUserId", item.AssignedUserId),
                    DbHelper.CreateParameter("@TicketGuid", item.TicketGuid),
                    DbHelper.CreateParameter("@DateClosed", item.DateClosed),
                    DbHelper.CreateParameter("@SubmitterId", item.SubmitterId),
                    DbHelper.CreateParameter("@TypeId", item.TypeId),
                    DbHelper.CreateParameter("@ETA", item.ETA),
                    DbHelper.CreateParameter("@NotifyAlso", item.NotifyAlso),
                    DbHelper.CreateParameter("@InstanceId", item.InstanceId)
                };
                var obj = DbHelper.ExecuteScalar(CommandType.Text, sql, parms);
                item.Id = DataUtil.GetId(obj);
            }

            return item.Id;
        }

        #region IIncidentTicketProvider Members

        public IEnumerable<IncidentTicket> GetList(int userId = -2, int categoryId = -2, int status = -2, int urgency = -2, int assignedGroupId = -2, int assignedUserId = -2)
        {
            List<IncidentTicket> items = new List<IncidentTicket>();

            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("IncidentTicket") + " WHERE 1=1";
            var parmList = new List<DbParameter>();
            if (userId != -2) { sql += " AND " + DbSyntax.QuoteIdentifier("UserId") + " = @UserId"; parmList.Add(DbHelper.CreateParameter("@UserId", userId)); }
            if (categoryId != -2) { sql += " AND " + DbSyntax.QuoteIdentifier("CategoryId") + " = @CategoryId"; parmList.Add(DbHelper.CreateParameter("@CategoryId", categoryId)); }
            if (status != -2) { sql += " AND " + DbSyntax.QuoteIdentifier("Status") + " = @Status"; parmList.Add(DbHelper.CreateParameter("@Status", status)); }
            if (urgency != -2) { sql += " AND " + DbSyntax.QuoteIdentifier("Urgency") + " = @Urgency"; parmList.Add(DbHelper.CreateParameter("@Urgency", urgency)); }
            if (assignedGroupId != -2) { sql += " AND " + DbSyntax.QuoteIdentifier("AssignedGroupId") + " = @AssignedGroupId"; parmList.Add(DbHelper.CreateParameter("@AssignedGroupId", assignedGroupId)); }
            if (assignedUserId != -2) { sql += " AND " + DbSyntax.QuoteIdentifier("AssignedUserId") + " = @AssignedUserId"; parmList.Add(DbHelper.CreateParameter("@AssignedUserId", assignedUserId)); }

            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql, parmList.ToArray()))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IncidentTicket Get(string ticketGuid)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("IncidentTicket") + " WHERE " + DbSyntax.QuoteIdentifier("TicketGuid") + " = @TicketGuid";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@TicketGuid", ticketGuid)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public int GetMaxTicketCount(DateTime date)
        {
            var sql = "SELECT COUNT(1) FROM " + DbSyntax.QuoteIdentifier("IncidentTicket") +
                " WHERE CAST(" + DbSyntax.QuoteIdentifier("DateCreated") + " AS DATE) = @Date";
            var obj = DbHelper.ExecuteScalar(CommandType.Text, sql,
                DbHelper.CreateParameter("@Date", date.Date));

            var dayMax = DataUtil.GetInt32(obj);

            return dayMax > 0 ? dayMax + 1 : 1;
        }

        #endregion
    }
}
