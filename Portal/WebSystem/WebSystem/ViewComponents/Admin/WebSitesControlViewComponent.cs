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
                SelectedSiteId = DataUtil.GetId(Request, WebColumns.SiteId),
                IsLoaded = true
            };

            try
            {
                var sites = WSite.GetList()?.OrderBy(i => i.Rank).ThenBy(i => i.Name).ToList() ?? new List<WSite>();
                model.Sites = BuildTree(sites, -1);

                if (model.SelectedSiteId < 1 && model.Sites.Count > 0)
                    model.SelectedSiteId = model.Sites[0].Id;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Failed to load site tree: {ex.Message}";
                model.IsLoaded = false;
            }

            return View(model);
        }

        private List<SiteNode> BuildTree(List<WSite> allSites, int parentId)
        {
            return allSites
                .Where(site => site.ParentId == parentId)
                .OrderBy(site => site.Rank)
                .ThenBy(site => site.Name)
                .Select(site => new SiteNode
                {
                    Id = site.Id,
                    Name = site.Name,
                    ParentId = site.ParentId,
                    IsActive = site.IsActive,
                    Url = SafeSiteUrl(site),
                    Children = BuildTree(allSites, site.Id)
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
