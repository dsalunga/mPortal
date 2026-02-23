using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Control template manager. Replaces WebPartControlTemplates.ascx (Central/WebPart).
    /// Usage: @await Component.InvokeAsync("WebPartControlTemplates")
    /// </summary>
    public class WebPartControlTemplatesViewComponent : WViewComponent
    {
        public WebPartControlTemplatesViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int selectedTemplateId = 0)
        {
            var model = new WebPartControlTemplatesViewModel
            {
                SelectedTemplateId = selectedTemplateId,
                Templates = new List<ControlTemplateItem>()
            };
            return View(model);
        }
    }

    public class WebPartControlTemplatesViewModel
    {
        public int SelectedTemplateId { get; set; }
        public List<ControlTemplateItem> Templates { get; set; } = new List<ControlTemplateItem>();
        public string ErrorMessage { get; set; }
    }

    public class ControlTemplateItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ControlType { get; set; }
        public bool IsDefault { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
