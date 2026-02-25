using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using System.Threading.Tasks;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    public class SiteListViewComponent : WViewComponent
    {
        public SiteListViewComponent(IWContext context) : base(context) { }

        public async Task<IViewComponentResult> InvokeAsync(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new SiteListViewModel
            {
                ObjectId = WcmsContext.ObjectId,
                Sites = new List<SiteListItem>()
            };

            return await Task.FromResult(View(model));
        }
    }

    public class SiteListViewModel
    {
        public int ObjectId { get; set; }
        public List<SiteListItem> Sites { get; set; }
    }

    public class SiteListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
