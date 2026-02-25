using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using System.Threading.Tasks;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    public class FlashBannerViewComponent : WViewComponent
    {
        public FlashBannerViewComponent(IWContext context) : base(context) { }

        public async Task<IViewComponentResult> InvokeAsync(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new FlashBannerViewModel
            {
                ObjectId = WcmsContext.ObjectId,
                RecordId = WcmsContext.RecordId,
                BannerUrl = string.Empty,
                TargetUrl = string.Empty,
                Width = 728,
                Height = 90,
                AltText = string.Empty
            };

            return await Task.FromResult(View(model));
        }
    }

    public class FlashBannerViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string BannerUrl { get; set; }
        public string TargetUrl { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string AltText { get; set; }
    }
}
