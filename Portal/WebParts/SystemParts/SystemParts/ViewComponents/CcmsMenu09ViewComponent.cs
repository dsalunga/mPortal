using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from CCMS_Menu_09.ascx (SystemParts/Menu).
    /// </summary>
    public class CcmsMenu09ViewComponent : WViewComponent
    {
        public CcmsMenu09ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new CcmsMenu09ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class CcmsMenu09ViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string txtCaption { get; set; } = string.Empty;
        public List<SelectListItem> ddlSitesItems { get; set; } = new();
        public string ddlSitesSelected { get; set; } = string.Empty;
        public bool chkIsActive { get; set; }
    }
}
