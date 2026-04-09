using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Single site editor form. Replaces WebSite.ascx (Central/WebSites).
    /// Usage: @await Component.InvokeAsync("WebSite")
    /// </summary>
    public class WebSiteViewComponent : WViewComponent
    {
        public WebSiteViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int siteId = 0)
        {
            var model = new WebSiteViewModel
            {
                SiteId = siteId,
                AvailableThemes = new List<string>()
            };

            return View(model);
        }
    }

    public class WebSiteViewModel
    {
        public int SiteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string DefaultPage { get; set; }
        public string ThemeName { get; set; }
        public bool IsActive { get; set; }
        public List<string> AvailableThemes { get; set; } = new List<string>();
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
    }
}
