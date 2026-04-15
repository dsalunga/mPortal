using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from ConfigAnimatedBanner.ascx (SystemParts/Photo).
    /// </summary>
    public class ConfiganimatedbannerViewComponent : WViewComponent
    {
        public ConfiganimatedbannerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ConfiganimatedbannerViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class ConfiganimatedbannerViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<SelectListItem> cboAlbumItems { get; set; } = new();
        public string cboAlbumSelected { get; set; } = string.Empty;
        public List<SelectListItem> cboRenderModeItems { get; set; } = new();
        public string cboRenderModeSelected { get; set; } = string.Empty;
        public string lblStatus { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
