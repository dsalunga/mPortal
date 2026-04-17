using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from AdminEventView.ascx (SystemParts/EventCalendar).
    /// </summary>
    public class AdmineventviewViewComponent : WViewComponent
    {
        public AdmineventviewViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdmineventviewViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/EventCalendar/AdminEventView/Default.cshtml", model);
        }
    }

    public class AdmineventviewViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        // Model is intentionally minimal: ObjectId/RecordId are the core CMS routing properties.
        // The Razor view fetches domain-specific data (articles, events, files, etc.) via WcmsContext.
    }
}
