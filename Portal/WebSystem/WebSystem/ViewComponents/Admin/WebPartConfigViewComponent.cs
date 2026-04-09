using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Web part configuration editor. Replaces WebPartConfig.ascx (Central/WebPart).
    /// Usage: @await Component.InvokeAsync("WebPartConfig", new { webPartId = id })
    /// </summary>
    public class WebPartConfigViewComponent : WViewComponent
    {
        public WebPartConfigViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int webPartId = 0)
        {
            var model = new WebPartConfigViewModel
            {
                WebPartId = webPartId,
                Properties = new List<WebPartPropertyItem>()
            };
            return View(model);
        }
    }

    public class WebPartConfigViewModel
    {
        public int WebPartId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; }
        public List<WebPartPropertyItem> Properties { get; set; } = new List<WebPartPropertyItem>();
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class WebPartPropertyItem
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string DataType { get; set; }
        public string Description { get; set; }
    }
}
