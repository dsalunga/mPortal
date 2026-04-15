using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from SurveyWelcome.ascx (SystemParts/GenericList).
    /// </summary>
    public class SurveywelcomeViewComponent : WViewComponent
    {
        public SurveywelcomeViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new SurveywelcomeViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class SurveywelcomeViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string lblTitle { get; set; } = string.Empty;
        public string lblMessage { get; set; } = string.Empty;
        public string lblStatus { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
