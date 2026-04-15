using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from ConfigDashboard.ascx (SystemParts/Article).
    /// </summary>
    public class ConfigdashboardViewComponent : WViewComponent
    {
        public ConfigdashboardViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ConfigdashboardViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class ConfigdashboardViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string hSiteId { get; set; } = string.Empty;
        public List<SelectListItem> cboSubscriptionModeItems { get; set; } = new();
        public string cboSubscriptionModeSelected { get; set; } = string.Empty;
        public string txtGroupId { get; set; } = string.Empty;
        public string txtIgnoreGroups { get; set; } = string.Empty;
        public bool chkUsePageParameter { get; set; }
        public string txtNavigateURL { get; set; } = string.Empty;
        public List<object> ObjectDataSource1Data { get; set; } = new();
        public string lblStatus { get; set; } = string.Empty;
    }
}
