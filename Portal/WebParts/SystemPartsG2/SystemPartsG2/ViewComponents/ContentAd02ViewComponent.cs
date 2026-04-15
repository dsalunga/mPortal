using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from content_ad_02.ascx (AppBundle2/Ads).
    /// </summary>
    public class ContentAd02ViewComponent : WViewComponent
    {
        public ContentAd02ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ContentAd02ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class ContentAd02ViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<SelectOption> CboCategoriesOptions { get; set; } = new();
        public string Name { get; set; } = string.Empty;
        public string SelectedCboCategories { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
    }
