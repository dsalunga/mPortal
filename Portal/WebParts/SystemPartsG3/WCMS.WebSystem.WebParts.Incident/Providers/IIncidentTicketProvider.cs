using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Incident.Providers
{
    public interface IIncidentTicketProvider : IDataProvider<IncidentTicket>
    {
        IEnumerable<IncidentTicket> GetList(int userId = -2, int categoryId = -2, int status = -2, int urgency = -2, int assignedGroupId = -2, int assignedUserId = -2);
        IncidentTicket Get(string ticketGuid);
        int GetMaxTicketCount(DateTime date);
    }
}
