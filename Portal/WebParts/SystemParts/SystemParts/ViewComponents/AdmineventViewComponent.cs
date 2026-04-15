using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from AdminEvent.ascx (SystemParts/EventCalendar).
    /// </summary>
    public class AdmineventViewComponent : WViewComponent
    {
        public AdmineventViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdmineventViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class AdmineventViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string hRecipients { get; set; } = string.Empty;
        public string hBaseGroup { get; set; } = string.Empty;
        public string txtSubject { get; set; } = string.Empty;
        public List<SelectListItem> cboLocationsItems { get; set; } = new();
        public string cboLocationsSelected { get; set; } = string.Empty;
        public bool chkOtherLocation { get; set; }
        public string txtLocation { get; set; } = string.Empty;
        public string txtStartDate { get; set; } = string.Empty;
        public string txtEndDate { get; set; } = string.Empty;
        public List<SelectListItem> cboCalendarItems { get; set; } = new();
        public string cboCalendarSelected { get; set; } = string.Empty;
        public string txtRepeatUntil { get; set; } = string.Empty;
        public bool chkNoEnd { get; set; }
        public List<SelectListItem> cboReminderBeforeItems { get; set; } = new();
        public string cboReminderBeforeSelected { get; set; } = string.Empty;
        public List<SelectListItem> cboTemplatesItems { get; set; } = new();
        public string cboTemplatesSelected { get; set; } = string.Empty;
        public string txtReminderToEmail { get; set; } = string.Empty;
        public string txtAdd { get; set; } = string.Empty;
        public List<object> ObjectDataSource1Data { get; set; } = new();
        public List<SelectListItem> cboCategoryItems { get; set; } = new();
        public string cboCategorySelected { get; set; } = string.Empty;
    }
}
