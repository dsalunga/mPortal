using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using System.Threading.Tasks;

namespace WCMS.WebSystem.ViewComponents
{
    public class ThemeDefaultForAjaxControlToolkitViewComponent : WViewComponent
    {
        public ThemeDefaultForAjaxControlToolkitViewComponent(IWContext context) : base(context) { }

        public async Task<IViewComponentResult> InvokeAsync(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new ThemeDefaultForAjaxControlToolkitModel
            {
                ObjectId = WcmsContext.ObjectId
            };

            return await Task.FromResult(View(model));
        }
    }

    public class ThemeDefaultForAjaxControlToolkitModel
    {
        public int ObjectId { get; set; }
    }
}
