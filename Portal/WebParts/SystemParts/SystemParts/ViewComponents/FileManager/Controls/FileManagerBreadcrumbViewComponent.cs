using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from Breadcrumb.ascx (SystemParts/FileManager/Controls).
    /// </summary>
    public class FileManagerBreadcrumbViewComponent : WViewComponent
    {
        public FileManagerBreadcrumbViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new FileManagerBreadcrumbViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/FileManager/Controls/Breadcrumb/Default.cshtml", model);
        }
    }

        public class FileManagerBreadcrumbViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<FileManagerBreadcrumbItem> Items { get; set; } = new();
    }

    public class FileManagerBreadcrumbItem
    {
        public string Name { get; set; } = string.Empty;
    }
}
