using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using WCMS.WebSystem.Apps.Integration.Net;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Shows list of available lessons with dates and language selector.
    /// Usage: @await Component.InvokeAsync("LessonList", new { objectId, recordId })
    /// </summary>
    public class LessonListViewComponent : WViewComponent
    {
        public LessonListViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new LessonListViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                BasePath = WcmsContext.BasePath,
                Lessons = new List<LessonItemModel>(),
                Languages = PlaybackLanguages.Values,
                SelectedLanguage = PlaybackLanguages.Neutral
            };

            return View(model);
        }
    }

    public class LessonListViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string BasePath { get; set; }
        public List<LessonItemModel> Lessons { get; set; }
        public Dictionary<string, string> Languages { get; set; }
        public string SelectedLanguage { get; set; }
    }

    public class LessonItemModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ServiceType { get; set; }
        public DateTime Date { get; set; }
        public string Language { get; set; }
        public bool HasFiles { get; set; }
    }
}
