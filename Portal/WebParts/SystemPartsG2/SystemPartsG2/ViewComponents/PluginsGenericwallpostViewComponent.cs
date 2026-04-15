using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from GenericWallPost.ascx (AppBundle2/Social/Plugins).
    /// </summary>
    public class PluginsGenericwallpostViewComponent : WViewComponent
    {
        public PluginsGenericwallpostViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new PluginsGenericwallpostViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class PluginsGenericwallpostViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
    }
}
