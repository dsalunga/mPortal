using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Folder tree browser. Replaces FolderView.ascx (SystemParts/FileManager).
    /// Usage: @await Component.InvokeAsync("FolderView", new { objectId, recordId })
    /// </summary>
    public class FolderViewViewComponent : WViewComponent
    {
        public FolderViewViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new FolderViewViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                Folders = new List<FolderNodeModel>(),
                CurrentPath = string.Empty,
                BasePath = WcmsContext.BasePath
            };

            return View(model);
        }
    }

    public class FolderViewViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string CurrentPath { get; set; }
        public string BasePath { get; set; }
        public List<FolderNodeModel> Folders { get; set; }
    }

    public class FolderNodeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int FileCount { get; set; }
        public List<FolderNodeModel> Children { get; set; }
    }
}
