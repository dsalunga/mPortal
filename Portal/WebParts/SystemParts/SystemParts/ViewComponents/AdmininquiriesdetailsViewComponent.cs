using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from AdminInquiriesDetails.ascx (SystemParts/Contact).
    /// </summary>
    public class AdmininquiriesdetailsViewComponent : WViewComponent
    {
        public AdmininquiriesdetailsViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdmininquiriesdetailsViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class AdmininquiriesdetailsViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string lblSubject { get; set; } = string.Empty;
        public string lblInquiryType { get; set; } = string.Empty;
        public string lblName { get; set; } = string.Empty;
        public string lblEmail { get; set; } = string.Empty;
        public string lblAddressLine1 { get; set; } = string.Empty;
        public string lblAddressLine2 { get; set; } = string.Empty;
        public string lblCity { get; set; } = string.Empty;
        public string lblCountry { get; set; } = string.Empty;
        public string lblState { get; set; } = string.Empty;
        public string lblZipCode { get; set; } = string.Empty;
        public string lblPhone { get; set; } = string.Empty;
        public string lblFax { get; set; } = string.Empty;
        public string lblSendTo { get; set; } = string.Empty;
        public string lblSendToEmail { get; set; } = string.Empty;
        public string lblDateTime { get; set; } = string.Empty;
        public string lblMessage { get; set; } = string.Empty;
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string DateTime { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Fax { get; set; } = string.Empty;
        public string InquiryType { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string SendTo { get; set; } = string.Empty;
        public string SendToEmail { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
    }
}
