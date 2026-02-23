using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Template list manager. Replaces WebTemplates.ascx (Central/Template).
    /// Usage: @await Component.InvokeAsync("WebTemplates")
    /// </summary>
    public class WebTemplatesViewComponent : WViewComponent
    {
        public WebTemplatesViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke()
        {
            var model = new WebTemplatesViewModel
            {
                Templates = new List<WebTemplateItem>()
            };

            return View(model);
        }
    }

    public class WebTemplatesViewModel
    {
        public List<WebTemplateItem> Templates { get; set; } = new List<WebTemplateItem>();
        public int SelectedTemplateId { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class WebTemplateItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ThemeName { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
