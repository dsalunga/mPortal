using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// File browser with upload/download. Replaces FileManager.ascx (SystemParts/FileManager).
    /// Usage: @await Component.InvokeAsync("FileManager", new { objectId, recordId })
    /// </summary>
    public class FileManagerViewComponent : WViewComponent
    {
        public FileManagerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new FileManagerViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                Files = new List<FileItemModel>(),
                CurrentPath = string.Empty,
                BasePath = WcmsContext.BasePath,
                IsLoggedIn = WcmsSession.IsLoggedIn
            };

            return View(model);
        }
    }

    public class FileManagerViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string CurrentPath { get; set; }
        public string BasePath { get; set; }
        public bool IsLoggedIn { get; set; }
        public List<FileItemModel> Files { get; set; }
    }

    public class FileItemModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public string ContentType { get; set; }
        public string LastModified { get; set; }
        public bool IsDirectory { get; set; }
    }
}
