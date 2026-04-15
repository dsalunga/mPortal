using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

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

            return View(model);
        }
    }

    public class ControlsBasiclistA3ViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
    }
}
