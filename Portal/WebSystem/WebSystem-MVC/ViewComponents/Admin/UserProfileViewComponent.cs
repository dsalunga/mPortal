using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// User profile editor. Replaces UserProfile.ascx (Central/Security).
    /// Usage: @await Component.InvokeAsync("UserProfile")
    /// </summary>
    public class UserProfileViewComponent : WViewComponent
    {
        public UserProfileViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int userId = 0)
        {
            var model = new UserProfileViewModel
            {
                UserId = userId,
                Groups = new List<string>(),
                Roles = new List<string>()
            };

            return View(model);
        }
    }

    public class UserProfileViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public bool IsLockedOut { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<string> Groups { get; set; } = new List<string>();
        public List<string> Roles { get; set; } = new List<string>();
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
    }
}
