using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from content_aditems_03.ascx (AppBundle2/Ads).
    /// </summary>
    public class AdsContentAditems03ViewComponent : WViewComponent
    {
        public AdsContentAditems03ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdsContentAditems03ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class AdsContentAditems03ViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<AdsContentAditems03Item> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class AdsContentAditems03Item
    {
        public string AdItemID { get; set; } = string.Empty;
        public string AlternateText { get; set; } = string.Empty;
        public string Appearance { get; set; } = string.Empty;
        public string Edit { get; set; } = string.Empty;
        public string Hits { get; set; } = string.Empty;
        public int Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Impressions { get; set; } = string.Empty;
        public string Keyword { get; set; } = string.Empty;
        public string NavigateUrl { get; set; } = string.Empty;
    }
}
