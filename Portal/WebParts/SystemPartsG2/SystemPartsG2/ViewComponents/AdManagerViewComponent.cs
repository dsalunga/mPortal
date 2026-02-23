using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    public class AdManagerViewComponent : WViewComponent
    {
        public AdManagerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdManagerViewModel
            {
                ObjectId = WcmsContext.ObjectId,
                Ads = new List<AdManagerItem>()
            };

            return View(model);
        }
    }

    public class AdManagerViewModel
    {
        public int ObjectId { get; set; }
        public List<AdManagerItem> Ads { get; set; }
    }

    public class AdManagerItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string TargetUrl { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
    }
}
