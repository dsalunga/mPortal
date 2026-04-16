using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from BodyEvent.ascx (AppBundle2/Misc/Script).
    /// </summary>
    public class ScriptBodyeventViewComponent : WViewComponent
    {
        public ScriptBodyeventViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ScriptBodyeventViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/AppBundle2/Misc/Script/BodyEvent/ScriptBodyevent/Default.cshtml", model);
        }
    }

    public class ScriptBodyeventViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
    }
}
