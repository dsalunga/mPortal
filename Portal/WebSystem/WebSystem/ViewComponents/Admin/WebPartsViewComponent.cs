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
                SelectedPartId = ResolveSelectedPartId(selectedPartId),
                Parts = new List<WebPartItem>()
            };

            try
            {
                var parts = WPart.GetList()?.OrderBy(i => i.Name).ToList() ?? new List<WPart>();
                model.Parts = parts.Select(part => new WebPartItem
                    {
                        Id = part.Id,
                        Name = part.Name,
                        Description = part.GetParameterValue(WebColumns.Description, string.Empty) ?? string.Empty,
                        Category = part.GetParameterValue("Category", string.Empty) ?? string.Empty,
                        IsActive = part.IsActive,
                        ControlCount = CountControls(part),
                        Children = new List<WebPartItem>()
                    })
                    .ToList();

                if (model.SelectedPartId < 1 && model.Parts.Count > 0)
                    model.SelectedPartId = model.Parts[0].Id;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Failed to load web parts: {ex.Message}";
            }

            return View(model);
        }

        private int ResolveSelectedPartId(int selectedPartId)
        {
            if (selectedPartId > 0)
                return selectedPartId;

            return DataUtil.GetId(Request, WebColumns.PartId);
        }

        private static int CountControls(WPart part)
        {
            try
            {
                return WebPartControl.GetList(part.Id)?.Count() ?? 0;
            }
            catch
            {
                return 0;
            }
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
