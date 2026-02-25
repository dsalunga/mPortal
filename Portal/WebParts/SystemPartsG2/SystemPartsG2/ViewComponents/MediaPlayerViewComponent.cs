using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using System.Threading.Tasks;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    public class MediaPlayerViewComponent : WViewComponent
    {
        public MediaPlayerViewComponent(IWContext context) : base(context) { }

        public async Task<IViewComponentResult> InvokeAsync(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new MediaPlayerViewModel
            {
                ObjectId = WcmsContext.ObjectId,
                RecordId = WcmsContext.RecordId,
                Title = string.Empty,
                MediaUrl = string.Empty,
                MediaType = string.Empty,
                PosterUrl = string.Empty,
                AutoPlay = false
            };

            return await Task.FromResult(View(model));
        }
    }

    public class MediaPlayerViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string Title { get; set; }
        public string MediaUrl { get; set; }
        public string MediaType { get; set; }
        public string PosterUrl { get; set; }
        public bool AutoPlay { get; set; }
        public string Blurb { get; set; }
        public string ThumbnailUrl { get; set; }
        public string MediaVersion { get; set; }
        public string MediaLength { get; set; }
        public string Agency { get; set; }
        public string BackUrl { get; set; }
    }
}
