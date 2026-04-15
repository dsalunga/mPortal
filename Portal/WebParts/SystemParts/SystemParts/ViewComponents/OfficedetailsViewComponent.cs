using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from OfficeDetails.ascx (SystemParts/Office).
    /// </summary>
    public class OfficedetailsViewComponent : WViewComponent
    {
        public OfficedetailsViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new OfficedetailsViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class OfficedetailsViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string lblEmailAddress { get; set; } = string.Empty;
        public string txtAddressLine1 { get; set; } = string.Empty;
        public string txtPhoneNumber { get; set; } = string.Empty;
        public string txtMobileNumber { get; set; } = string.Empty;
        public string txtContactPerson { get; set; } = string.Empty;
        public string lblStatus { get; set; } = string.Empty;
    }
}
