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
                ParentSiteId = ResolveParentSiteId(parentSiteId),
                ChildSites = new List<ChildSiteNode>()
            };

            try
            {
                var allSites = WSite.GetList()?.OrderBy(i => i.Rank).ThenBy(i => i.Name).ToList() ?? new List<WSite>();
                if (model.ParentSiteId > 0)
                {
                    var parent = allSites.FirstOrDefault(i => i.Id == model.ParentSiteId) ?? WSite.Get(model.ParentSiteId);
                    model.ParentSiteName = parent?.Name;
                }

                model.ChildSites = BuildTree(allSites, model.ParentSiteId <= 0 ? -1 : model.ParentSiteId);
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Failed to load child sites: {ex.Message}";
            }

            return View(model);
        }

        private int ResolveParentSiteId(int parentSiteId)
        {
            if (parentSiteId > 0)
                return parentSiteId;

            return DataUtil.GetId(Request, WebColumns.SiteId);
        }

        private List<ChildSiteNode> BuildTree(List<WSite> allSites, int parentId)
        {
            return allSites
                .Where(i => i.ParentId == parentId)
                .OrderBy(i => i.Rank)
                .ThenBy(i => i.Name)
                .Select(i => new ChildSiteNode
                {
                    Id = i.Id,
                    Name = i.Name,
                    Url = SafeSiteUrl(i),
                    IsActive = i.IsActive,
                    PageCount = SafePageCount(i.Id),
                    Children = BuildTree(allSites, i.Id)
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

        private static int SafePageCount(int siteId)
        {
            try
            {
                return WPage.GetCount(siteId);
            }
            catch
            {
                return 0;
            }
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
