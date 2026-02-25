using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using System.Threading.Tasks;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    public class MediaListViewComponent : WViewComponent
    {
        public MediaListViewComponent(IWContext context) : base(context) { }

        public async Task<IViewComponentResult> InvokeAsync(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new MediaListViewModel
            {
                ObjectId = WcmsContext.ObjectId,
                Items = new List<MediaListItem>()
            };

            return await Task.FromResult(View(model));
        }
    }

    public class MediaListViewModel
    {
        public int ObjectId { get; set; }
        public List<MediaListItem> Items { get; set; }
    }

    public class MediaListItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MediaUrl { get; set; }
        public string MediaType { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Duration { get; set; }
    }
}
