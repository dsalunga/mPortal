using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Photo Album gallery view. Ported from Album.ascx (SystemParts/Photo/Controls).
    /// Usage: @await Component.InvokeAsync("Photoalbum", new { objectId, recordId })
    /// </summary>
    public class PhotoalbumViewComponent : WViewComponent
    {
        public PhotoalbumViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new PhotoalbumViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                Columns = 2,
                CellPadding = 15
            };

            return View(model);
        }
    }

    public class PhotoalbumViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int Columns { get; set; } = 2;
        public int CellPadding { get; set; } = 15;
        public List<PhotoalbumItem> Albums { get; set; } = new();
    }

    public class PhotoalbumItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string LinkUrl { get; set; } = string.Empty;
    }
}
