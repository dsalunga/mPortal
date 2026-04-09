using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Template panel manager. Replaces WebTemplatePanels.ascx (Central/Template).
    /// Usage: @await Component.InvokeAsync("WebTemplatePanels")
    /// </summary>
    public class WebTemplatePanelsViewComponent : WViewComponent
    {
        public WebTemplatePanelsViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int templateId = 0)
        {
            var model = new WebTemplatePanelsViewModel
            {
                TemplateId = templateId,
                Panels = new List<TemplatePanelItem>()
            };

            return View(model);
        }
    }

    public class WebTemplatePanelsViewModel
    {
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public List<TemplatePanelItem> Panels { get; set; } = new List<TemplatePanelItem>();
        public string ErrorMessage { get; set; }
    }

    public class TemplatePanelItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Zone { get; set; }
        public int SortOrder { get; set; }
        public string DefaultContent { get; set; }
        public bool IsRequired { get; set; }
    }
}
