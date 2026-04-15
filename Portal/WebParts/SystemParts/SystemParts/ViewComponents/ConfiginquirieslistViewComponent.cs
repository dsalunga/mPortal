using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from ConfigInquiriesList.ascx (SystemParts/Contact).
    /// </summary>
    public class ConfiginquirieslistViewComponent : WViewComponent
    {
        public ConfiginquirieslistViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ConfiginquirieslistViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class ConfiginquirieslistViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string hObjectId { get; set; } = string.Empty;
        public string hRecordId { get; set; } = string.Empty;
        public List<SelectListItem> cboModeItems { get; set; } = new();
        public string cboModeSelected { get; set; } = string.Empty;
        public List<SelectListItem> ObjectDataSourceContactsItems { get; set; } = new();
        public string ObjectDataSourceContactsSelected { get; set; } = string.Empty;
        public string lblStatus { get; set; } = string.Empty;
        public List<SelectListItem> cboSitesItems { get; set; } = new();
        public string cboSitesSelected { get; set; } = string.Empty;
        public List<object> ObjectDataSource1Data { get; set; } = new();
    }
}
