using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from RegistrationV2.ascx (Apps/Integration/Registration).
    /// </summary>
    public class Registrationv2ViewComponent : WViewComponent
    {
        public Registrationv2ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new Registrationv2ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class Registrationv2ViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<SelectOption> CboCountriesOptions { get; set; } = new();
        public List<SelectOption> CboGenderOptions { get; set; } = new();
        public string EnterEmail { get; set; } = string.Empty;
        public string ExternalID { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Locale { get; set; } = string.Empty;
        public string MembershipDate { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string PortalEmail { get; set; } = string.Empty;
        public string RegisterEmail { get; set; } = string.Empty;
        public string SelectedCboCountries { get; set; } = string.Empty;
        public string SelectedCboGender { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
    }
