using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from SM_Downloads_03.ascx (AppBundle2/Download).
    /// </summary>
    public class DownloadSmDownloads03ViewComponent : WViewComponent
    {
        public DownloadSmDownloads03ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new DownloadSmDownloads03ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/AppBundle2/Download/SM_Downloads_03/DownloadSmDownloads03/Default.cshtml", model);
        }
    }

        public class DownloadSmDownloads03ViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<DownloadSmDownloads03Item> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class DownloadSmDownloads03Item
    {
        public string Actions { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string DateModified { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Rank { get; set; } = string.Empty;
    }
}
