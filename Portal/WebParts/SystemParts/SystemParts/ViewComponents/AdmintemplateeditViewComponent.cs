using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from AdminTemplateEdit.ascx (SystemParts/EventCalendar).
    /// </summary>
    public class AdmintemplateeditViewComponent : WViewComponent
    {
        public AdmintemplateeditViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdmintemplateeditViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class AdmintemplateeditViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string BackColor { get; set; } = string.Empty;
        public string ForeColor { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string SmsContent { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
}
