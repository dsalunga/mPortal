using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using System.Threading.Tasks;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    public class ListingPageViewComponent : WViewComponent
    {
        public ListingPageViewComponent(IWContext context) : base(context) { }

        public async Task<IViewComponentResult> InvokeAsync(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ListingPageViewModel
            {
                ObjectId = WcmsContext.ObjectId,
                Title = string.Empty,
                Items = new List<ListingPageItem>(),
                PageSize = 10,
                CurrentPage = 1,
                TotalItems = 0
            };

            return await Task.FromResult(View(model));
        }
    }

    public class ListingPageViewModel
    {
        public int ObjectId { get; set; }
        public string Title { get; set; }
        public List<ListingPageItem> Items { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
    }

    public class ListingPageItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Summary { get; set; }
        public string ImageUrl { get; set; }
        public string Date { get; set; }
    }
}
