using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    public class ThemeTestStandaloneViewComponent : WViewComponent
    {
        public ThemeTestStandaloneViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new ThemeTestStandaloneModel
            {
                ObjectId = WcmsContext.ObjectId
            };

            return View(model);
        }
    }

    public class ThemeTestStandaloneModel
    {
        public int ObjectId { get; set; }
    }
}
