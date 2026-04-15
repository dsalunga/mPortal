using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from WM_CreateSurveyPage_04.ascx (SystemParts/GenericList).
    /// </summary>
    public class WmCreatesurveypage04ViewComponent : WViewComponent
    {
        public WmCreatesurveypage04ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new WmCreatesurveypage04ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class WmCreatesurveypage04ViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string txtTitle { get; set; } = string.Empty;
        public string txtRank { get; set; } = string.Empty;
    }
}
