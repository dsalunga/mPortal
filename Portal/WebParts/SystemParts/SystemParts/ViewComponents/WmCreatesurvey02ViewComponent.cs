using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from WM_CreateSurvey_02.ascx (SystemParts/GenericList).
    /// </summary>
    public class WmCreatesurvey02ViewComponent : WViewComponent
    {
        public WmCreatesurvey02ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new WmCreatesurvey02ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class WmCreatesurvey02ViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public bool Active { get; set; } = false;
        public bool PageTitle { get; set; } = false;
        public string StatusMessage { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
