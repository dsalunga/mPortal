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
                SelectedNodeId = ResolveSelectedNodeId(),
                Nodes = new List<SecurityTreeNode>()
            };

            try
            {
                model.Nodes = new List<SecurityTreeNode>
                {
                    new SecurityTreeNode
                    {
                        Id = "users",
                        Text = "Users",
                        Icon = "bi-people",
                        Url = CentralPages.WebUsers,
                        NodeType = "section",
                        IsExpanded = true,
                        Children = BuildUserNodes()
                    },
                    new SecurityTreeNode
                    {
                        Id = "groups",
                        Text = "Groups",
                        Icon = "bi-collection",
                        Url = CentralPages.WebGroups,
                        NodeType = "section",
                        IsExpanded = true,
                        Children = BuildGroupNodes()
                    },
                    new SecurityTreeNode
                    {
                        Id = "roles",
                        Text = "Roles",
                        Icon = "bi-shield-lock",
                        Url = CentralPages.WebRoles,
                        NodeType = "section",
                        IsExpanded = true,
                        Children = BuildRoleNodes()
                    },
                    new SecurityTreeNode
                    {
                        Id = "permissions",
                        Text = "Permissions",
                        Icon = "bi-key",
                        Url = CentralPages.WebPermissions,
                        NodeType = "section",
                        IsExpanded = false,
                        Children = new List<SecurityTreeNode>()
                    }
                };
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Failed to load security tree: {ex.Message}";
            }

            return View(model);
        }

        private string ResolveSelectedNodeId()
        {
            if (DataUtil.GetId(Request, WebColumns.UserId) > 0)
                return $"user:{DataUtil.GetId(Request, WebColumns.UserId)}";
            if (DataUtil.GetId(Request, WebColumns.GroupId) > 0)
                return $"group:{DataUtil.GetId(Request, WebColumns.GroupId)}";
            if (DataUtil.GetId(Request, WebColumns.RoleId) > 0)
                return $"role:{DataUtil.GetId(Request, WebColumns.RoleId)}";

            return Request.Query["NodeId"];
        }

        private static List<SecurityTreeNode> BuildUserNodes()
        {
            return WebUser.GetList()
                .OrderBy(i => i.UserName)
                .Take(25)
                .Select(user => new SecurityTreeNode
                {
                    Id = $"user:{user.Id}",
                    Text = user.UserName,
                    Icon = "bi-person",
                    Url = $"{CentralPages.WebUsers}?{WebColumns.UserId}={user.Id}",
                    NodeType = "user",
                    IsExpanded = false
                })
                .ToList();
        }

        private static List<SecurityTreeNode> BuildGroupNodes()
        {
            return WebGroup.GetList()
                .OrderBy(i => i.Name)
                .Take(40)
                .Select(group => new SecurityTreeNode
                {
                    Id = $"group:{group.Id}",
                    Text = group.Name,
                    Icon = "bi-folder",
                    Url = $"{CentralPages.WebGroups}?{WebColumns.GroupId}={group.Id}",
                    NodeType = "group",
                    IsExpanded = false
                })
                .ToList();
        }

        private static List<SecurityTreeNode> BuildRoleNodes()
        {
            return WebRole.GetList()
                .OrderBy(i => i.Name)
                .Select(role => new SecurityTreeNode
                {
                    Id = $"role:{role.Id}",
                    Text = role.Name,
                    Icon = "bi-shield",
                    Url = $"{CentralPages.WebRoles}?{WebColumns.RoleId}={role.Id}",
                    NodeType = "role",
                    IsExpanded = false
                })
                .ToList();
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
