using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from Default.ascx (SystemParts/FileManager).
    /// </summary>
    public class FileManagerPageViewComponent : WViewComponent
    {
        public FileManagerPageViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new FileManagerPageViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/FileManager/Default/Default.cshtml", model);
        }
    }

    public class FileManagerPageViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        // Model is intentionally minimal: ObjectId/RecordId are the core CMS routing properties.
        // The Razor view fetches domain-specific data (articles, events, files, etc.) via WcmsContext.
    }
}
