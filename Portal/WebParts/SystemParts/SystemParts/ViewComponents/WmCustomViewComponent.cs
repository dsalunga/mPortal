using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from WM_Custom.ascx (SystemParts/Content).
    /// </summary>
    public class WmCustomViewComponent : WViewComponent
    {
        public WmCustomViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new WmCustomViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class WmCustomViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string hIsPlainTextDefault { get; set; } = string.Empty;
        public List<SelectListItem> cboSitesItems { get; set; } = new();
        public string cboSitesSelected { get; set; } = string.Empty;
        public List<object> ObjectDataSource1Data { get; set; } = new();
        public string txtTitle { get; set; } = string.Empty;
        public bool chkActiveContent { get; set; }
        public string litID { get; set; } = string.Empty;
        public List<SelectListItem> cboSiteIDItems { get; set; } = new();
        public string cboSiteIDSelected { get; set; } = string.Empty;
        public bool chkIsActive { get; set; }
        public bool chkEditorSensitive { get; set; }
    }
}
