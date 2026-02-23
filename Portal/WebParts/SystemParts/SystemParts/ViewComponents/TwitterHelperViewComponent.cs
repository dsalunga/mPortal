using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Twitter feed/embed. Replaces TwitterHelper.ascx (SystemParts).
    /// Usage: @await Component.InvokeAsync("TwitterHelper", new { objectId, recordId })
    /// </summary>
    public class TwitterHelperViewComponent : WViewComponent
    {
        public TwitterHelperViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new TwitterHelperViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class TwitterHelperViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string TwitterHandle { get; set; }
        public string WidgetId { get; set; }
        public int TweetCount { get; set; }
        public string Theme { get; set; }
    }
}
