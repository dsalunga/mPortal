using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    public class SplashNavigatorViewComponent : WViewComponent
    {
        public SplashNavigatorViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new SplashNavigatorViewModel
            {
                ObjectId = WcmsContext.ObjectId,
                Items = new List<SplashNavigatorItem>()
            };

            return View(model);
        }
    }

    public class SplashNavigatorViewModel
    {
        public int ObjectId { get; set; }
        public List<SplashNavigatorItem> Items { get; set; }
    }

    public class SplashNavigatorItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
    }
}
