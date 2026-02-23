using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Child site hierarchy. Replaces WebChildSites.ascx (Central/WebSites).
    /// Usage: @await Component.InvokeAsync("WebChildSites")
    /// </summary>
    public class WebChildSitesViewComponent : WViewComponent
    {
        public WebChildSitesViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int parentSiteId = 0)
        {
            var model = new WebChildSitesViewModel
            {
                ParentSiteId = parentSiteId,
                ChildSites = new List<ChildSiteNode>()
            };

            return View(model);
        }
    }

    public class WebChildSitesViewModel
    {
        public int ParentSiteId { get; set; }
        public string ParentSiteName { get; set; }
        public List<ChildSiteNode> ChildSites { get; set; } = new List<ChildSiteNode>();
        public string ErrorMessage { get; set; }
    }

    public class ChildSiteNode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public int PageCount { get; set; }
        public List<ChildSiteNode> Children { get; set; } = new List<ChildSiteNode>();
    }
}
