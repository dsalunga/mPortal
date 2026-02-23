using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Theme list manager. Replaces WebThemes.ascx (Central/Template).
    /// Usage: @await Component.InvokeAsync("WebThemes")
    /// </summary>
    public class WebThemesViewComponent : WViewComponent
    {
        public WebThemesViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke()
        {
            var model = new WebThemesViewModel
            {
                Themes = new List<WebThemeItem>()
            };

            return View(model);
        }
    }

    public class WebThemesViewModel
    {
        public List<WebThemeItem> Themes { get; set; } = new List<WebThemeItem>();
        public int SelectedThemeId { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class WebThemeItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PreviewUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
