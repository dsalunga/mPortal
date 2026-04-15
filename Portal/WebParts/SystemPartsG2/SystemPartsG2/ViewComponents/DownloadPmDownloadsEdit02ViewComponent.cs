using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from PM_Downloads_Edit_02.ascx (AppBundle2/Download).
    /// </summary>
    public class DownloadPmDownloadsEdit02ViewComponent : WViewComponent
    {
        public DownloadPmDownloadsEdit02ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new DownloadPmDownloadsEdit02ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class DownloadPmDownloadsEdit02ViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string Date { get; set; } = string.Empty;
        public string Filename { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Rank { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
}
