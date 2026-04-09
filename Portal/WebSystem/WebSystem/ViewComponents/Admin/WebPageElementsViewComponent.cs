using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Page elements list. Replaces WebPageElements.ascx (Central/WebSites).
    /// Usage: @await Component.InvokeAsync("WebPageElements")
    /// </summary>
    public class WebPageElementsViewComponent : WViewComponent
    {
        public WebPageElementsViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int pageId = 0)
        {
            var model = new WebPageElementsViewModel
            {
                PageId = pageId,
                Elements = new List<PageElementItem>()
            };

            return View(model);
        }
    }

    public class WebPageElementsViewModel
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public List<PageElementItem> Elements { get; set; } = new List<PageElementItem>();
        public string ErrorMessage { get; set; }
    }

    public class PageElementItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ElementType { get; set; }
        public string PanelName { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
