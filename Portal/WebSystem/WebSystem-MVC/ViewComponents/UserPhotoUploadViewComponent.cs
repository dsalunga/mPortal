using System;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Photo upload form for user profile images. Replaces UserPhotoUpload.ascx (Common).
    /// Usage: @await Component.InvokeAsync("UserPhotoUpload")
    /// </summary>
    public class UserPhotoUploadViewComponent : WViewComponent
    {
        public UserPhotoUploadViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke()
        {
            var model = new UserPhotoUploadViewModel
            {
                IsLoggedIn = WcmsSession.IsLoggedIn,
                UserId = WcmsSession.UserId,
                UserName = WcmsSession.User?.FullName
            };

            if (WcmsSession.User != null)
            {
                model.CurrentPhotoUrl = WcmsSession.User.GetPhotoPath();
            }

            return View(model);
        }
    }

    public class UserPhotoUploadViewModel
    {
        public bool IsLoggedIn { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string CurrentPhotoUrl { get; set; }
        public string StatusMessage { get; set; }
    }
}
