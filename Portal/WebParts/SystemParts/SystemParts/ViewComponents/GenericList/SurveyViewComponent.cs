using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Survey form with questions. Replaces Survey.ascx (SystemParts/GenericList).
    /// Usage: @await Component.InvokeAsync("Survey", new { objectId, recordId })
    /// </summary>
    public class SurveyViewComponent : WViewComponent
    {
        public SurveyViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new SurveyViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                Questions = new List<SurveyQuestionModel>(),
                BasePath = WcmsContext.BasePath,
                CurrentPage = 1,
                TotalPages = 1,
                ShowBackButton = false,
                ShowRestartButton = false,
                SubmitButtonText = "Submit"
            };

            return View("~/Views/Shared/Components/GenericList/Survey/Default.cshtml", model);
        }
    }

    public class SurveyViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int SurveyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string BasePath { get; set; }
        public bool IsSubmitted { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string PageLabel { get; set; }
        public bool ShowBackButton { get; set; }
        public bool ShowRestartButton { get; set; }
        public string ErrorMessage { get; set; }
        public string SubmitButtonText { get; set; }
        public List<SurveyQuestionModel> Questions { get; set; }
    }

    public class SurveyQuestionModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string QuestionType { get; set; }
        public bool IsRequired { get; set; }
        public int SortOrder { get; set; }
        public List<SurveyOptionModel> Options { get; set; }
    }

    public class SurveyOptionModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int SortOrder { get; set; }
    }
}
