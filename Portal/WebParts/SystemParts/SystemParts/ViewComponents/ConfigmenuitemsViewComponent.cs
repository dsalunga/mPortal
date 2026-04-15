using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from ConfigMenuItems.ascx (SystemParts/Menu).
    /// </summary>
    public class ConfigmenuitemsViewComponent : WViewComponent
    {
        public ConfigmenuitemsViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ConfigmenuitemsViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class ConfigmenuitemsViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<SelectListItem> DropDownList1Items { get; set; } = new();
        public string DropDownList1Selected { get; set; } = string.Empty;
        public List<object> ObjectDataSource1Data { get; set; } = new();
        public string Image1Url { get; set; } = string.Empty;
        public string Image2Url { get; set; } = string.Empty;
    }
}
