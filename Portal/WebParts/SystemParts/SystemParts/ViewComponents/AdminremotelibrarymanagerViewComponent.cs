using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from AdminRemoteLibraryManager.ascx (SystemParts/FileManager).
    /// </summary>
    public class AdminremotelibrarymanagerViewComponent : WViewComponent
    {
        public AdminremotelibrarymanagerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdminremotelibrarymanagerViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class AdminremotelibrarymanagerViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<AdminremotelibrarymanagerItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class AdminremotelibrarymanagerItem
    {
        public string Actions { get; set; } = string.Empty;
        public string Active { get; set; } = string.Empty;
        public string BaseAddress { get; set; } = string.Empty;
        public string DisplayBaseAddress { get; set; } = string.Empty;
        public int Id { get; set; }
        public string LastIndexDate { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string SourceType { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
