using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from subsiteslist.ascx (AppBundle2/SiteList).
    /// </summary>
    public class SitelistSubsiteslistViewComponent : WViewComponent
    {
        public SitelistSubsiteslistViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new SitelistSubsiteslistViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/AppBundle2/SiteList/subsiteslist/SitelistSubsiteslist/Default.cshtml", model);
        }
    }

        public class SitelistSubsiteslistViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<SitelistSubsiteslistItem> Items { get; set; } = new();
    }

    public class SitelistSubsiteslistItem
    {
        public string Name { get; set; } = string.Empty;
    }
}
