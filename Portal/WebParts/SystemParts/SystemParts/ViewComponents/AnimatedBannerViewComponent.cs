using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Rotating banner/slider. Replaces AnimatedBanner.ascx (SystemParts/Gallery).
    /// Usage: @await Component.InvokeAsync("AnimatedBanner", new { objectId, recordId })
    /// </summary>
    public class AnimatedBannerViewComponent : WViewComponent
    {
        public AnimatedBannerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AnimatedBannerViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                Slides = new List<BannerSlideModel>(),
                IntervalMs = 5000,
                ShowIndicators = true,
                ShowControls = true
            };

            return View(model);
        }
    }

    public class AnimatedBannerViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int IntervalMs { get; set; }
        public bool ShowIndicators { get; set; }
        public bool ShowControls { get; set; }
        public List<BannerSlideModel> Slides { get; set; }
    }

    public class BannerSlideModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
        public string LinkUrl { get; set; }
        public int SortOrder { get; set; }
    }
}
