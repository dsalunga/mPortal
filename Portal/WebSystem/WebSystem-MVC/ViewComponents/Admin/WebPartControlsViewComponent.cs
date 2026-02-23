using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
            var model = new WebPartControlsViewModel
            {
                WebPartId = webPartId,
                Controls = new List<WebPartControlItem>()
            };
            return View(model);
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
