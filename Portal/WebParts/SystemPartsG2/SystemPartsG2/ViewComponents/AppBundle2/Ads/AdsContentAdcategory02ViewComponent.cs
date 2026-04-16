using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from content_adcategory_02.ascx (AppBundle2/Ads).
    /// </summary>
    public class AdsContentAdcategory02ViewComponent : WViewComponent
    {
        public AdsContentAdcategory02ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdsContentAdcategory02ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/AppBundle2/Ads/content_adcategory_02/AdsContentAdcategory02/Default.cshtml", model);
        }
    }

        public class AdsContentAdcategory02ViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
}
