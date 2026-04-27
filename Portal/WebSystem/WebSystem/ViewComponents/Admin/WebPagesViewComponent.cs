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
    /// Page list under a site. Replaces WebPages.ascx (Central/WebSites).
    /// Usage: @await Component.InvokeAsync("WebPages")
    /// </summary>
    public class WebPagesViewComponent : WViewComponent
    {
        public WebPagesViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int siteId = 0, int selectedPageId = 0)
        {
            var model = new WebPagesViewModel
            {
                SiteId = ResolveSiteId(siteId),
                SelectedPageId = ResolveSelectedPageId(selectedPageId),
                Pages = new List<WebPageItem>()
            };

            try
            {
                if (model.SiteId < 1)
                {
                    model.SiteId = WSite.GetList()?.OrderBy(i => i.Rank).Select(i => i.Id).FirstOrDefault() ?? 0;
                }

                var site = model.SiteId > 0 ? WSite.Get(model.SiteId) : null;
                model.SiteName = site?.Name;

                var pages = model.SiteId > 0
                    ? (WPage.GetList(model.SiteId)?.OrderBy(i => i.Rank).ThenBy(i => i.Name).ToList() ?? new List<WPage>())
                    : new List<WPage>();

                model.Pages = pages.Select(page => new WebPageItem
                    {
                        Id = page.Id,
                        Name = page.Name,
                        Title = page.EvaluatedTitle,
                        Url = SafePageUrl(page),
                        SortOrder = page.Rank,
                        IsActive = page.IsActive,
                        IsDefault = site != null && page.Id == site.HomePageId,
                        ModifiedDate = null
                    })
                    .ToList();

                if (model.SelectedPageId < 1 && model.Pages.Count > 0)
                    model.SelectedPageId = model.Pages[0].Id;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Failed to load pages: {ex.Message}";
            }

            return View(model);
        }

        private int ResolveSiteId(int siteId)
        {
            if (siteId > 0)
                return siteId;

            return DataUtil.GetId(Request, WebColumns.SiteId);
        }

        private int ResolveSelectedPageId(int selectedPageId)
        {
            if (selectedPageId > 0)
                return selectedPageId;

            return DataUtil.GetId(Request, WebColumns.PageId);
        }

        private static string SafePageUrl(WPage page)
        {
            try
            {
                return page.BuildRelativeUrl();
            }
            catch
            {
                return page.Identity;
            }
        }
    }

    public class WebPagesViewModel
    {
        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public int SelectedPageId { get; set; }
        public List<WebPageItem> Pages { get; set; } = new List<WebPageItem>();
        public string ErrorMessage { get; set; }
    }

    public class WebPageItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
