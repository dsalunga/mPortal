using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
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
            var resolvedExpandAll = expandAll || string.Equals(Request.Query["ExpandAll"], "1", StringComparison.OrdinalIgnoreCase);
            var model = new SiteMapViewModel
            {
                ExpandAll = resolvedExpandAll,
                RootNodes = new List<SiteMapNode>()
            };

            try
            {
                var sites = WSite.GetList()?.OrderBy(i => i.Rank).ThenBy(i => i.Name).ToList() ?? new List<WSite>();
                model.RootNodes = sites
                    .Where(i => i.ParentId < 0)
                    .Select(site => new SiteMapNode
                    {
                        Id = site.Id,
                        Name = site.Name,
                        Url = CentralPages.WebSiteHome + $"?{WebColumns.SiteId}={site.Id}",
                        NodeType = "site",
                        IsActive = site.IsActive,
                        Children = BuildPageNodes(site.Id)
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Failed to load site map: {ex.Message}";
            }

            return View(model);
        }

        private List<SiteMapNode> BuildPageNodes(int siteId)
        {
            var pages = WPage.GetList(siteId)?.OrderBy(i => i.Rank).ThenBy(i => i.Name).ToList() ?? new List<WPage>();
            return BuildPageNodes(pages, -1);
        }

        private static List<SiteMapNode> BuildPageNodes(List<WPage> pages, int parentId)
        {
            return pages
                .Where(i => i.ParentId == parentId)
                .OrderBy(i => i.Rank)
                .ThenBy(i => i.Name)
                .Select(page => new SiteMapNode
                {
                    Id = page.Id,
                    Name = page.Name,
                    Url = CentralPages.WebPageHome + $"?{WebColumns.PageId}={page.Id}&{WebColumns.SiteId}={page.SiteId}",
                    NodeType = "page",
                    IsActive = page.IsActive,
                    Children = BuildPageNodes(pages, page.Id)
                })
                .ToList();
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
