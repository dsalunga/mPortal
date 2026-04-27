using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
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
            var resolvedTemplateId = ResolveTemplateId(selectedTemplateId);
            var partControlId = ResolvePartControlId();
            var model = new WebPartControlTemplatesViewModel
            {
                SelectedTemplateId = resolvedTemplateId,
                Templates = new List<ControlTemplateItem>()
            };

            try
            {
                if (partControlId <= 0 && resolvedTemplateId > 0)
                    partControlId = WebPartControlTemplate.Get(resolvedTemplateId)?.PartControlId ?? 0;

                var templates = partControlId > 0
                    ? WebPartControlTemplate.GetList(partControlId)?.OrderBy(i => i.Name).ToList() ?? new List<WebPartControlTemplate>()
                    : new List<WebPartControlTemplate>();

                model.Templates = templates.Select(template => new ControlTemplateItem
                    {
                        Id = template.Id,
                        Name = template.Name,
                        Description = template.FileName,
                        ControlType = template.TemplateEngineId == TemplateEngineTypes.ASPX ? "ASPX" : "Razor",
                        IsDefault = template.IsStandalone,
                        ModifiedDate = null
                    })
                    .ToList();

                if (model.SelectedTemplateId < 1 && model.Templates.Count > 0)
                    model.SelectedTemplateId = model.Templates[0].Id;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Failed to load control templates: {ex.Message}";
            }

            return View(model);
        }

        private int ResolveTemplateId(int selectedTemplateId)
        {
            if (selectedTemplateId > 0)
                return selectedTemplateId;

            return DataUtil.GetId(Request, WebColumns.PartControlTemplateId);
        }

        private int ResolvePartControlId()
        {
            return DataUtil.GetId(Request, WebColumns.PartControlId);
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
