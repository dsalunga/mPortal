using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Single page editor form. Replaces WebPage.ascx (Central/WebSites).
    /// Usage: @await Component.InvokeAsync("WebPage")
    /// </summary>
    public class WebPageViewComponent : WViewComponent
    {
        public WebPageViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int pageId = 0, int siteId = 0)
        {
            var model = new WebPageViewModel
            {
                PageId = pageId,
                SiteId = siteId,
                AvailableTemplates = new List<string>()
            };

            return View(model);
        }
    }

    public class WebPageViewModel
    {
        public int PageId { get; set; }
        public int SiteId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string TemplateName { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public List<string> AvailableTemplates { get; set; } = new List<string>();
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
    }
}
