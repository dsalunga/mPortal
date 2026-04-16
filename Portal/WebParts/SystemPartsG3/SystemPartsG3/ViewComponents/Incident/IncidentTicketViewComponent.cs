using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// View/edit an individual incident ticket with comments.
    /// Replaces TicketView.ascx (SystemPartsG3/Incident).
    /// Usage: @await Component.InvokeAsync("IncidentTicket", new { objectId })
    /// </summary>
    public class IncidentTicketViewComponent : WViewComponent
    {
        public IncidentTicketViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new IncidentTicketViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                TicketId = 0,
                Description = string.Empty,
                Status = 0,
                StatusText = string.Empty,
                Urgency = 0,
                UrgencyText = string.Empty,
                CategoryId = 0,
                CategoryName = string.Empty,
                TypeId = 0,
                TypeName = string.Empty,
                AssignedUserId = 0,
                AssignedUserName = string.Empty,
                AssignedGroupId = 0,
                SubmitterId = 0,
                UserId = 0,
                InstanceId = 0,
                TicketGuid = string.Empty,
                NotifyAlso = string.Empty,
                Comments = new List<IncidentTicketComment>(),
                CanEdit = WcmsSession.IsLoggedIn
            };

            return View("~/Views/Shared/Components/Incident/TicketView/Default.cshtml", model);
        }
    }

    public class IncidentTicketViewModel
    {
        public int ObjectId { get; set; }
        public int TicketId { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; }
        public int Urgency { get; set; }
        public string UrgencyText { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int AssignedUserId { get; set; }
        public string AssignedUserName { get; set; }
        public int AssignedGroupId { get; set; }
        public int SubmitterId { get; set; }
        public int UserId { get; set; }
        public int InstanceId { get; set; }
        public string TicketGuid { get; set; }
        public string NotifyAlso { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateClosed { get; set; }
        public DateTime? ETA { get; set; }
        public List<IncidentTicketComment> Comments { get; set; }
        public bool CanEdit { get; set; }
    }

    public class IncidentTicketComment
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
