using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using System.Threading.Tasks;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    public class ThemeArea51UserProfileDetailsViewComponent : WViewComponent
    {
        public ThemeArea51UserProfileDetailsViewComponent(IWContext context) : base(context) { }

        public async Task<IViewComponentResult> InvokeAsync(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new ThemeArea51UserProfileDetailsModel
            {
                ObjectId = WcmsContext.ObjectId
            };

            return await Task.FromResult(View(model));
        }
    }

    public class ThemeArea51UserProfileDetailsModel
    {
        public int ObjectId { get; set; }
    }
}
