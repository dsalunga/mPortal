using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using System.Threading.Tasks;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    public class MediaAdminViewComponent : WViewComponent
    {
        public MediaAdminViewComponent(IWContext context) : base(context) { }

        public async Task<IViewComponentResult> InvokeAsync(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new MediaAdminViewModel
            {
                ObjectId = WcmsContext.ObjectId,
                Items = new List<MediaAdminItem>()
            };

            return await Task.FromResult(View(model));
        }
    }

    public class MediaAdminViewModel
    {
        public int ObjectId { get; set; }
        public List<MediaAdminItem> Items { get; set; }
    }

    public class MediaAdminItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MediaUrl { get; set; }
        public string MediaType { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public bool IsActive { get; set; }
    }
}
