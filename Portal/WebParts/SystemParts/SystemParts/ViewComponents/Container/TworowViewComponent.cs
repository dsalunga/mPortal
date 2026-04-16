using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from TwoRow.ascx (SystemParts/Container).
    /// </summary>
    public class TworowViewComponent : WViewComponent
    {
        public TworowViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new TworowViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/Container/TwoRow/Default.cshtml", model);
        }
    }

        public class TworowViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string ContentHtml { get; set; } = string.Empty;
    }
}
