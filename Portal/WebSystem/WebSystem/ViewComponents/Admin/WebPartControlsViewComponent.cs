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
    /// Control list under a web part. Replaces WebPartControls.ascx (Central/WebPart).
    /// Usage: @await Component.InvokeAsync("WebPartControls", new { webPartId = id })
    /// </summary>
    public class WebPartControlsViewComponent : WViewComponent
    {
        public WebPartControlsViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int webPartId = 0)
        {
            var resolvedPartId = ResolvePartId(webPartId);
            var model = new WebPartControlsViewModel
            {
                WebPartId = resolvedPartId,
                Controls = new List<WebPartControlItem>()
            };

            try
            {
                var part = resolvedPartId > 0 ? WPart.Get(resolvedPartId) : null;
                model.WebPartName = part?.Name;

                var controls = resolvedPartId > 0
                    ? WebPartControl.GetList(resolvedPartId)?.OrderBy(i => i.Name).ToList() ?? new List<WebPartControl>()
                    : new List<WebPartControl>();

                model.Controls = controls.Select((ctrl, index) => new WebPartControlItem
                    {
                        Id = ctrl.Id,
                        Name = ctrl.Name,
                        ControlType = ctrl.Identity,
                        IsActive = true,
                        SortOrder = index + 1,
                        ModifiedDate = null
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Failed to load web part controls: {ex.Message}";
            }

            return View(model);
        }

        private int ResolvePartId(int webPartId)
        {
            if (webPartId > 0)
                return webPartId;

            return DataUtil.GetId(Request, WebColumns.PartId);
        }
    }

    public class WebPartControlsViewModel
    {
        public int WebPartId { get; set; }
        public string WebPartName { get; set; }
        public List<WebPartControlItem> Controls { get; set; } = new List<WebPartControlItem>();
        public string ErrorMessage { get; set; }
    }

    public class WebPartControlItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ControlType { get; set; }
        public bool IsActive { get; set; }
        public int SortOrder { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
