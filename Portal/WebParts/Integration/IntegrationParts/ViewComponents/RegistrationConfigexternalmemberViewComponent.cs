using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from ConfigExternalMember.ascx (Apps/Integration/Registration).
    /// </summary>
    public class RegistrationConfigexternalmemberViewComponent : WViewComponent
    {
        public RegistrationConfigexternalmemberViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new RegistrationConfigexternalmemberViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class RegistrationConfigexternalmemberViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string ExternalIDNo { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
        public string TempExternalID { get; set; } = string.Empty;
    }
}
