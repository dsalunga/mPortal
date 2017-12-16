using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Incident.Providers
{
    public interface IIncidentTicketHistoryProvider : IDataProvider<IncidentTicketHistory>
    {
        IEnumerable<IncidentTicketHistory> GetList(int ticketId = -2, int userId = -2, int type = -2);
    }
}
