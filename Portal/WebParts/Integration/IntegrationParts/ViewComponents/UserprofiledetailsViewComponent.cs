using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from UserProfileDetails.ascx (Apps/Integration/Profile).
    /// </summary>
    public class UserprofiledetailsViewComponent : WViewComponent
    {
        public UserprofiledetailsViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new UserprofiledetailsViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class UserprofiledetailsViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string AdditionalInfo { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
}
