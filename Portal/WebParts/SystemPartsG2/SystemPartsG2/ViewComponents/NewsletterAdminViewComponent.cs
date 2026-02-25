using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using System.Threading.Tasks;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    public class NewsletterAdminViewComponent : WViewComponent
    {
        public NewsletterAdminViewComponent(IWContext context) : base(context) { }

        public async Task<IViewComponentResult> InvokeAsync(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new NewsletterAdminViewModel
            {
                ObjectId = WcmsContext.ObjectId,
                Newsletters = new List<NewsletterAdminItem>(),
                TotalSubscribers = 0
            };

            return await Task.FromResult(View(model));
        }
    }

    public class NewsletterAdminViewModel
    {
        public int ObjectId { get; set; }
        public List<NewsletterAdminItem> Newsletters { get; set; }
        public int TotalSubscribers { get; set; }
    }

    public class NewsletterAdminItem
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Status { get; set; }
        public string SentDate { get; set; }
        public int RecipientCount { get; set; }
    }
}
