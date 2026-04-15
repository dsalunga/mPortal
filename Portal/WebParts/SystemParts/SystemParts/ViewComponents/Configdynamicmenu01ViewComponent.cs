using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from ConfigDynamicMenu01.ascx (SystemParts/Menu).
    /// </summary>
    public class Configdynamicmenu01ViewComponent : WViewComponent
    {
        public Configdynamicmenu01ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new Configdynamicmenu01ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class Configdynamicmenu01ViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<SelectListItem> cboMenuItems { get; set; } = new();
        public string cboMenuSelected { get; set; } = string.Empty;
        public List<SelectListItem> cboRenderModeItems { get; set; } = new();
        public string cboRenderModeSelected { get; set; } = string.Empty;
        public List<SelectListItem> cboOrientationItems { get; set; } = new();
        public string cboOrientationSelected { get; set; } = string.Empty;
        public string txtWidth { get; set; } = string.Empty;
        public string txtHeight { get; set; } = string.Empty;
        public string lblStatus { get; set; } = string.Empty;
    }
}
