using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from AdminNewsletter.ascx (AppBundle2/Newsletter).
    /// </summary>
    public class AdminnewsletterViewComponent : WViewComponent
    {
        public AdminnewsletterViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdminnewsletterViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/AppBundle2/Newsletter/AdminNewsletter/Adminnewsletter/Default.cshtml", model);
        }
    }

        public class AdminnewsletterViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<AdminnewsletterItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class AdminnewsletterItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SubscribeDate { get; set; } = string.Empty;
    }
}
