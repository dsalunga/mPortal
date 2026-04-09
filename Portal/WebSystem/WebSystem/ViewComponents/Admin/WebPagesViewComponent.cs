using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
                SiteId = siteId,
                SelectedPageId = selectedPageId,
                Pages = new List<WebPageItem>()
            };

            return View(model);
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
