using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Site map hierarchy view. Replaces SiteMap.ascx (Central root).
    /// Usage: @await Component.InvokeAsync("SiteMap", new { expandAll = true })
    /// </summary>
    public class SiteMapViewComponent : WViewComponent
    {
        public SiteMapViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(bool expandAll = false)
        {
            var model = new SiteMapViewModel
            {
                ExpandAll = expandAll,
                RootNodes = new List<SiteMapNode>()
            };

            return View(model);
        }
    }

    public class SiteMapViewModel
    {
        public List<SiteMapNode> RootNodes { get; set; } = new List<SiteMapNode>();
        public bool ExpandAll { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class SiteMapNode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string NodeType { get; set; }
        public bool IsActive { get; set; }
        public List<SiteMapNode> Children { get; set; } = new List<SiteMapNode>();
    }
}
