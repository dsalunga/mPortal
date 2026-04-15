using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from ConfigLinkedMenuEdit.ascx (SystemParts/Menu).
    /// </summary>
    public class ConfiglinkedmenueditViewComponent : WViewComponent
    {
        public ConfiglinkedmenueditViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ConfiglinkedmenueditViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class ConfiglinkedmenueditViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string txtCaption { get; set; } = string.Empty;
        public string txtNavigateURL { get; set; } = string.Empty;
        public List<SelectListItem> ObjectDataSource1Items { get; set; } = new();
        public string ObjectDataSource1Selected { get; set; } = string.Empty;
        public List<SelectListItem> cboMenusItems { get; set; } = new();
        public string cboMenusSelected { get; set; } = string.Empty;
        public List<SelectListItem> cboMenuItemsItems { get; set; } = new();
        public string cboMenuItemsSelected { get; set; } = string.Empty;
        public List<SelectListItem> cboItemPostionItems { get; set; } = new();
        public string cboItemPostionSelected { get; set; } = string.Empty;
        public bool chkIsActive { get; set; }
        public bool chkCheckPermission { get; set; }
    }
}
