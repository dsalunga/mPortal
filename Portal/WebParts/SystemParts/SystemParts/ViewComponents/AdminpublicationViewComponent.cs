using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from AdminPublication.ascx (SystemParts/Article).
    /// </summary>
    public class AdminpublicationViewComponent : WViewComponent
    {
        public AdminpublicationViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdminpublicationViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class AdminpublicationViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<SelectListItem> cboSitesItems { get; set; } = new();
        public string cboSitesSelected { get; set; } = string.Empty;
        public List<object> ObjectDataSource1Data { get; set; } = new();
        public string txtTitle { get; set; } = string.Empty;
        public List<SelectListItem> ddlMonthItems { get; set; } = new();
        public string ddlMonthSelected { get; set; } = string.Empty;
        public List<SelectListItem> ddlDayItems { get; set; } = new();
        public string ddlDaySelected { get; set; } = string.Empty;
        public List<SelectListItem> ddlYearItems { get; set; } = new();
        public string ddlYearSelected { get; set; } = string.Empty;
        public string txtTags { get; set; } = string.Empty;
        public string litID { get; set; } = string.Empty;
        public string txtAuthor { get; set; } = string.Empty;
        public List<SelectListItem> ddlSiteIDItems { get; set; } = new();
        public string ddlSiteIDSelected { get; set; } = string.Empty;
        public bool chkIsActive { get; set; }
    }
}
