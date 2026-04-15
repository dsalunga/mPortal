using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from AttendanceRequest.ascx (Apps/Integration/LessonReviewer).
    /// </summary>
    public class AttendancerequestViewComponent : WViewComponent
    {
        public AttendancerequestViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AttendancerequestViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class AttendancerequestViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public bool CblCoAttendees { get; set; } = false;
        public string Note { get; set; } = string.Empty;
        public bool Notes { get; set; } = false;
        public bool SendCopy { get; set; } = false;
        public string StatusMessage { get; set; } = string.Empty;
    }
}
