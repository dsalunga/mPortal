using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from AdminAlbumEdit.ascx (SystemParts/Photo).
    /// </summary>
    public class AdminalbumeditViewComponent : WViewComponent
    {
        public AdminalbumeditViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdminalbumeditViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class AdminalbumeditViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string FolderName { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;
        public string PhotoHeight { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Width { get; set; } = string.Empty;
    }
}
