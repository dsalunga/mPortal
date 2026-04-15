using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from ConfigMemberLink.ascx (Apps/Integration/Registration).
    /// </summary>
    public class ConfigmemberlinkViewComponent : WViewComponent
    {
        public ConfigmemberlinkViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ConfigmemberlinkViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class ConfigmemberlinkViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public bool AccountSendEmail { get; set; } = false;
        public string ExternalID { get; set; } = string.Empty;
        public bool PortalSendEmail { get; set; } = false;
        public string StatusMessage { get; set; } = string.Empty;
    }
}
