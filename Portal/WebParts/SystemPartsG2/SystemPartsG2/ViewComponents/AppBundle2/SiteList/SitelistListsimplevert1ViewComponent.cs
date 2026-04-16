using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from listsimplevert1.ascx (AppBundle2/SiteList).
    /// </summary>
    public class SitelistListsimplevert1ViewComponent : WViewComponent
    {
        public SitelistListsimplevert1ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new SitelistListsimplevert1ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/AppBundle2/SiteList/listsimplevert1/SitelistListsimplevert1/Default.cshtml", model);
        }
    }

        public class SitelistListsimplevert1ViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<SitelistListsimplevert1Item> Items { get; set; } = new();
    }

    public class SitelistListsimplevert1Item
    {
        public string Name { get; set; } = string.Empty;
    }
}
