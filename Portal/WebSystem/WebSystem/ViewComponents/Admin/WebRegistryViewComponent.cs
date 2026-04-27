using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
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
            var selectedPath = Request.Query["Path"].ToString();
            var selectedRegistryId = DataUtil.GetId(Request, WebColumns.RegistryId);
            var model = new WebRegistryViewModel
            {
                SelectedPath = selectedPath,
                Nodes = new List<RegistryNode>(),
                Entries = new List<RegistryEntry>()
            };

            try
            {
                var allNodes = WebRegistry.GetList()?.ToList() ?? new List<WebRegistry>();
                model.Nodes = BuildTree(allNodes, -1);

                if (string.IsNullOrWhiteSpace(model.SelectedPath))
                {
                    if (selectedRegistryId > 0)
                        model.SelectedPath = BuildPath(allNodes, selectedRegistryId);
                    else if (model.Nodes.Count > 0)
                        model.SelectedPath = model.Nodes[0].Path;
                }

                var selectedNode = FindNodeByPath(allNodes, model.SelectedPath);
                if (selectedNode == null && selectedRegistryId > 0)
                    selectedNode = allNodes.FirstOrDefault(i => i.Id == selectedRegistryId);

                if (selectedNode != null)
                {
                    var selectedChildren = allNodes.Where(i => i.ParentId == selectedNode.Id).OrderBy(i => i.Key).ToList();
                    model.Entries = selectedChildren.Select(child => new RegistryEntry
                        {
                            Id = child.Id,
                            Key = child.Key,
                            Value = child.Value,
                            DataType = "String"
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Failed to load registry: {ex.Message}";
            }

            return View(model);
        }

        private static List<RegistryNode> BuildTree(List<WebRegistry> allNodes, int parentId)
        {
            return allNodes
                .Where(i => i.ParentId == parentId)
                .OrderBy(i => i.Key)
                .Select(i => new RegistryNode
                {
                    Id = i.Id,
                    Name = i.Key,
                    Path = BuildPath(allNodes, i.Id),
                    ParentId = i.ParentId,
                    Children = BuildTree(allNodes, i.Id)
                })
                .ToList();
        }

        private static string BuildPath(List<WebRegistry> allNodes, int nodeId)
        {
            var map = allNodes.ToDictionary(i => i.Id, i => i);
            var names = new Stack<string>();
            var currentId = nodeId;
            while (currentId > 0 && map.TryGetValue(currentId, out var node))
            {
                if (!string.IsNullOrEmpty(node.Key))
                    names.Push(node.Key);
                currentId = node.ParentId;
            }

            return "/" + string.Join("/", names);
        }

        private static WebRegistry FindNodeByPath(List<WebRegistry> allNodes, string selectedPath)
        {
            if (string.IsNullOrWhiteSpace(selectedPath))
                return null;

            var parts = selectedPath.Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
                return null;

            var parentId = -1;
            WebRegistry found = null;
            foreach (var part in parts)
            {
                found = allNodes.FirstOrDefault(i =>
                    i.ParentId == parentId &&
                    i.Key.Equals(part, StringComparison.OrdinalIgnoreCase));
                if (found == null)
                    return null;

                parentId = found.Id;
            }

            return found;
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
