using System;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Formatted date display. Replaces DateDisplay.ascx (SystemParts).
    /// Usage: @await Component.InvokeAsync("DateDisplay", new { objectId, recordId })
    /// </summary>
    public class DateDisplayViewComponent : WViewComponent
    {
        public DateDisplayViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new DateDisplayViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                CurrentDate = DateTime.Now,
                Format = "dddd, MMMM d, yyyy"
            };

            return View(model);
        }
    }

    public class DateDisplayViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public DateTime CurrentDate { get; set; }
        public string Format { get; set; }
        public string CssClass { get; set; }
    }
}
