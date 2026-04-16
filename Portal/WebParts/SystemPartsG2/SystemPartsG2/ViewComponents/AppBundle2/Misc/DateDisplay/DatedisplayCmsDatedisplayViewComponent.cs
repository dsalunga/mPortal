using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from CMS_DateDisplay.ascx (AppBundle2/Misc/DateDisplay).
    /// </summary>
    public class DatedisplayCmsDatedisplayViewComponent : WViewComponent
    {
        public DatedisplayCmsDatedisplayViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new DatedisplayCmsDatedisplayViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/AppBundle2/Misc/DateDisplay/CMS_DateDisplay/DatedisplayCmsDatedisplay/Default.cshtml", model);
        }
    }

        public class DatedisplayCmsDatedisplayViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string FormatString { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
}
