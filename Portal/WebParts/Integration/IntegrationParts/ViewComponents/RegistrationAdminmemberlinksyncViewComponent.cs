using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from AdminMemberLinkSync.ascx (Apps/Integration/Registration).
    /// </summary>
    public class RegistrationAdminmemberlinksyncViewComponent : WViewComponent
    {
        public RegistrationAdminmemberlinksyncViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new RegistrationAdminmemberlinksyncViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class RegistrationAdminmemberlinksyncViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<RegistrationAdminmemberlinksyncItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class RegistrationAdminmemberlinksyncItem
    {
        public string Actions { get; set; } = string.Empty;
        public string CMS { get; set; } = string.Empty;
        public string DateCreated { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string External { get; set; } = string.Empty;
        public string ExternalIdNo { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public int Id { get; set; }
        public string ItemType { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MembershipDate { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
