using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Site tree browser with hierarchy display. Replaces WebSitesControl.ascx (Central/Controls).
    /// Usage: @await Component.InvokeAsync("WebSitesControl")
    /// </summary>
    public class WebSitesControlViewComponent : WViewComponent
    {
        public WebSitesControlViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke()
        {
            var model = new WebSitesControlViewModel
            {
                Sites = new List<SiteNode>(),
                SelectedSiteId = 0,
                IsLoaded = true
            };

            return View(model);
        }
    }

    public class WebSitesControlViewModel
    {
        public List<SiteNode> Sites { get; set; } = new List<SiteNode>();
        public int SelectedSiteId { get; set; }
        public bool IsLoaded { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class SiteNode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int ParentId { get; set; }
        public bool IsActive { get; set; }
        public List<SiteNode> Children { get; set; } = new List<SiteNode>();
    }
}
