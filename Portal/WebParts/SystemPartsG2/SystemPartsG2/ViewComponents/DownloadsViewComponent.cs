using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    public class DownloadsViewComponent : WViewComponent
    {
        public DownloadsViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new DownloadsViewModel
            {
                ObjectId = WcmsContext.ObjectId,
                Categories = new List<DownloadCategory>(),
                Items = new List<DownloadItem>()
            };

            return View(model);
        }
    }

    public class DownloadsViewModel
    {
        public int ObjectId { get; set; }
        public List<DownloadCategory> Categories { get; set; }
        public List<DownloadItem> Items { get; set; }
    }

    public class DownloadCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DownloadItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public long FileSize { get; set; }
        public int DownloadCount { get; set; }
        public string CategoryName { get; set; }
    }
}
