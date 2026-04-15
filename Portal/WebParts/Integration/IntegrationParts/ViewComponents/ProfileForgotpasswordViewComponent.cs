using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from ForgotPassword.ascx (Themes/area51/Parts/Profile/Profile).
    /// </summary>
    public class ProfileForgotpasswordViewComponent : WViewComponent
    {
        public ProfileForgotpasswordViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ProfileForgotpasswordViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class ProfileForgotpasswordViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string DateOfMembership { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ExternalId { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
}
