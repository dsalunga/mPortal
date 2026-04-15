using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from WebGroupUsers.ascx (Apps/Integration/Profile).
    /// </summary>
    public class ProfileWebgroupusersViewComponent : WViewComponent
    {
        public ProfileWebgroupusersViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ProfileWebgroupusersViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class ProfileWebgroupusersViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<ProfileWebgroupusersItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class ProfileWebgroupusersItem
    {
        public string Appr { get; set; } = string.Empty;
        public string DateJoined { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ExternalIDNo { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public int Id { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
