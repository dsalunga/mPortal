using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
                Roles = new List<WebRoleItem>()
            };

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
