using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from Newsletter.ascx (AppBundle2/Newsletter).
    /// </summary>
    public class NewsletterNewsletterViewComponent : WViewComponent
    {
        public NewsletterNewsletterViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new NewsletterNewsletterViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class NewsletterNewsletterViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public List<SelectOption> RblGenderOptions { get; set; } = new();
        public string SelectedRblGender { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
    }
