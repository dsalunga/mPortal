using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from ConfigDynamicMenuV2.ascx (SystemParts/Menu).
    /// </summary>
    public class Configdynamicmenuv2ViewComponent : WViewComponent
    {
        public Configdynamicmenuv2ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new Configdynamicmenuv2ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class Configdynamicmenuv2ViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<SelectListItem> cboMenuItems { get; set; } = new();
        public string cboMenuSelected { get; set; } = string.Empty;
        public string lblStatus { get; set; } = string.Empty;
    }
}
