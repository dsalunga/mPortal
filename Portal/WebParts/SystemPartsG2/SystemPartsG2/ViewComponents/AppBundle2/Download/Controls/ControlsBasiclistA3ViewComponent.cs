using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from BasicList_A3.ascx (AppBundle2/Download/Controls).
    /// </summary>
    public class ControlsBasiclistA3ViewComponent : WViewComponent
    {
        public ControlsBasiclistA3ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ControlsBasiclistA3ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/AppBundle2/Download/Controls/BasicList_A3/ControlsBasiclistA3/Default.cshtml", model);
        }
    }

        public class ControlsBasiclistA3ViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<ControlsBasiclistA3Item> Items { get; set; } = new();
    }

    public class ControlsBasiclistA3Item
    {
        public string DownloadID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
