using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from AdminAlbum.ascx (SystemParts/Photo).
    /// </summary>
    public class AdminalbumViewComponent : WViewComponent
    {
        public AdminalbumViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdminalbumViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/Photo/AdminAlbum/Default.cshtml", model);
        }
    }

        public class AdminalbumViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<AdminalbumItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class AdminalbumItem
    {
        public string Actions { get; set; } = string.Empty;
        public string DateModified { get; set; } = string.Empty;
        public int Id { get; set; }
        public string ImageFile { get; set; } = string.Empty;
        public string PhotoHeight { get; set; } = string.Empty;
        public string PhotoWidth { get; set; } = string.Empty;
        public string Preview { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
