using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Monthly calendar with events. Replaces CalendarView.ascx (SystemParts/EventCalendar).
    /// Usage: @await Component.InvokeAsync("CalendarView", new { objectId, recordId })
    /// </summary>
    public class CalendarViewViewComponent : WViewComponent
    {
        public CalendarViewViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new CalendarViewViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                CurrentMonth = DateTime.Today,
                Events = new List<CalendarEventItem>(),
                BasePath = WcmsContext.BasePath
            };

            return View(model);
        }
    }

    public class CalendarViewViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public DateTime CurrentMonth { get; set; }
        public string BasePath { get; set; }
        public List<CalendarEventItem> Events { get; set; }
    }

    public class CalendarEventItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string CssClass { get; set; }
    }
}
