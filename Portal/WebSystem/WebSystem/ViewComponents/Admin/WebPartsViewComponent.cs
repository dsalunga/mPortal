using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Web part list with tree navigation. Replaces WebParts.ascx (Central/WebPart).
    /// Usage: @await Component.InvokeAsync("WebParts")
    /// </summary>
    public class WebPartsViewComponent : WViewComponent
    {
        public WebPartsViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int selectedPartId = 0)
        {
            var model = new WebPartsViewModel
            {
                SelectedPartId = selectedPartId,
                Parts = new List<WebPartItem>()
            };
            return View(model);
        }
    }

    public class WebPartsViewModel
    {
        public int SelectedPartId { get; set; }
        public List<WebPartItem> Parts { get; set; } = new List<WebPartItem>();
        public string ErrorMessage { get; set; }
    }

    public class WebPartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; }
        public int ControlCount { get; set; }
        public List<WebPartItem> Children { get; set; } = new List<WebPartItem>();
    }
}
