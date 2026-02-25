using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using System.Threading.Tasks;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    public class AdManagerViewComponent : WViewComponent
    {
        public AdManagerViewComponent(IWContext context) : base(context) { }

        public async Task<IViewComponentResult> InvokeAsync(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdManagerViewModel
            {
                ObjectId = WcmsContext.ObjectId,
                Ads = new List<AdManagerItem>(),
                SelectedAds = new List<AdManagerItem>(),
                Categories = new List<AdCategory>(),
                Authors = new List<AdAuthor>()
            };

            return await Task.FromResult(View(model));
        }
    }

    public class AdManagerViewModel
    {
        public int ObjectId { get; set; }
        public List<AdManagerItem> Ads { get; set; }
        public List<AdManagerItem> SelectedAds { get; set; }
        public List<AdCategory> Categories { get; set; }
        public List<AdAuthor> Authors { get; set; }
        public string SelectedCategoryId { get; set; }
        public string SelectedAuthorId { get; set; }
    }

    public class AdManagerItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string TargetUrl { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
        public string Name { get; set; }
        public int Hits { get; set; }
        public string FirstName { get; set; }
        public string CategoryName { get; set; }
        public string AdvertisementFile { get; set; }
    }

    public class AdCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class AdAuthor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
    }
}
