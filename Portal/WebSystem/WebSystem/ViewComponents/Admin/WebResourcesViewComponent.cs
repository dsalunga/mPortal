using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Resource file manager. Replaces WebResources.ascx (Central/Misc).
    /// Usage: @await Component.InvokeAsync("WebResources")
    /// </summary>
    public class WebResourcesViewComponent : WViewComponent
    {
        public WebResourcesViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(string selectedFolder = "")
        {
            var model = new WebResourcesViewModel
            {
                SelectedFolder = selectedFolder,
                Folders = new List<ResourceFolder>(),
                Files = new List<ResourceFileItem>()
            };

            return View(model);
        }
    }

    public class WebResourcesViewModel
    {
        public string SelectedFolder { get; set; }
        public List<ResourceFolder> Folders { get; set; } = new List<ResourceFolder>();
        public List<ResourceFileItem> Files { get; set; } = new List<ResourceFileItem>();
        public string ErrorMessage { get; set; }
    }

    public class ResourceFolder
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public int FileCount { get; set; }
    }

    public class ResourceFileItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public string FileType { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
