using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from ChangePasswordV2.ascx (Apps/Integration/Account).
    /// </summary>
    public class Changepasswordv2ViewComponent : WViewComponent
    {
        public Changepasswordv2ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new Changepasswordv2ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class Changepasswordv2ViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int ActiveViewIndex { get; set; }
        public string StatusMessage { get; set; } = string.Empty;
    }
}
