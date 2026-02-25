using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using WCMS.LessonReviewer.Core;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Media player UI for lesson playback.
    /// Usage: @await Component.InvokeAsync("LessonPlayer", new { objectId, recordId })
    /// </summary>
    public class LessonPlayerViewComponent : WViewComponent
    {
        public LessonPlayerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new LessonPlayerViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                BasePath = WcmsContext.BasePath,
                Files = new List<PlaybackFileModel>(),
                Service = new ServiceDefinition()
            };

            return View(model);
        }
    }

    public class LessonPlayerViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string BasePath { get; set; }
        public string ServiceType { get; set; }
        public string ServiceDate { get; set; }
        public string Language { get; set; }
        public ServiceDefinition Service { get; set; }
        public List<PlaybackFileModel> Files { get; set; }
    }

    public class PlaybackFileModel
    {
        public string Filename { get; set; }
        public string Url { get; set; }
        public string Caption { get; set; }
        public int SequenceNo { get; set; }
        public string SegmentCode { get; set; }
    }
}
