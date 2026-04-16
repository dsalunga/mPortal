using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from BasicList.ascx (AppBundle2/Download/Controls).
    /// </summary>
    public class ControlsBasiclistViewComponent : WViewComponent
    {
        public ControlsBasiclistViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ControlsBasiclistViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/AppBundle2/Download/Controls/BasicList/Default.cshtml", model);
        }
    }

        public class ControlsBasiclistViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<ControlsBasiclistItem> Items { get; set; } = new();
    }

    public class ControlsBasiclistItem
    {
        public string DownloadID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
