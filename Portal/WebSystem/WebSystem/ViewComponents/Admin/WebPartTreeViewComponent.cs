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
                SelectedNodeId = ResolveSelectedNodeId(selectedNodeId),
                RootNodes = new List<WebPartTreeNode>()
            };

            try
            {
                var parts = WPart.GetList()?.OrderBy(i => i.Name).ToList() ?? new List<WPart>();
                model.RootNodes = parts.Select(part =>
                {
                    var controls = WebPartControl.GetList(part.Id)?.OrderBy(i => i.Name).ToList() ?? new List<WebPartControl>();
                    return new WebPartTreeNode
                    {
                        Id = part.Id,
                        Name = part.Name,
                        NodeType = "folder",
                        ParentId = -1,
                        IsExpanded = model.SelectedNodeId == part.Id || controls.Any(i => i.Id == model.SelectedNodeId),
                        IsActive = part.IsActive,
                        Children = controls.Select(ctrl => new WebPartTreeNode
                        {
                            Id = ctrl.Id,
                            Name = ctrl.Name,
                            NodeType = "control",
                            ParentId = part.Id,
                            IsExpanded = false,
                            IsActive = true
                        }).ToList()
                    };
                }).ToList();
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Failed to load web part tree: {ex.Message}";
            }

            return View(model);
        }

        private int ResolveSelectedNodeId(int selectedNodeId)
        {
            if (selectedNodeId > 0)
                return selectedNodeId;

            var partControlId = DataUtil.GetId(Request, WebColumns.PartControlId);
            if (partControlId > 0)
                return partControlId;

            return DataUtil.GetId(Request, WebColumns.PartId);
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
