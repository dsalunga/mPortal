using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from ConfigContent.ascx (SystemParts/Content).
    /// </summary>
    public class ConfigcontentViewComponent : WViewComponent
    {
        public ConfigcontentViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ConfigcontentViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class ConfigcontentViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string txtTitle { get; set; } = string.Empty;
        public bool chkActiveContent { get; set; }
        public List<object> grdHistoryData { get; set; } = new();
        public List<object> grdDraftData { get; set; } = new();
        public string litID { get; set; } = string.Empty;
        public List<SelectListItem> ddlSiteIDItems { get; set; } = new();
        public string ddlSiteIDSelected { get; set; } = string.Empty;
        public List<SelectListItem> ddlRankItems { get; set; } = new();
        public string ddlRankSelected { get; set; } = string.Empty;
        public bool chkIsActive { get; set; }
        public string hidId { get; set; } = string.Empty;
    }
}
