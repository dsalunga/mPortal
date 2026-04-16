using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from Preview.ascx (SystemParts/Photo).
    /// </summary>
    public class PhotoPreviewViewComponent : WViewComponent
    {
        public PhotoPreviewViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new PhotoPreviewViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/Photo/Preview/Default.cshtml", model);
        }
    }

        public class PhotoPreviewViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<PhotoPreviewItem> Items { get; set; } = new();
    }

    public class PhotoPreviewItem
    {
        public string Name { get; set; } = string.Empty;
    }
}
