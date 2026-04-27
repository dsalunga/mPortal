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
    /// Page template panels. Replaces WebPagePanels.ascx (Central/WebSites).
    /// Usage: @await Component.InvokeAsync("WebPagePanels")
    /// </summary>
    public class WebPagePanelsViewComponent : WViewComponent
    {
        public WebPagePanelsViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int pageId = 0)
        {
            var resolvedPageId = ResolvePageId(pageId);
            var masterPageId = DataUtil.GetId(Request, WebColumns.MasterPageId);
            var model = new WebPagePanelsViewModel
            {
                PageId = resolvedPageId,
                Panels = new List<PagePanelItem>()
            };

            try
            {
                IEnumerable<WebTemplatePanel> templatePanels = Array.Empty<WebTemplatePanel>();
                WPage page = null;

                if (resolvedPageId > 0)
                {
                    page = WPage.Get(resolvedPageId);
                    model.PageName = page?.Name;

                    if (page != null)
                    {
                        var evalPanel = page.Panel;
                        if (evalPanel != null)
                        {
                            model.TemplateName = evalPanel.Template?.Name;
                            templatePanels = WebTemplatePanel.Provider.GetList(evalPanel.ObjectId, evalPanel.RecordId) ?? Array.Empty<WebTemplatePanel>();
                        }
                    }
                }
                else if (masterPageId > 0)
                {
                    var master = WebMasterPage.Get(masterPageId);
                    if (master != null)
                    {
                        model.TemplateName = master.Template?.Name;
                        templatePanels = WebTemplatePanel.Provider.GetList(WebObjects.WebTemplate, master.TemplateId) ?? Array.Empty<WebTemplatePanel>();
                    }
                }

                model.Panels = templatePanels
                    .OrderBy(i => i.Rank)
                    .ThenBy(i => i.Name)
                    .Select(panel =>
                    {
                        var usage = resolvedPageId > 0 ? WebPagePanel.Get(panel.Id, resolvedPageId) : null;
                        return new PagePanelItem
                        {
                            Id = panel.Id,
                            Name = panel.Name,
                            PanelType = PanelUsage.ToString(usage?.UsageTypeId ?? PanelUsage.Inherit),
                            SortOrder = panel.Rank,
                            ElementCount = ResolveElementCount(resolvedPageId, masterPageId, panel.Id),
                            IsActive = true
                        };
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Failed to load page panels: {ex.Message}";
            }

            return View(model);
        }

        private int ResolvePageId(int pageId)
        {
            if (pageId > 0)
                return pageId;

            return DataUtil.GetId(Request, WebColumns.PageId);
        }

        private static int ResolveElementCount(int pageId, int masterPageId, int templatePanelId)
        {
            try
            {
                var count = 0;
                if (pageId > 0)
                    count += WebPageElement.GetCount(pageId, WebObjects.WebPage, templatePanelId);
                if (masterPageId > 0)
                    count += WebPageElement.GetCount(masterPageId, WebObjects.WebMasterPage, templatePanelId);

                return count;
            }
            catch
            {
                return 0;
            }
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
