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

            return View(model);
        }
    }

    public class AdmincalendarViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<SelectListItem> cboCalendarItems { get; set; } = new();
        public string cboCalendarSelected { get; set; } = string.Empty;
        public List<SelectListItem> cboMonthItems { get; set; } = new();
        public string cboMonthSelected { get; set; } = string.Empty;
        public List<SelectListItem> cboYearItems { get; set; } = new();
        public string cboYearSelected { get; set; } = string.Empty;
        public List<object> GridView1Data { get; set; } = new();
    }
}
