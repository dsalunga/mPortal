using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// File upload form. Replaces UploadFiles.ascx (SystemParts/FileManager).
    /// Usage: @await Component.InvokeAsync("UploadFiles", new { objectId, recordId })
    /// </summary>
    public class UploadFilesViewComponent : WViewComponent
    {
        public UploadFilesViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new UploadFilesViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                IsLoggedIn = WcmsSession.IsLoggedIn,
                MaxFileSizeMb = 10,
                AllowedExtensions = string.Empty
            };

            return View("~/Views/Shared/Components/FileManager/UploadFiles/Default.cshtml", model);
        }
    }

    public class UploadFilesViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public bool IsLoggedIn { get; set; }
        public int MaxFileSizeMb { get; set; }
        public string AllowedExtensions { get; set; }
        public string UploadPath { get; set; }
        public bool IsUploaded { get; set; }
        public string Message { get; set; }
    }
}
