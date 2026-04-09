using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Registry tree browser/editor. Replaces WebRegistry.ascx (Central/Tools).
    /// Usage: @await Component.InvokeAsync("WebRegistry")
    /// </summary>
    public class WebRegistryViewComponent : WViewComponent
    {
        public WebRegistryViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke()
        {
            var model = new WebRegistryViewModel
            {
                Nodes = new List<RegistryNode>(),
                Entries = new List<RegistryEntry>()
            };

            return View(model);
        }
    }

    public class WebRegistryViewModel
    {
        public List<RegistryNode> Nodes { get; set; } = new List<RegistryNode>();
        public List<RegistryEntry> Entries { get; set; } = new List<RegistryEntry>();
        public string SelectedPath { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class RegistryNode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int ParentId { get; set; }
        public List<RegistryNode> Children { get; set; } = new List<RegistryNode>();
    }

    public class RegistryEntry
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string DataType { get; set; }
    }
}
