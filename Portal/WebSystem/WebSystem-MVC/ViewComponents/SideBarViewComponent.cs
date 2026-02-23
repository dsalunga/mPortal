using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Renders sidebar container with child web parts. Replaces SideBar.ascx (Central).
    /// Usage: @await Component.InvokeAsync("SideBar", new { cssClass = "sidebar-left" })
    /// </summary>
    public class SideBarViewComponent : WViewComponent
    {
        public SideBarViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(string cssClass = "sidebar")
        {
            return View(model: cssClass);
        }
    }
}
