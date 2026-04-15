using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from subsiteslist_nb.ascx (AppBundle2/SiteList).
    /// </summary>
    public class SitelistSubsiteslistNbViewComponent : WViewComponent
    {
        public SitelistSubsiteslistNbViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new SitelistSubsiteslistNbViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class SitelistSubsiteslistNbViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
    }
}
