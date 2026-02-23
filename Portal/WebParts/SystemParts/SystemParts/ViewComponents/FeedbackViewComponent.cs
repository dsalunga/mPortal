using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Content feedback / comments View Component. Replaces FeedBack.ascx (SystemParts/Content).
    /// Usage: @await Component.InvokeAsync("Feedback", new { objectId, recordId })
    /// </summary>
    public class FeedbackViewComponent : WViewComponent
    {
        public FeedbackViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            var model = new FeedbackViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                IsLoggedIn = WcmsSession.IsLoggedIn,
                UserName = WcmsSession.User?.FullName
            };

            return View(model);
        }
    }

    public class FeedbackViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public bool IsLoggedIn { get; set; }
        public string UserName { get; set; }
    }
}
