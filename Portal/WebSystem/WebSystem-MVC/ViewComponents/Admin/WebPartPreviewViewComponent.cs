using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Web part preview. Replaces WebPartPreview.ascx (Central root).
    /// Usage: @await Component.InvokeAsync("WebPartPreview", new { webPartId = 1 })
    /// </summary>
    public class WebPartPreviewViewComponent : WViewComponent
    {
        public WebPartPreviewViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int webPartId = 0)
        {
            var model = new WebPartPreviewViewModel
            {
                WebPartId = webPartId,
                Properties = new List<PreviewPropertyItem>()
            };

            return View(model);
        }
    }

    public class WebPartPreviewViewModel
    {
        public int WebPartId { get; set; }
        public string WebPartName { get; set; }
        public string Description { get; set; }
        public string PreviewHtml { get; set; }
        public List<PreviewPropertyItem> Properties { get; set; } = new List<PreviewPropertyItem>();
        public bool IsLoaded { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class PreviewPropertyItem
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string DataType { get; set; }
    }
}
