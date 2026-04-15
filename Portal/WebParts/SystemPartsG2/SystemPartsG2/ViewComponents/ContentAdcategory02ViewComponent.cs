using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from content_adcategory_02.ascx (AppBundle2/Ads).
    /// </summary>
    public class ContentAdcategory02ViewComponent : WViewComponent
    {
        public ContentAdcategory02ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ContentAdcategory02ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class ContentAdcategory02ViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
}
