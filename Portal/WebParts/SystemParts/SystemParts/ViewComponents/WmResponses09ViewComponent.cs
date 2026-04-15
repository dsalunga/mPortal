using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from WM_Responses_09.ascx (SystemParts/GenericList).
    /// </summary>
    public class WmResponses09ViewComponent : WViewComponent
    {
        public WmResponses09ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new WmResponses09ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class WmResponses09ViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        // TODO: Add model properties based on legacy control analysis
    }
}
