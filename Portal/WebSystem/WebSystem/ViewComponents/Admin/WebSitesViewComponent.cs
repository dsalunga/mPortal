using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Site list with tree navigation. Replaces WebSites.ascx (Central/WebSites).
    /// Usage: @await Component.InvokeAsync("WebSites")
    /// </summary>
    public class WebSitesViewComponent : WViewComponent
    {
        public WebSitesViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int selectedSiteId = 0)
        {
            var model = new WebSitesViewModel
            {
                SelectedSiteId = selectedSiteId,
                Sites = new List<WebSiteNode>()
            };

            return View(model);
        }
    }

    public class WebSitesViewModel
    {
        public int SelectedSiteId { get; set; }
        public List<WebSiteNode> Sites { get; set; } = new List<WebSiteNode>();
        public string ErrorMessage { get; set; }
    }

    public class WebSiteNode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public int PageCount { get; set; }
        public List<WebSiteNode> Children { get; set; } = new List<WebSiteNode>();
    }
}
