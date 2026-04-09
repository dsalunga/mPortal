using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Object-level permission editor. Replaces WebPermissions.ascx (Central/Security).
    /// Usage: @await Component.InvokeAsync("WebPermissions")
    /// </summary>
    public class WebPermissionsViewComponent : WViewComponent
    {
        public WebPermissionsViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0)
        {
            var model = new WebPermissionsViewModel
            {
                ObjectId = objectId,
                Entries = new List<PermissionEntry>(),
                AvailableRoles = new List<PermissionRoleItem>()
            };

            return View(model);
        }
    }

    public class WebPermissionsViewModel
    {
        public int ObjectId { get; set; }
        public string ObjectName { get; set; }
        public string ObjectType { get; set; }
        public List<PermissionEntry> Entries { get; set; } = new List<PermissionEntry>();
        public List<PermissionRoleItem> AvailableRoles { get; set; } = new List<PermissionRoleItem>();
        public string ErrorMessage { get; set; }
    }

    public class PermissionEntry
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }
        public bool CanDelete { get; set; }
        public bool CanAdmin { get; set; }
    }

    public class PermissionRoleItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
