using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Contact Us V2 form with reCAPTCHA support. Ported from ContactUsV2.ascx (SystemParts/Contact).
    /// Usage: @await Component.InvokeAsync("Contactusv2", new { objectId, recordId })
    /// </summary>
    public class Contactusv2ViewComponent : WViewComponent
    {
        public Contactusv2ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            var model = new Contactusv2FormModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                IsLoggedIn = WcmsSession.IsLoggedIn
            };

            if (WcmsSession.IsLoggedIn)
            {
                var user = WcmsSession.User;
                model.Name = user?.FullName;
                model.Email = user?.Email;
            }

            return View("~/Views/Shared/Components/Contact/ContactUsV2/Default.cshtml", model);
        }
    }

    public class Contactusv2FormModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public bool IsLoggedIn { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool Submitted { get; set; }
        public bool RequireCode { get; set; } = true;
        public string RecaptchaSiteKey { get; set; } = string.Empty;
    }
}
