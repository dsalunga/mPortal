using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from Breadcrumb.ascx (SystemParts/Navigation).
    /// </summary>
    public class NavigationBreadcrumbViewComponent : WViewComponent
    {
        public NavigationBreadcrumbViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new NavigationBreadcrumbViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class NavigationBreadcrumbViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<NavigationBreadcrumbItem> Items { get; set; } = new();
    }

    public class NavigationBreadcrumbItem
    {
        public string Name { get; set; } = string.Empty;
    }
}
