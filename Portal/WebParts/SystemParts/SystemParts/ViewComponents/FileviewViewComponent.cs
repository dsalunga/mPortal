using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from FileView.ascx (SystemParts/FileManager).
    /// </summary>
    public class FileviewViewComponent : WViewComponent
    {
        public FileviewViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new FileviewViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class FileviewViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string hFileId { get; set; } = string.Empty;
        public List<object> ObjectDataSourceFoldersData { get; set; } = new();
    }
}
