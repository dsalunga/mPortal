using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.Diagnostics;
using WCMS.Framework.ViewComponents;
using WCMS.WebSystem.WebParts.Content;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Renders CMS element content. Replaces Content.ascx (SystemParts).
    /// Usage: @await Component.InvokeAsync("Content", new { objectId, recordId })
    /// </summary>
    public class ContentViewComponent : WViewComponent
    {
        public ContentViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            var sw = PerformanceLog.StartLog();

            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var contentText = ContentHelper.GetElementContent(WcmsContext as WContext);

            PerformanceLog.EndLog(
                string.Format("Content: {0}/{1}", WcmsContext.ObjectId, WcmsContext.RecordId),
                sw, WcmsContext.PageId);

            return View("~/Views/Shared/Components/Content/Default.cshtml", model: contentText ?? string.Empty);
        }
    }
}
