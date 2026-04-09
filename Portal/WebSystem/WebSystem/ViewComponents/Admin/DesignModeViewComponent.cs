using System;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Toggle design/preview mode. Replaces DesignMode.ascx (Central/Controls).
    /// Usage: @await Component.InvokeAsync("DesignMode")
    /// </summary>
    public class DesignModeViewComponent : WViewComponent
    {
        public DesignModeViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke()
        {
            var model = new DesignModeViewModel
            {
                IsDesignMode = false,
                CurrentPageId = 0,
                CurrentPageName = string.Empty
            };

            return View(model);
        }
    }

    public class DesignModeViewModel
    {
        public bool IsDesignMode { get; set; }
        public int CurrentPageId { get; set; }
        public string CurrentPageName { get; set; }
        public string PreviewUrl { get; set; }
        public bool CanEdit { get; set; }
    }
}
