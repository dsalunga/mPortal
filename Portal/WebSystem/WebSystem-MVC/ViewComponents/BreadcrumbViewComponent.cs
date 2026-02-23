using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Renders breadcrumb navigation trail. Replaces Breadcrumb.ascx (Central).
    /// Usage: @await Component.InvokeAsync("Breadcrumb")
    /// </summary>
    public class BreadcrumbViewComponent : WViewComponent
    {
        public BreadcrumbViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke()
        {
            var query = WcmsContext.Query;
            var breadcrumb = new CentralBreadcrumb(query);
            var html = breadcrumb.Render();

            return View(model: html ?? string.Empty);
        }
    }
}
