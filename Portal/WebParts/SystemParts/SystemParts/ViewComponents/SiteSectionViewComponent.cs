using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Section title/header display. Replaces SiteSection.ascx (SystemParts).
    /// Usage: @await Component.InvokeAsync("SiteSection", new { objectId, recordId })
    /// </summary>
    public class SiteSectionViewComponent : WViewComponent
    {
        public SiteSectionViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new SiteSectionViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                BasePath = WcmsContext.BasePath
            };

            return View("~/Views/Shared/Components/SiteSection/Default.cshtml", model);
        }
    }

    public class SiteSectionViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string CssClass { get; set; }
        public string BasePath { get; set; }
        public string HeaderTag { get; set; }
    }
}
