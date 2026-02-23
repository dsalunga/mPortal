using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Photo gallery with albums. Replaces PictureGallery.ascx (SystemParts/Gallery).
    /// Usage: @await Component.InvokeAsync("PictureGallery", new { objectId, recordId })
    /// </summary>
    public class PictureGalleryViewComponent : WViewComponent
    {
        public PictureGalleryViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new PictureGalleryViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                Albums = new List<PhotoAlbumModel>(),
                BasePath = WcmsContext.BasePath
            };

            return View(model);
        }
    }

    public class PictureGalleryViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string BasePath { get; set; }
        public List<PhotoAlbumModel> Albums { get; set; }
    }

    public class PhotoAlbumModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CoverImageUrl { get; set; }
        public int PhotoCount { get; set; }
        public List<PhotoItemModel> Photos { get; set; }
    }

    public class PhotoItemModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Description { get; set; }
    }
}
