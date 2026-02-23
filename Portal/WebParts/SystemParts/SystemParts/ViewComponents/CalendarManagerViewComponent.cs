using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Calendar admin management. Replaces CalendarManager.ascx (SystemParts/EventCalendar).
    /// Usage: @await Component.InvokeAsync("CalendarManager", new { objectId, recordId })
    /// </summary>
    public class CalendarManagerViewComponent : WViewComponent
    {
        public CalendarManagerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new CalendarManagerViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                Events = new List<CalendarManagerEventItem>(),
                IsLoggedIn = WcmsSession.IsLoggedIn
            };

            return View(model);
        }
    }

    public class CalendarManagerViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public bool IsLoggedIn { get; set; }
        public List<CalendarManagerEventItem> Events { get; set; }
    }

    public class CalendarManagerEventItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
    }
}
