using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from SM_Surveys.ascx (SystemParts/GenericList).
    /// </summary>
    public class SmSurveysViewComponent : WViewComponent
    {
        public SmSurveysViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new SmSurveysViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class SmSurveysViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<SelectListItem> cboSortItems { get; set; } = new();
        public string cboSortSelected { get; set; } = string.Empty;
        public List<SelectListItem> cboOrderItems { get; set; } = new();
        public string cboOrderSelected { get; set; } = string.Empty;
        public List<SelectListItem> cboSitesItems { get; set; } = new();
        public string cboSitesSelected { get; set; } = string.Empty;
    }
}
