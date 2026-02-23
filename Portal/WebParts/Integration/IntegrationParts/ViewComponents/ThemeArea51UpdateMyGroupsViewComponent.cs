using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    public class ThemeArea51UpdateMyGroupsViewComponent : WViewComponent
    {
        public ThemeArea51UpdateMyGroupsViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new ThemeArea51UpdateMyGroupsModel
            {
                ObjectId = WcmsContext.ObjectId
            };

            return View(model);
        }
    }

    public class ThemeArea51UpdateMyGroupsModel
    {
        public int ObjectId { get; set; }
    }
}
