using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Contact Us form View Component. Replaces ContactUs.ascx / ContactUsV2.ascx (SystemParts/Contact).
    /// Usage: @await Component.InvokeAsync("Contact", new { objectId, recordId })
    /// </summary>
    public class ContactViewComponent : WViewComponent
    {
        public ContactViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            var model = new ContactFormModel
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

            return View(model);
        }
    }

    public class ContactFormModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public bool IsLoggedIn { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool Submitted { get; set; }
    }
}
