using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Survey results display. Replaces SurveyResults.ascx (SystemParts/GenericList).
    /// Usage: @await Component.InvokeAsync("SurveyResults", new { objectId, recordId })
    /// </summary>
    public class SurveyResultsViewComponent : WViewComponent
    {
        public SurveyResultsViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new SurveyResultsViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                Questions = new List<SurveyResultQuestionModel>(),
                TotalResponses = 0
            };

            return View("~/Views/Shared/Components/GenericList/SurveyResults/Default.cshtml", model);
        }
    }

    public class SurveyResultsViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string Title { get; set; }
        public int TotalResponses { get; set; }
        public List<SurveyResultQuestionModel> Questions { get; set; }
    }

    public class SurveyResultQuestionModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<SurveyResultOptionModel> Options { get; set; }
    }

    public class SurveyResultOptionModel
    {
        public string Text { get; set; }
        public int Count { get; set; }
        public double Percentage { get; set; }
    }
}
