using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
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
                PageId = ResolvePageId(pageId),
                Elements = new List<PageElementItem>()
            };

            try
            {
                if (model.PageId <= 0)
                    model.PageId = DataUtil.GetId(Request, WebColumns._PageId);

                var page = model.PageId > 0 ? WPage.Get(model.PageId) : null;
                model.PageName = page?.Name;

                var items = model.PageId > 0
                    ? WebPageElement.GetList(model.PageId, WebObjects.WebPage)?.OrderBy(i => i.Rank).ToList() ?? new List<WebPageElement>()
                    : new List<WebPageElement>();

                model.Elements = items.Select(item => new PageElementItem
                    {
                        Id = item.Id,
                        Name = item.Name,
                        ElementType = ResolveElementType(item),
                        PanelName = item.Panel?.Name ?? item.Panel?.PanelName ?? string.Empty,
                        SortOrder = item.Rank,
                        IsActive = item.IsActive,
                        ModifiedDate = null
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Failed to load page elements: {ex.Message}";
            }

            return View(model);
        }

        private int ResolvePageId(int pageId)
        {
            if (pageId > 0)
                return pageId;

            return DataUtil.GetId(Request, WebColumns.PageId);
        }

        private static string ResolveElementType(WebPageElement element)
        {
            var template = element.PartControlTemplate;
            if (template == null)
                return "Unknown";

            var part = template.Part;
            if (part == null)
                return template.Name;

            return $"{part.Name} / {template.Name}";
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
