using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.Incident.Providers;

namespace WCMS.WebSystem.WebParts.Incident
{
    public class IncidentTicket : WebObjectBase, ISelfManager
    {
        private static IIncidentTicketProvider _provider = new IncidentTicketProvider();

        public IncidentTicket()
        {
            UserId = -1;
            CategoryId = -1;
            TypeId = -1;

            AssignedGroupId = -1;
            AssignedUserId = -1;
            SubmitterId = -1;
            InstanceId = -1;

            Urgency = TicketUrgency.Normal;
            Status = TicketStatus.Submitted;
            DateCreated = WConstants.DateTimeMinValue;
            DateClosed = WConstants.DateTimeMinValue;
            ETA = WConstants.DateTimeMinValue;

            Description = string.Empty;
            TicketGuid = string.Empty;
            NotifyAlso = string.Empty;
        }

        /// <summary>
        /// Requestor
        /// </summary>
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public int CategoryId { get; set; }
        public int AssignedGroupId { get; set; }
        public int AssignedUserId { get; set; }
        public string Description { get; set; }
        public int Urgency { get; set; }
        public int Status { get; set; }
        public string TicketGuid { get; set; }
        public DateTime DateClosed { get; set; }
        public int SubmitterId { get; set; }
        public int TypeId { get; set; }
        public int InstanceId { get; set; }
        public DateTime ETA { get; set; }
        public string NotifyAlso { get; set; }

        public WebUser Requestor { get { return WebUser.Get(UserId); } }
        public WebUser Submitter { get { return WebUser.Get(SubmitterId); } }
        public WebUser AssignedUser { get { return WebUser.Get(AssignedUserId); } }
        public WebGroup AssignedGroup { get { return WebGroup.Get(AssignedGroupId); } }
        public IncidentInstance Instance { get { return IncidentInstance.Provider.Get(InstanceId); } }

        public override int OBJECT_ID { get { return IncidentConstants.INCIDENT_TICKET_ID; } }

        public static IIncidentTicketProvider Provider { get { return _provider; } }

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
