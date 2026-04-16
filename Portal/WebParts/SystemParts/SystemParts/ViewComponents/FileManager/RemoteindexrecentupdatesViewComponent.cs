using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from RemoteIndexRecentUpdates.ascx (SystemParts/FileManager).
    /// </summary>
    public class RemoteindexrecentupdatesViewComponent : WViewComponent
    {
        public RemoteindexrecentupdatesViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new RemoteindexrecentupdatesViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/FileManager/RemoteIndexRecentUpdates/Default.cshtml", model);
        }
    }

        public class RemoteindexrecentupdatesViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<RemoteindexrecentupdatesItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class RemoteindexrecentupdatesItem
    {
        public string DateModified { get; set; } = string.Empty;
        public string DateModifiedString { get; set; } = string.Empty;
        public int Id { get; set; }
        public string RecentFiles { get; set; } = string.Empty;
        public string SizeString { get; set; } = string.Empty;
        public string TypeName { get; set; } = string.Empty;
    }
}
