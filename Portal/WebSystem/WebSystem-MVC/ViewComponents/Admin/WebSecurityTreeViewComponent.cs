using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Security navigation tree. Replaces WebSecurityTree.ascx (Central/Security).
    /// Usage: @await Component.InvokeAsync("WebSecurityTree")
    /// </summary>
    public class WebSecurityTreeViewComponent : WViewComponent
    {
        public WebSecurityTreeViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke()
        {
            var model = new WebSecurityTreeViewModel
            {
                Nodes = new List<SecurityTreeNode>()
            };

            return View(model);
        }
    }

    public class WebSecurityTreeViewModel
    {
        public List<SecurityTreeNode> Nodes { get; set; } = new List<SecurityTreeNode>();
        public string SelectedNodeId { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class SecurityTreeNode
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public string NodeType { get; set; }
        public bool IsExpanded { get; set; }
        public List<SecurityTreeNode> Children { get; set; } = new List<SecurityTreeNode>();
    }
}
