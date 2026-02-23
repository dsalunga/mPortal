using System;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Password change form. Replaces ChangePassword.ascx (Central/Security).
    /// Usage: @await Component.InvokeAsync("ChangePassword")
    /// </summary>
    public class ChangePasswordViewComponent : WViewComponent
    {
        public ChangePasswordViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int userId = 0)
        {
            var model = new ChangePasswordViewModel
            {
                UserId = userId,
                RequireCurrentPassword = userId == 0
            };

            return View(model);
        }
    }

    public class ChangePasswordViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool RequireCurrentPassword { get; set; }
        public int MinPasswordLength { get; set; } = 8;
        public bool RequireSpecialCharacter { get; set; } = true;
        public bool RequireDigit { get; set; } = true;
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
    }
}
