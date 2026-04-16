using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from fullcontent.ascx (AppBundle2/Media).
    /// </summary>
    public class MediaFullcontentViewComponent : WViewComponent
    {
        public MediaFullcontentViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new MediaFullcontentViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/AppBundle2/Media/fullcontent/MediaFullcontent/Default.cshtml", model);
        }
    }

        public class MediaFullcontentViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string LContentHtml { get; set; } = string.Empty;
    }
}
