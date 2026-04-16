using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from AdminTabControl.ascx (SystemParts/Menu/Controls).
    /// </summary>
    public class MenuAdmintabcontrolViewComponent : WViewComponent
    {
        public MenuAdmintabcontrolViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new MenuAdmintabcontrolViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/Menu/Controls/AdminTabControl/Default.cshtml", model);
        }
    }

        public class MenuAdmintabcontrolViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<MenuAdmintabcontrolItem> Items { get; set; } = new();
    }

    public class MenuAdmintabcontrolItem
    {
        public string Name { get; set; } = string.Empty;
    }
}
