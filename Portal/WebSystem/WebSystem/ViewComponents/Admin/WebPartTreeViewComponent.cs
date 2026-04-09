using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Web part hierarchy tree view. Replaces WebPartTree.ascx (Central/WebPart).
    /// Usage: @await Component.InvokeAsync("WebPartTree")
    /// </summary>
    public class WebPartTreeViewComponent : WViewComponent
    {
        public WebPartTreeViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int selectedNodeId = 0)
        {
            var model = new WebPartTreeViewModel
            {
                SelectedNodeId = selectedNodeId,
                RootNodes = new List<WebPartTreeNode>()
            };
            return View(model);
        }
    }

    public class WebPartTreeViewModel
    {
        public int SelectedNodeId { get; set; }
        public List<WebPartTreeNode> RootNodes { get; set; } = new List<WebPartTreeNode>();
        public string ErrorMessage { get; set; }
    }

    public class WebPartTreeNode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NodeType { get; set; }
        public int ParentId { get; set; }
        public bool IsExpanded { get; set; }
        public bool IsActive { get; set; }
        public List<WebPartTreeNode> Children { get; set; } = new List<WebPartTreeNode>();
    }
}
