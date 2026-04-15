using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from InstagramGallery.ascx (Apps/Integration/MusicCompetition).
    /// </summary>
    public class MusiccompetitionInstagramgalleryViewComponent : WViewComponent
    {
        public MusiccompetitionInstagramgalleryViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new MusiccompetitionInstagramgalleryViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class MusiccompetitionInstagramgalleryViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<MusiccompetitionInstagramgalleryItem> Items { get; set; } = new();
    }

    public class MusiccompetitionInstagramgalleryItem
    {
        public string Name { get; set; } = string.Empty;
    }
}
