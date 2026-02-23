using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Email composition form. Replaces SendEmail.ascx (SystemParts).
    /// Usage: @await Component.InvokeAsync("SendEmail", new { objectId, recordId })
    /// </summary>
    public class SendEmailViewComponent : WViewComponent
    {
        public SendEmailViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new SendEmailViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                IsLoggedIn = WcmsSession.IsLoggedIn,
                SenderName = WcmsSession.User?.FullName
            };

            return View(model);
        }
    }

    public class SendEmailViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public bool IsLoggedIn { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsSent { get; set; }
    }
}
