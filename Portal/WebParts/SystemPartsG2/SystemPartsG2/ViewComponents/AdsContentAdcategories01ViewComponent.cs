using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from content_adcategories_01.ascx (AppBundle2/Ads).
    /// </summary>
    public class AdsContentAdcategories01ViewComponent : WViewComponent
    {
        public AdsContentAdcategories01ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdsContentAdcategories01ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class AdsContentAdcategories01ViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<AdsContentAdcategories01Item> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class AdsContentAdcategories01Item
    {
        public string AdCategoryID { get; set; } = string.Empty;
        public string Edit { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Items { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
