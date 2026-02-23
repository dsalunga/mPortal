using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Template HTML editor. Replaces WebTemplateEditor.ascx (Central/Template).
    /// Usage: @await Component.InvokeAsync("WebTemplateEditor")
    /// </summary>
    public class WebTemplateEditorViewComponent : WViewComponent
    {
        public WebTemplateEditorViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int templateId = 0)
        {
            var model = new WebTemplateEditorViewModel
            {
                TemplateId = templateId,
                AvailablePanelZones = new List<string> { "Header", "Content", "Sidebar", "Footer" }
            };

            return View(model);
        }
    }

    public class WebTemplateEditorViewModel
    {
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string HtmlContent { get; set; }
        public string CssContent { get; set; }
        public List<string> AvailablePanelZones { get; set; } = new List<string>();
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
    }
}
