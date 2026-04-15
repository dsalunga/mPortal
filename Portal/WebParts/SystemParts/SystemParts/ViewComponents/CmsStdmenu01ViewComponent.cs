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
        public List<SelectOption> CboOrientationOptions { get; set; } = new();
        public List<SelectOption> DropDownList1Options { get; set; } = new();
        public List<SelectOption> DropDownList2Options { get; set; } = new();
        public string Height { get; set; } = string.Empty;
        public string Home { get; set; } = string.Empty;
        public string SelectedCboOrientation { get; set; } = string.Empty;
        public string SelectedDropDownList1 { get; set; } = string.Empty;
        public string SelectedDropDownList2 { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
        public string Width { get; set; } = string.Empty;
    }
    }
