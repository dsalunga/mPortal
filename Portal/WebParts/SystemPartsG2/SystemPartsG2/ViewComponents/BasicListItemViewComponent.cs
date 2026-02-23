using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    public class BasicListItemViewComponent : WViewComponent
    {
        public BasicListItemViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new BasicListItemViewModel
            {
                ObjectId = WcmsContext.ObjectId,
                RecordId = WcmsContext.RecordId,
                Title = string.Empty,
                Url = string.Empty,
                Description = string.Empty,
                ImageUrl = string.Empty
            };

            return View(model);
        }
    }

    public class BasicListItemViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
