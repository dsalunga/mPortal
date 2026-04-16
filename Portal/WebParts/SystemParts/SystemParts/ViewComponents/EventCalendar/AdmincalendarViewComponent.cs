using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from AdminCalendar.ascx (SystemParts/EventCalendar).
    /// </summary>
    public class AdmincalendarViewComponent : WViewComponent
    {
        public AdmincalendarViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdmincalendarViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/EventCalendar/AdminCalendar/Default.cshtml", model);
        }
    }

        public class AdmincalendarViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<AdmincalendarItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class AdmincalendarItem
    {
        public string CategoryName { get; set; } = string.Empty;
        public string Edit { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public string FinalLocation { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Recurrence { get; set; } = string.Empty;
        public string StartDate { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
    }
}
