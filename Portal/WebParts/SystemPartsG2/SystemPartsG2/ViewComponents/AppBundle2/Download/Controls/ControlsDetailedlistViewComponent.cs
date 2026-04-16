using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from DetailedList.ascx (AppBundle2/Download/Controls).
    /// </summary>
    public class ControlsDetailedlistViewComponent : WViewComponent
    {
        public ControlsDetailedlistViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ControlsDetailedlistViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/AppBundle2/Download/Controls/DetailedList/ControlsDetailedlist/Default.cshtml", model);
        }
    }

        public class ControlsDetailedlistViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<ControlsDetailedlistItem> Items { get; set; } = new();
    }

    public class ControlsDetailedlistItem
    {
        public string DownloadID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
