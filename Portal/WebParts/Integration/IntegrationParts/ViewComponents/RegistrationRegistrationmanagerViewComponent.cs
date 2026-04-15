using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from RegistrationManager.ascx (Apps/Integration/Registration).
    /// </summary>
    public class RegistrationRegistrationmanagerViewComponent : WViewComponent
    {
        public RegistrationRegistrationmanagerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new RegistrationRegistrationmanagerViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class RegistrationRegistrationmanagerViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<RegistrationRegistrationmanagerItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class RegistrationRegistrationmanagerItem
    {
        public string Address { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;
        public string Airline { get; set; } = string.Empty;
        public string ArrivalDate { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string DepartureDate { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public string EntryDate { get; set; } = string.Empty;
        public string ExternalId { get; set; } = string.Empty;
        public string FlightNo { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Locale { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string PlaceType { get; set; } = string.Empty;
    }
}
