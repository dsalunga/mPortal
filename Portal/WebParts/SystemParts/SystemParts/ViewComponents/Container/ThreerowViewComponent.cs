using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from ThreeRow.ascx (SystemParts/Container).
    /// </summary>
    public class ThreerowViewComponent : WViewComponent
    {
        public ThreerowViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ThreerowViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/Container/ThreeRow/Default.cshtml", model);
        }
    }

        public class ThreerowViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string ContentHtml { get; set; } = string.Empty;
    }
}
