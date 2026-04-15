using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from cms_media.ascx (AppBundle2/Media).
    /// </summary>
    public class CmsMediaViewComponent : WViewComponent
    {
        public CmsMediaViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new CmsMediaViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class CmsMediaViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<CmsMediaItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class CmsMediaItem
    {
        public string Agency { get; set; } = string.Empty;
        public string Edit { get; set; } = string.Empty;
        public int Id { get; set; }
        public string MediaID { get; set; } = string.Empty;
        public string MediaVersion { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
