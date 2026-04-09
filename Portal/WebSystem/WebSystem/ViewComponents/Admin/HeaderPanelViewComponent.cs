using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Admin header panel. Replaces HeaderPanel.ascx (Central root).
    /// Usage: @await Component.InvokeAsync("HeaderPanel")
    /// </summary>
    public class HeaderPanelViewComponent : WViewComponent
    {
        public HeaderPanelViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke()
        {
            var model = new HeaderPanelViewModel();

            return View(model);
        }
    }

    public class HeaderPanelViewModel
    {
        public string SiteName { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int NotificationCount { get; set; }
        public bool IsAdminMode { get; set; }
    }
}
