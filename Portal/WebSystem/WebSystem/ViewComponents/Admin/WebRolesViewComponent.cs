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
    /// Role management list. Replaces WebRoles.ascx (Central/Security).
    /// Usage: @await Component.InvokeAsync("WebRoles")
    /// </summary>
    public class WebRolesViewComponent : WViewComponent
    {
        public WebRolesViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke()
        {
            var model = new WebRolesViewModel
            {
                SelectedRoleId = DataUtil.GetId(Request, WebColumns.RoleId),
                Roles = new List<WebRoleItem>()
            };

            try
            {
                model.Roles = WebRole.GetList()
                    .OrderBy(role => role.Name)
                    .Select(role => new WebRoleItem
                    {
                        Id = role.Id,
                        Name = role.Name,
                        Description = string.Empty,
                        IsSystem = false,
                        MemberCount = 0,
                        Permissions = new List<string>()
                    })
                    .ToList();

                if (model.SelectedRoleId < 1 && model.Roles.Count > 0)
                    model.SelectedRoleId = model.Roles[0].Id;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Failed to load roles: {ex.Message}";
            }

            return View(model);
        }
    }

    public class WebRolesViewModel
    {
        public List<WebRoleItem> Roles { get; set; } = new List<WebRoleItem>();
        public int SelectedRoleId { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class WebRoleItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsSystem { get; set; }
        public int MemberCount { get; set; }
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
