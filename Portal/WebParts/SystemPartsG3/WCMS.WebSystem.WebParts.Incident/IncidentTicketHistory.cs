using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.Incident.Providers;

namespace WCMS.WebSystem.WebParts.Incident
{
    public class IncidentTicketHistory : WebObjectBase, ISelfManager
    {
        private static IIncidentTicketHistoryProvider _provider = new IncidentTicketHistoryProvider();

        public IncidentTicketHistory()
        {
            TicketId = -1;
            UserId = -1;

            DateCreated = WConstants.DateTimeMinValue;
            Type = TicketHistoryType.History;
        }

        public int TicketId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public int Type { get; set; }

        public WebUser User
        {
            get { return WebUser.Get(UserId); }
        }

        public override int OBJECT_ID { get { return IncidentConstants.INCIDENT_TICKET_HISTORY_ID; } }

        public static IIncidentTicketHistoryProvider Provider { get { return _provider; } }

        #region ISelfManager Members

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        public int Update()
        {
            return _provider.Update(this);
        }

        #endregion
    }
}
