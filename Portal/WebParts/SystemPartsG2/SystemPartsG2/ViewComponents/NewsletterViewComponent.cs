using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using System.Threading.Tasks;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    public class NewsletterViewComponent : WViewComponent
    {
        public NewsletterViewComponent(IWContext context) : base(context) { }

        public async Task<IViewComponentResult> InvokeAsync(int objectId = 0, int recordId = 0)
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
                ShowNameField = true,
                ShowGenderField = false,
                ViewState = "subscribe",
                SuccessMessage = "Thank you for subscribing. You will be receiving updates soon.",
                AlreadySubscribedMessage = "You are already subscribed to this newsletter."
            };

            return await Task.FromResult(View(model));
        }
    }

    public class NewsletterViewModel
    {
        public int ObjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SubscribeUrl { get; set; }
        public bool ShowNameField { get; set; }
        public bool ShowGenderField { get; set; }
        public string ViewState { get; set; }
        public string SuccessMessage { get; set; }
        public string AlreadySubscribedMessage { get; set; }
    }
}
