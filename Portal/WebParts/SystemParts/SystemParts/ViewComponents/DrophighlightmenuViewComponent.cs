using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from DropHighlightMenu.ascx (SystemParts/Menu).
    /// </summary>
    public class DrophighlightmenuViewComponent : WViewComponent
    {
        public DrophighlightmenuViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new DrophighlightmenuViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class DrophighlightmenuViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<DrophighlightmenuItem> Items { get; set; } = new();
    }

    public class DrophighlightmenuItem
    {
        public string Name { get; set; } = string.Empty;
    }
}
