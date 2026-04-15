using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from Attendees.ascx (Apps/Integration/EventRegister).
    /// </summary>
    public class AttendeesViewComponent : WViewComponent
    {
        public AttendeesViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AttendeesViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class AttendeesViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<AttendeesItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class AttendeesItem
    {
        public string Active { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public string DateCreated { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public int Id { get; set; }
        public string LastLogin { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string LastUpdate { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Nickname { get; set; } = string.Empty;
    }
}
