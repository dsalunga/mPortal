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
                SelectedTemplateId = DataUtil.GetId(Request, WebColumns.TemplateId),
                Templates = new List<WebTemplateItem>()
            };

            try
            {
                var templates = WebTemplate.Provider.GetList()?.OrderBy(i => i.Name).ToList() ?? new List<WebTemplate>();
                model.Templates = templates.Select(template => new WebTemplateItem
                    {
                        Id = template.Id,
                        Name = template.Name,
                        Description = template.GetParameterValue(WebColumns.Description, string.Empty) ?? string.Empty,
                        ThemeName = template.Theme?.Name ?? string.Empty,
                        IsDefault = false,
                        IsActive = true,
                        ModifiedDate = template.DateModified
                    })
                    .ToList();

                if (model.SelectedTemplateId < 1 && model.Templates.Count > 0)
                    model.SelectedTemplateId = model.Templates[0].Id;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Failed to load templates: {ex.Message}";
            }

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
