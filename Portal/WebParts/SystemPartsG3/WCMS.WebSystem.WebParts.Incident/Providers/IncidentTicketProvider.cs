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
    public class IncidentTicketProvider : GenericSqlDataProviderBase<IncidentTicket>, IIncidentTicketProvider
    {
        protected override string DeleteProcedure { get { return "IncidentTicket_Del"; } }
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
            var obj = SqlHelper.ExecuteScalar("IncidentTicket_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@UserId", item.UserId),
                new SqlParameter("@DateCreated", item.DateCreated),
                new SqlParameter("@CategoryId", item.CategoryId),
                new SqlParameter("@Description", item.Description),
                new SqlParameter("@Urgency", item.Urgency),
                new SqlParameter("@Status", item.Status),
                new SqlParameter("@AssignedGroupId", item.AssignedGroupId),
                new SqlParameter("@AssignedUserId", item.AssignedUserId),
                new SqlParameter("@TicketGuid", item.TicketGuid),
                new SqlParameter("@DateClosed", item.DateClosed),
                new SqlParameter("@SubmitterId", item.SubmitterId),
                new SqlParameter("@TypeId", item.TypeId),
                new SqlParameter("@ETA", item.ETA),
                new SqlParameter("@NotifyAlso", item.NotifyAlso),
                new SqlParameter("@InstanceId", item.InstanceId)
            );

            item.Id = DataUtil.GetId(obj);
            return item.Id;
        }

        #region IIncidentTicketProvider Members

        public IEnumerable<IncidentTicket> GetList(int userId = -2, int categoryId = -2, int status = -2, int urgency = -2, int assignedGroupId = -2, int assignedUserId = -2)
        {
            List<IncidentTicket> items = new List<IncidentTicket>();

            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@UserId", userId),
                new SqlParameter("@CategoryId", categoryId),
                new SqlParameter("@Status", status),
                new SqlParameter("@Urgency", urgency),
                new SqlParameter("@AssignedGroupId", assignedGroupId),
                new SqlParameter("@AssignedUserId", assignedUserId)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public IncidentTicket Get(string ticketGuid)
        {
            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@TicketGuid", ticketGuid)))
            {
                if (r.Read())
                    return From(r);
            }

            return null;
        }

        public int GetMaxTicketCount(DateTime date)
        {
            var obj = SqlHelper.ExecuteScalar("IncidentTicket_GetMaxCount",
                new SqlParameter("@Date", date.Date));

            var dayMax = DataUtil.GetInt32(obj);

            return dayMax > 0 ? dayMax + 1 : 1;
        }

        #endregion
    }
}
