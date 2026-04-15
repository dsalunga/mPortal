using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from RemoteLibraryView.ascx (SystemParts/FileManager).
    /// </summary>
    public class RemotelibraryviewViewComponent : WViewComponent
    {
        public RemotelibraryviewViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new RemotelibraryviewViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class RemotelibraryviewViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<RemotelibraryviewItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class RemotelibraryviewItem
    {
        public string DateModified { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SizeString { get; set; } = string.Empty;
        public string TypeName { get; set; } = string.Empty;
    }
}
