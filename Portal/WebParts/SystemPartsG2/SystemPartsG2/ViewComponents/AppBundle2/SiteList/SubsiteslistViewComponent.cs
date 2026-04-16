using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from subsiteslist.ascx (AppBundle2/SiteList).
    /// </summary>
    public class SubsiteslistViewComponent : WViewComponent
    {
        public SubsiteslistViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new SubsiteslistViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/AppBundle2/SiteList/subsiteslist/Subsiteslist/Default.cshtml", model);
        }
    }

        public class SubsiteslistViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<SubsiteslistItem> Items { get; set; } = new();
    }

    public class SubsiteslistItem
    {
        public string Name { get; set; } = string.Empty;
    }
}
