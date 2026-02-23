using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Two-column layout container. Replaces TwoColumn.ascx (SystemParts/Container).
    /// Usage: @await Component.InvokeAsync("TwoColumn", new { objectId, recordId })
    /// </summary>
    public class TwoColumnViewComponent : WViewComponent
    {
        public TwoColumnViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new TwoColumnViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                LeftColumnWidth = 6,
                RightColumnWidth = 6,
                LeftContent = string.Empty,
                RightContent = string.Empty,
                CssClass = string.Empty
            };

            return View(model);
        }
    }

    public class TwoColumnViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int LeftColumnWidth { get; set; }
        public int RightColumnWidth { get; set; }
        public string LeftContent { get; set; }
        public string RightContent { get; set; }
        public string CssClass { get; set; }
    }
}
