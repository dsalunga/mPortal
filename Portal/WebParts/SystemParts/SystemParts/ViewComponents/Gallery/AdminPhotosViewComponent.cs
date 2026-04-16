using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Photo admin management. Replaces AdminPhotos.ascx (SystemParts/Gallery).
    /// Usage: @await Component.InvokeAsync("AdminPhotos", new { objectId, recordId })
    /// </summary>
    public class AdminPhotosViewComponent : WViewComponent
    {
        public AdminPhotosViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdminPhotosViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                Photos = new List<AdminPhotoItem>(),
                IsLoggedIn = WcmsSession.IsLoggedIn
            };

            return View("~/Views/Shared/Components/Gallery/AdminPhotos/Default.cshtml", model);
        }
    }

    public class AdminPhotosViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public bool IsLoggedIn { get; set; }
        public List<AdminPhotoItem> Photos { get; set; }
    }

    public class AdminPhotoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string AlbumName { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
