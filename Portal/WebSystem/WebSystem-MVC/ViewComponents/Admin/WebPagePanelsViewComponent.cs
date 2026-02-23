using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Page template panels. Replaces WebPagePanels.ascx (Central/WebSites).
    /// Usage: @await Component.InvokeAsync("WebPagePanels")
    /// </summary>
    public class WebPagePanelsViewComponent : WViewComponent
    {
        public WebPagePanelsViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int pageId = 0)
        {
            var model = new WebPagePanelsViewModel
            {
                PageId = pageId,
                Panels = new List<PagePanelItem>()
            };

            return View(model);
        }
    }

    public class WebPagePanelsViewModel
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string TemplateName { get; set; }
        public List<PagePanelItem> Panels { get; set; } = new List<PagePanelItem>();
        public string ErrorMessage { get; set; }
    }

    public class PagePanelItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PanelType { get; set; }
        public int SortOrder { get; set; }
        public int ElementCount { get; set; }
        public bool IsActive { get; set; }
    }
}
