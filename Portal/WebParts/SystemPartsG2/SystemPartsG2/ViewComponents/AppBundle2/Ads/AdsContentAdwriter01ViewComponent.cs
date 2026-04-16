using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from content_adwriter_01.ascx (AppBundle2/Ads).
    /// </summary>
    public class AdsContentAdwriter01ViewComponent : WViewComponent
    {
        public AdsContentAdwriter01ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdsContentAdwriter01ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/AppBundle2/Ads/content_adwriter_01/AdsContentAdwriter01/Default.cshtml", model);
        }
    }

        public class AdsContentAdwriter01ViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<AdsContentAdwriter01Item> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class AdsContentAdwriter01Item
    {
        public string AdCategoryName { get; set; } = string.Empty;
        public string AdID { get; set; } = string.Empty;
        public string AdvertisementFile { get; set; } = string.Empty;
        public string Appearance { get; set; } = string.Empty;
        public string Edit { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Hits { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Items { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
