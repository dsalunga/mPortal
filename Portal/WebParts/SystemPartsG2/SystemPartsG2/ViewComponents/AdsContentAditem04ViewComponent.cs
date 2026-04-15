using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from content_aditem_04.ascx (AppBundle2/Ads).
    /// </summary>
    public class AdsContentAditem04ViewComponent : WViewComponent
    {
        public AdsContentAditem04ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdsContentAditem04ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class AdsContentAditem04ViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string AlternateText { get; set; } = string.Empty;
        public string Filename { get; set; } = string.Empty;
        public string Impressions { get; set; } = string.Empty;
        public string Keyword { get; set; } = string.Empty;
        public string NavigateUrl { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
}
