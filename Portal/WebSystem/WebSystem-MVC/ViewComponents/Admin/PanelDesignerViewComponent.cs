using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Page panel layout designer. Replaces PanelDesigner.ascx (Central/Controls).
    /// Usage: @await Component.InvokeAsync("PanelDesigner")
    /// </summary>
    public class PanelDesignerViewComponent : WViewComponent
    {
        public PanelDesignerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int pageId = 0)
        {
            var model = new PanelDesignerViewModel
            {
                PageId = pageId,
                Panels = new List<PanelItem>(),
                AvailableLayouts = new List<string> { "Single", "Two Column", "Three Column", "Sidebar Left", "Sidebar Right" }
            };

            return View(model);
        }
    }

    public class PanelDesignerViewModel
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string SelectedLayout { get; set; }
        public List<string> AvailableLayouts { get; set; } = new List<string>();
        public List<PanelItem> Panels { get; set; } = new List<PanelItem>();
        public string ErrorMessage { get; set; }
    }

    public class PanelItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Zone { get; set; }
        public int SortOrder { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<PanelElementItem> Elements { get; set; } = new List<PanelElementItem>();
    }

    public class PanelElementItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ElementType { get; set; }
        public int SortOrder { get; set; }
    }
}
