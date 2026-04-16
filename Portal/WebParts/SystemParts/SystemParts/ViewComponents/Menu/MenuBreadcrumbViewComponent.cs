using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from Breadcrumb.ascx (SystemParts/Menu).
    /// </summary>
    public class MenuBreadcrumbViewComponent : WViewComponent
    {
        public MenuBreadcrumbViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new MenuBreadcrumbViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/Menu/Breadcrumb/Default.cshtml", model);
        }
    }

        public class MenuBreadcrumbViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<MenuBreadcrumbItem> Items { get; set; } = new();
    }

    public class MenuBreadcrumbItem
    {
        public string Name { get; set; } = string.Empty;
    }
}
