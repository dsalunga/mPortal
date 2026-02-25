using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using System.Threading.Tasks;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    public class BasicListViewComponent : WViewComponent
    {
        public BasicListViewComponent(IWContext context) : base(context) { }

        public async Task<IViewComponentResult> InvokeAsync(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new BasicListViewModel
            {
                ObjectId = WcmsContext.ObjectId,
                Title = string.Empty,
                Items = new List<BasicListEntry>(),
                ShowBullets = true
            };

            return await Task.FromResult(View(model));
        }
    }

    public class BasicListViewModel
    {
        public int ObjectId { get; set; }
        public string Title { get; set; }
        public List<BasicListEntry> Items { get; set; }
        public bool ShowBullets { get; set; }
    }

    public class BasicListEntry
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
    }
}
