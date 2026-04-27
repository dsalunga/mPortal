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
                SelectedSiteId = ResolveSelectedSiteId(selectedSiteId),
                Sites = new List<WebSiteNode>()
            };

            try
            {
                var sites = WSite.GetList()?.OrderBy(i => i.Rank).ThenBy(i => i.Name).ToList() ?? new List<WSite>();
                var pageCounts = BuildPageCountMap(sites);

                model.Sites = BuildTree(sites, -1, pageCounts);
                if (model.SelectedSiteId < 1 && model.Sites.Count > 0)
                    model.SelectedSiteId = model.Sites[0].Id;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Failed to load sites: {ex.Message}";
            }

            return View(model);
        }

        private int ResolveSelectedSiteId(int selectedSiteId)
        {
            if (selectedSiteId > 0)
                return selectedSiteId;

            return DataUtil.GetId(Request, WebColumns.SiteId);
        }

        private static Dictionary<int, int> BuildPageCountMap(IEnumerable<WSite> sites)
        {
            var counts = new Dictionary<int, int>();
            foreach (var site in sites)
            {
                try
                {
                    counts[site.Id] = WPage.GetCount(site.Id);
                }
                catch
                {
                    counts[site.Id] = 0;
                }
            }

            return counts;
        }

        private List<WebSiteNode> BuildTree(List<WSite> allSites, int parentId, IReadOnlyDictionary<int, int> pageCounts)
        {
            return allSites
                .Where(site => site.ParentId == parentId)
                .OrderBy(site => site.Rank)
                .ThenBy(site => site.Name)
                .Select(site => new WebSiteNode
                {
                    Id = site.Id,
                    Name = site.Name,
                    Url = SafeSiteUrl(site),
                    IsActive = site.IsActive,
                    PageCount = pageCounts.TryGetValue(site.Id, out var count) ? count : 0,
                    Children = BuildTree(allSites, site.Id, pageCounts)
                })
                .ToList();
        }

        private static string SafeSiteUrl(WSite site)
        {
            try
            {
                return site.BuildRelativeUrl();
            }
            catch
            {
                return site.Identity;
            }
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
