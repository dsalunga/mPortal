using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Three-column layout container. Replaces ThreeColumn.ascx (SystemParts/Container).
    /// Usage: @await Component.InvokeAsync("ThreeColumn", new { objectId, recordId })
    /// </summary>
    public class ThreeColumnViewComponent : WViewComponent
    {
        public ThreeColumnViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ThreeColumnViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                LeftColumnWidth = 3,
                CenterColumnWidth = 6,
                RightColumnWidth = 3,
                LeftContent = string.Empty,
                CenterContent = string.Empty,
                RightContent = string.Empty,
                CssClass = string.Empty
            };

            return View(model);
        }
    }

    public class ThreeColumnViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int LeftColumnWidth { get; set; }
        public int CenterColumnWidth { get; set; }
        public int RightColumnWidth { get; set; }
        public string LeftContent { get; set; }
        public string CenterContent { get; set; }
        public string RightContent { get; set; }
        public string CssClass { get; set; }
    }
}
