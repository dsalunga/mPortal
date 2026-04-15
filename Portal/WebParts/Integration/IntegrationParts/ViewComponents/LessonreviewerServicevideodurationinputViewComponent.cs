using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from ServiceVideoDurationInput.ascx (Apps/Integration/Profile/LessonReviewer).
    /// </summary>
    public class LessonreviewerServicevideodurationinputViewComponent : WViewComponent
    {
        public LessonreviewerServicevideodurationinputViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new LessonreviewerServicevideodurationinputViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class LessonreviewerServicevideodurationinputViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
    }
}
