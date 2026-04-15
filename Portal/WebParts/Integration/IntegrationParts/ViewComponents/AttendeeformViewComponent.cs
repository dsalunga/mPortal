using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from AttendeeForm.ascx (Apps/Integration/EventRegister).
    /// </summary>
    public class AttendeeformViewComponent : WViewComponent
    {
        public AttendeeformViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AttendeeformViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class AttendeeformViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<SelectOption> CboCoordinatorOptions { get; set; } = new();
        public List<SelectOption> CboCountriesOptions { get; set; } = new();
        public List<SelectOption> CboGenderOptions { get; set; } = new();
        public string Email { get; set; } = string.Empty;
        public string ExternalId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Locale { get; set; } = string.Empty;
        public string MembershipDate { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string Nickname { get; set; } = string.Empty;
        public string SelectedCboCoordinator { get; set; } = string.Empty;
        public string SelectedCboCountries { get; set; } = string.Empty;
        public string SelectedCboGender { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
    }
