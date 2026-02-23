using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    public class NewsletterViewComponent : WViewComponent
    {
        public NewsletterViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new NewsletterViewModel
            {
                ObjectId = WcmsContext.ObjectId,
                Title = string.Empty,
                Description = string.Empty,
                SubscribeUrl = string.Empty,
                ShowNameField = true
            };

            return View(model);
        }
    }

    public class NewsletterViewModel
    {
        public int ObjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SubscribeUrl { get; set; }
        public bool ShowNameField { get; set; }
    }
}
