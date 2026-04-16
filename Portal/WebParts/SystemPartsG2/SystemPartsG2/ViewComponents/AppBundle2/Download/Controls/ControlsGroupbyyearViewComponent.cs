using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from GroupByYear.ascx (AppBundle2/Download/Controls).
    /// </summary>
    public class ControlsGroupbyyearViewComponent : WViewComponent
    {
        public ControlsGroupbyyearViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ControlsGroupbyyearViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/AppBundle2/Download/Controls/GroupByYear/ControlsGroupbyyear/Default.cshtml", model);
        }
    }

        public class ControlsGroupbyyearViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<ControlsGroupbyyearItem> Items { get; set; } = new();
    }

    public class ControlsGroupbyyearItem
    {
        public string FileYear { get; set; } = string.Empty;
    }
}
