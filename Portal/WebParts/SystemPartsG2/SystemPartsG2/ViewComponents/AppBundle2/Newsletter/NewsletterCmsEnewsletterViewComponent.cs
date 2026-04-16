using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from CMS_Enewsletter.ascx (AppBundle2/Newsletter).
    /// </summary>
    public class NewsletterCmsEnewsletterViewComponent : WViewComponent
    {
        public NewsletterCmsEnewsletterViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new NewsletterCmsEnewsletterViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/AppBundle2/Newsletter/CMS_Enewsletter/NewsletterCmsEnewsletter/Default.cshtml", model);
        }
    }

        public class NewsletterCmsEnewsletterViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<NewsletterCmsEnewsletterItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class NewsletterCmsEnewsletterItem
    {
        public string Active { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public int Id { get; set; }
    }
}
