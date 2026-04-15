using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from LogAttendance.ascx (Apps/Integration/Profile/LessonReviewer).
    /// </summary>
    public class LessonreviewerLogattendanceViewComponent : WViewComponent
    {
        public LessonreviewerLogattendanceViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new LessonreviewerLogattendanceViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class LessonreviewerLogattendanceViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string AbsentReason { get; set; } = string.Empty;
        public List<SelectOption> CboNotesOptions { get; set; } = new();
        public string CustomNote { get; set; } = string.Empty;
        public bool Note { get; set; } = false;
        public string SelectedCboNotes { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
    }
