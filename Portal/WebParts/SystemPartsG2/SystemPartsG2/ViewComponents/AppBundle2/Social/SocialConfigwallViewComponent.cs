using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from ConfigWall.ascx (AppBundle2/Social).
    /// </summary>
    public class SocialConfigwallViewComponent : WViewComponent
    {
        public SocialConfigwallViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new SocialConfigwallViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/AppBundle2/Social/ConfigWall/SocialConfigwall/Default.cshtml", model);
        }
    }

        public class SocialConfigwallViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<SocialConfigwallItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class SocialConfigwallItem
    {
        public string Actions { get; set; } = string.Empty;
        public int Id { get; set; }
        public string PageName { get; set; } = string.Empty;
    }
}
