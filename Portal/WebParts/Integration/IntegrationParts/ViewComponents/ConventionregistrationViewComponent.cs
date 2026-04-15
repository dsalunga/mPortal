using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from ConventionRegistration.ascx (Apps/Integration/Registration).
    /// </summary>
    public class ConventionregistrationViewComponent : WViewComponent
    {
        public ConventionregistrationViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ConventionregistrationViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class ConventionregistrationViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Airline { get; set; } = string.Empty;
        public string ArrivalDate { get; set; } = string.Empty;
        public List<SelectOption> CboAgeOptions { get; set; } = new();
        public List<SelectOption> CboCountryOptions { get; set; } = new();
        public List<SelectOption> CboGenderOptions { get; set; } = new();
        public string DepartureDate { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public string ExternalId { get; set; } = string.Empty;
        public string FlightNo { get; set; } = string.Empty;
        public string Locale { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string PlaceType { get; set; } = string.Empty;
        public List<SelectOption> RblPlaceToStayOptions { get; set; } = new();
        public string SelectedCboAge { get; set; } = string.Empty;
        public string SelectedCboCountry { get; set; } = string.Empty;
        public string SelectedCboGender { get; set; } = string.Empty;
        public string SelectedRblPlaceToStay { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
    }
