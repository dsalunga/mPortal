using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Lists incident tickets with status filtering.
    /// Replaces TicketManagerView.ascx (SystemPartsG3/Incident).
    /// Usage: @await Component.InvokeAsync("IncidentTicketManager", new { objectId })
    /// </summary>
    public class IncidentTicketManagerViewComponent : WViewComponent
    {
        public IncidentTicketManagerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new IncidentTicketManagerViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                Tickets = new List<IncidentTicketListItem>(),
                StatusFilter = -1,
                CanEdit = WcmsSession.IsLoggedIn
            };

            return View(model);
        }
    }

    public class IncidentTicketManagerViewModel
    {
        public int ObjectId { get; set; }
        public List<IncidentTicketListItem> Tickets { get; set; }
        public int StatusFilter { get; set; }
        public bool CanEdit { get; set; }
    }

    public class IncidentTicketListItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; }
        public int Urgency { get; set; }
        public string UrgencyText { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateClosed { get; set; }
        public int AssignedUserId { get; set; }
        public string AssignedUserName { get; set; }
    }
}
