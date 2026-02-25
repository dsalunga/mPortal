using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Shows lesson schedule/calendar view.
    /// Usage: @await Component.InvokeAsync("LessonSchedule", new { objectId, recordId })
    /// </summary>
    public class LessonScheduleViewComponent : WViewComponent
    {
        public LessonScheduleViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new LessonScheduleViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                BasePath = WcmsContext.BasePath,
                CurrentMonth = DateTime.Today,
                Entries = new List<LessonScheduleEntry>()
            };

            return View(model);
        }
    }

    public class LessonScheduleViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string BasePath { get; set; }
        public DateTime CurrentMonth { get; set; }
        public List<LessonScheduleEntry> Entries { get; set; }
    }

    public class LessonScheduleEntry
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ServiceType { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string CssClass { get; set; }
    }
}
