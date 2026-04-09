using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Master page manager. Replaces WebMasterPages.ascx (Central/WebSites).
    /// Usage: @await Component.InvokeAsync("WebMasterPages")
    /// </summary>
    public class WebMasterPagesViewComponent : WViewComponent
    {
        public WebMasterPagesViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int selectedMasterPageId = 0)
        {
            var model = new WebMasterPagesViewModel
            {
                SelectedMasterPageId = selectedMasterPageId,
                MasterPages = new List<MasterPageItem>()
            };

            return View(model);
        }
    }

    public class WebMasterPagesViewModel
    {
        public int SelectedMasterPageId { get; set; }
        public List<MasterPageItem> MasterPages { get; set; } = new List<MasterPageItem>();
        public string ErrorMessage { get; set; }
    }

    public class MasterPageItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
