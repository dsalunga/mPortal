using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from CMS_DateDisplay.ascx (SystemParts/Others/DateDisplay).
    /// </summary>
    public class CmsDatedisplayViewComponent : WViewComponent
    {
        public CmsDatedisplayViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new CmsDatedisplayViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/Others/DateDisplay/CMS_DateDisplay/Default.cshtml", model);
        }
    }

        public class CmsDatedisplayViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string FormatString { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
}
