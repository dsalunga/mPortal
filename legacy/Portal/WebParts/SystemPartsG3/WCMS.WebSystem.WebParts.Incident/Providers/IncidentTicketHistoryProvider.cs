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
    public class IncidentTicketHistoryProvider : GenericSqlDataProviderBase<IncidentTicketHistory> , IIncidentTicketHistoryProvider
    {
        protected override string DeleteProcedure { get { return "IncidentTicketHistory_Del"; } }
        protected override string SelectProcedure { get { return "IncidentTicketHistory_Get"; } }

        protected override IncidentTicketHistory From(IDataReader r, IncidentTicketHistory source)
        {
            IncidentTicketHistory item = source ?? new IncidentTicketHistory();
            item.Id = DataUtil.GetId(r, WebColumns.Id);
            item.TicketId = DataUtil.GetId(r, "TicketId");
            item.UserId = DataUtil.GetId(r, WebColumns.UserId);
            item.Content = DataUtil.Get(r, "Content");
            item.DateCreated = DataUtil.GetDateTime(r, "DateCreated");
            item.Type = DataUtil.GetInt32(r, "Type");

            return item;
        }

        public override int Update(IncidentTicketHistory item)
        {
            var obj = SqlHelper.ExecuteScalar("IncidentTicketHistory_Set",
                new SqlParameter("@Id", item.Id),
                new SqlParameter("@TicketId", item.TicketId),
                new SqlParameter("@UserId", item.UserId),
                new SqlParameter("@Content", item.Content),
                new SqlParameter("@DateCreated", item.DateCreated),
                new SqlParameter("@Type", item.Type));

            item.Id = DataUtil.GetId(obj);
            return item.Id;
        }

        #region IIncidentTicketHistoryProvider Members

        public IEnumerable<IncidentTicketHistory> GetList(int ticketId = -2, int userId = -2, int type = -2)
        {
            List<IncidentTicketHistory> items = new List<IncidentTicketHistory>();

            using (var r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter("@TicketId", ticketId),
                new SqlParameter("@UserId", userId),
                new SqlParameter("@Type", type)))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        #endregion
    }
}
