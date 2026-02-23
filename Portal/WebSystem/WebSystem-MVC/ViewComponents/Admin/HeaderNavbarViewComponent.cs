using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Admin top navbar. Replaces HeaderNavbar.ascx (Central root).
    /// Usage: @await Component.InvokeAsync("HeaderNavbar", new { activeMenuId = "dashboard" })
    /// </summary>
    public class HeaderNavbarViewComponent : WViewComponent
    {
        public HeaderNavbarViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(string activeMenuId = "")
        {
            var model = new HeaderNavbarViewModel
            {
                ActiveMenuId = activeMenuId,
                MenuItems = new List<NavbarMenuItem>()
            };

            return View(model);
        }
    }

    public class HeaderNavbarViewModel
    {
        public List<NavbarMenuItem> MenuItems { get; set; } = new List<NavbarMenuItem>();
        public string ActiveMenuId { get; set; }
        public string UserName { get; set; }
        public bool IsAdminMode { get; set; }
    }

    public class NavbarMenuItem
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Url { get; set; }
        public string IconClass { get; set; }
        public bool IsActive { get; set; }
        public List<NavbarMenuItem> Children { get; set; } = new List<NavbarMenuItem>();
    }
}
