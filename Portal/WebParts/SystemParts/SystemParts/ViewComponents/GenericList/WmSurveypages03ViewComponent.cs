using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from WM_SurveyPages_03.ascx (SystemParts/GenericList).
    /// </summary>
    public class WmSurveypages03ViewComponent : WViewComponent
    {
        public WmSurveypages03ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new WmSurveypages03ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/GenericList/WM_SurveyPages_03/Default.cshtml", model);
        }
    }

        public class WmSurveypages03ViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<WmSurveypages03Item> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class WmSurveypages03Item
    {
        public string Edit { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Questions { get; set; } = string.Empty;
        public string Rank { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
