using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    public class OfficedetailsViewComponent : WViewComponent
    {
        private const string StatusKey = "Officedetails.StatusMessage";
        private const string ErrorKey = "Officedetails.ErrorMessage";

        public OfficedetailsViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var postController = WcmsContext.Element?.GetParameterValue("PostController");
            if (string.IsNullOrWhiteSpace(postController) ||
                postController.Equals("Admin", System.StringComparison.OrdinalIgnoreCase))
            {
                postController = "CentralPartActions";
            }

            var officeId = WcmsContext.GetId(WebColumns.OfficeId);
            var item = officeId > 0 ? WebOffice.Get(officeId) : null;
            var cancelRedirect = WcmsContext.Element?.GetParameterValue("CancelRedirect");
            var model = new OfficedetailsViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                PostController = postController,
                PostAction = WcmsContext.Element?.GetParameterValue("PostAction") ?? "Officedetails",
                OfficeId = officeId,
                CancelRedirect = cancelRedirect ?? string.Empty
            };

            if (item != null)
            {
                model.Name = item.Name;
                model.Label4 = item.EmailAddress;
                model.Label1 = item.AddressLine1;
                model.Label6 = item.PhoneNumber;
                model.Label5 = item.MobileNumber;
                model.Label3 = item.ContactPerson;
            }
            else
            {
                model.ErrorMessage = "Office details are not available.";
            }

            if (TempData.TryGetValue(StatusKey, out var status) && status != null)
                model.StatusMessage = status.ToString();

            if (TempData.TryGetValue(ErrorKey, out var error) && error != null)
                model.ErrorMessage = error.ToString();

            return View(model);
        }
    }

    public class OfficedetailsViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int OfficeId { get; set; }
        public string Name { get; set; } = "Locale Name";
        public string CancelRedirect { get; set; } = string.Empty;
        public string PostController { get; set; } = "CentralPartActions";
        public string PostAction { get; set; } = "Officedetails";
        public string StatusMessage { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;

        public string Label4 { get; set; } = string.Empty;
        public string Label1 { get; set; } = string.Empty;
        public string Label6 { get; set; } = string.Empty;
        public string Label5 { get; set; } = string.Empty;
        public string Label3 { get; set; } = string.Empty;
        public string lblStatus { get; set; } = string.Empty;

    }
}
