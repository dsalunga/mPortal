using System;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Single event detail view. Replaces CalendarEventView.ascx (SystemParts/EventCalendar).
    /// Usage: @await Component.InvokeAsync("CalendarEventView", new { objectId, recordId })
    /// </summary>
    public class CalendarEventViewViewComponent : WViewComponent
    {
        public CalendarEventViewViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new CalendarEventViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                BasePath = WcmsContext.BasePath
            };

            return View(model);
        }
    }

    public class CalendarEventViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int EventId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string OrganizerName { get; set; }
        public string OrganizerEmail { get; set; }
        public string BasePath { get; set; }
    }
}
