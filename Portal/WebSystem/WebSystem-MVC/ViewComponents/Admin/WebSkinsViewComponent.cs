using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Skin list manager. Replaces WebSkins.ascx (Central/Template).
    /// Usage: @await Component.InvokeAsync("WebSkins")
    /// </summary>
    public class WebSkinsViewComponent : WViewComponent
    {
        public WebSkinsViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke()
        {
            var model = new WebSkinsViewModel
            {
                Skins = new List<WebSkinItem>()
            };

            return View(model);
        }
    }

    public class WebSkinsViewModel
    {
        public List<WebSkinItem> Skins { get; set; } = new List<WebSkinItem>();
        public int SelectedSkinId { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class WebSkinItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CssFile { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
