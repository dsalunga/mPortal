using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from asopv2-division-content.ascx (Themes/ASOP).
    /// </summary>
    public class AsopAsopv2DivisionContentViewComponent : WViewComponent
    {
        public AsopAsopv2DivisionContentViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AsopAsopv2DivisionContentViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class AsopAsopv2DivisionContentViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
    }
}
