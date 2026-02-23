using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// New user creation form. Replaces CreateUser.ascx (Central/Security).
    /// Usage: @await Component.InvokeAsync("CreateUser")
    /// </summary>
    public class CreateUserViewComponent : WViewComponent
    {
        public CreateUserViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke()
        {
            var model = new CreateUserViewModel
            {
                AvailableGroups = new List<CreateUserGroupItem>(),
                AvailableRoles = new List<CreateUserRoleItem>()
            };

            return View(model);
        }
    }

    public class CreateUserViewModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int SelectedGroupId { get; set; }
        public List<int> SelectedRoleIds { get; set; } = new List<int>();
        public List<CreateUserGroupItem> AvailableGroups { get; set; } = new List<CreateUserGroupItem>();
        public List<CreateUserRoleItem> AvailableRoles { get; set; } = new List<CreateUserRoleItem>();
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class CreateUserGroupItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateUserRoleItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
