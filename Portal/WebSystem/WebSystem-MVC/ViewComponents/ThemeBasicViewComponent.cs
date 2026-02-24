using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    public class ThemeBasicViewComponent : WViewComponent
    {
        public ThemeBasicViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new ThemeBasicModel
            {
                ObjectId = WcmsContext.ObjectId
            };

            return View(model);
        }
    }

    public class ThemeBasicModel
    {
        public int ObjectId { get; set; }
    }
}
