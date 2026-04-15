using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from RenameFile.ascx (SystemParts/FileManager).
    /// </summary>
    public class RenamefileViewComponent : WViewComponent
    {
        public RenamefileViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new RenamefileViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class RenamefileViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string NewName { get; set; } = string.Empty;
        public string OldName { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
}
