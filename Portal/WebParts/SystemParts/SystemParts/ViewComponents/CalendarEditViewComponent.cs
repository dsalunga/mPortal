using System;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Event create/edit form. Replaces CalendarEdit.ascx (SystemParts/EventCalendar).
    /// Usage: @await Component.InvokeAsync("CalendarEdit", new { objectId, recordId })
    /// </summary>
    public class CalendarEditViewComponent : WViewComponent
    {
        public CalendarEditViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new CalendarEditViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                IsLoggedIn = WcmsSession.IsLoggedIn,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today
            };

            return View(model);
        }
    }

    public class CalendarEditViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int EventId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public bool AllDay { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}
