using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Office document browser. Replaces OfficeBrowser.ascx (SystemParts).
    /// Usage: @await Component.InvokeAsync("OfficeBrowser", new { objectId, recordId })
    /// </summary>
    public class OfficeBrowserViewComponent : WViewComponent
    {
        public OfficeBrowserViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new OfficeBrowserViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                Documents = new List<OfficeDocumentModel>(),
                BasePath = WcmsContext.BasePath
            };

            return View("~/Views/Shared/Components/OfficeBrowser/Default.cshtml", model);
        }
    }

    public class OfficeBrowserViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string BasePath { get; set; }
        public List<OfficeDocumentModel> Documents { get; set; }
    }

    public class OfficeDocumentModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public string LastModified { get; set; }
    }
}
