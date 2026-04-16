using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Social share buttons. Replaces ShareButton.ascx (Social module).
    /// Usage: @await Component.InvokeAsync("SocialShare", new { objectId, recordId })
    /// </summary>
    public class SocialShareViewComponent : WViewComponent
    {
        public SocialShareViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new SocialShareViewModel
            {
                ObjectId = WcmsContext.ObjectId,
                RecordId = WcmsContext.RecordId,
                PageUrl = string.Empty,
                PageTitle = string.Empty,
                Providers = new List<ShareProvider>()
            };

            return View("~/Views/Shared/Components/Social module/ShareButton/Default.cshtml", model);
        }
    }

    public class SocialShareViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string PageUrl { get; set; }
        public string PageTitle { get; set; }
        public List<ShareProvider> Providers { get; set; }
    }

    public class ShareProvider
    {
        public string Name { get; set; }
        public string IconCss { get; set; }
        public string ShareUrl { get; set; }
    }
}
