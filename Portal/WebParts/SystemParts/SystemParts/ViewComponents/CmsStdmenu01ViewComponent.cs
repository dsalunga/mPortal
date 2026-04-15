using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from CMS_StdMenu_01.ascx (SystemParts/Menu).
    /// </summary>
    public class CmsStdmenu01ViewComponent : WViewComponent
    {
        public CmsStdmenu01ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new CmsStdmenu01ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class CmsStdmenu01ViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<SelectListItem> DropDownList1Items { get; set; } = new();
        public string DropDownList1Selected { get; set; } = string.Empty;
        public List<SelectListItem> DropDownList2Items { get; set; } = new();
        public string DropDownList2Selected { get; set; } = string.Empty;
        public List<SelectListItem> cboOrientationItems { get; set; } = new();
        public string cboOrientationSelected { get; set; } = string.Empty;
        public string txtHome { get; set; } = string.Empty;
        public string txtWidth { get; set; } = string.Empty;
        public string txtHeight { get; set; } = string.Empty;
        public string lblStatus { get; set; } = string.Empty;
    }
}
