using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Instagram feed display. Replaces InstagramGallery.ascx (SystemParts/Gallery).
    /// Usage: @await Component.InvokeAsync("InstagramGallery", new { objectId, recordId })
    /// </summary>
    public class InstagramGalleryViewComponent : WViewComponent
    {
        public InstagramGalleryViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new InstagramGalleryViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                Posts = new List<InstagramPostModel>(),
                ColumnsPerRow = 4
            };

            return View(model);
        }
    }

    public class InstagramGalleryViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string Username { get; set; }
        public int ColumnsPerRow { get; set; }
        public List<InstagramPostModel> Posts { get; set; }
    }

    public class InstagramPostModel
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Caption { get; set; }
        public string Permalink { get; set; }
        public string MediaType { get; set; }
    }
}
