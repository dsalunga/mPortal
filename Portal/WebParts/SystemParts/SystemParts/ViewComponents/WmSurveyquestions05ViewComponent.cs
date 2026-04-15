using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from WM_SurveyQuestions_05.ascx (SystemParts/GenericList).
    /// </summary>
    public class WmSurveyquestions05ViewComponent : WViewComponent
    {
        public WmSurveyquestions05ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new WmSurveyquestions05ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class WmSurveyquestions05ViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<WmSurveyquestions05Item> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class WmSurveyquestions05Item
    {
        public string Choices { get; set; } = string.Empty;
        public string Edit { get; set; } = string.Empty;
        public string Horizontal { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public string Rank { get; set; } = string.Empty;
        public string Required { get; set; } = string.Empty;
    }
}
