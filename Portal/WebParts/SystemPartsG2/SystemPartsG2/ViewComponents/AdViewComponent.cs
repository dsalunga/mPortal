using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    public class AdViewComponent : WViewComponent
    {
        public AdViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdViewModel
            {
                ObjectId = WcmsContext.ObjectId,
                RecordId = WcmsContext.RecordId,
                Title = string.Empty,
                ImageUrl = string.Empty,
                TargetUrl = string.Empty,
                AltText = string.Empty,
                IsActive = true
            };

            return View(model);
        }
    }

    public class AdViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string TargetUrl { get; set; }
        public string AltText { get; set; }
        public bool IsActive { get; set; }
    }
}
