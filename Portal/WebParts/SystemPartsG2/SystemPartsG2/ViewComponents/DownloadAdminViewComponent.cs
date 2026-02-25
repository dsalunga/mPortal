using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using System.Threading.Tasks;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    public class DownloadAdminViewComponent : WViewComponent
    {
        public DownloadAdminViewComponent(IWContext context) : base(context) { }

        public async Task<IViewComponentResult> InvokeAsync(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new DownloadAdminViewModel
            {
                ObjectId = WcmsContext.ObjectId,
                Downloads = new List<DownloadAdminItem>()
            };

            return await Task.FromResult(View(model));
        }
    }

    public class DownloadAdminViewModel
    {
        public int ObjectId { get; set; }
        public List<DownloadAdminItem> Downloads { get; set; }
    }

    public class DownloadAdminItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public long FileSize { get; set; }
        public int DownloadCount { get; set; }
        public bool IsActive { get; set; }
    }
}
