using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Weekly schedule grid. Replaces WeeklyScheduler.ascx (SystemParts/WeeklyScheduler).
    /// Usage: @await Component.InvokeAsync("WeeklyScheduler", new { objectId, recordId })
    /// </summary>
    public class WeeklySchedulerViewComponent : WViewComponent
    {
        public WeeklySchedulerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new WeeklySchedulerViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                Entries = new List<ScheduleEntryModel>(),
                StartHour = 8,
                EndHour = 18
            };

            return View(model);
        }
    }

    public class WeeklySchedulerViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string Title { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }
        public List<ScheduleEntryModel> Entries { get; set; }
    }

    public class ScheduleEntryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int DayOfWeek { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Location { get; set; }
        public string CssClass { get; set; }
    }
}
