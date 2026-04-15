using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from ProfileUpdate.ascx (Apps/Integration/Profile).
    /// </summary>
    public class ProfileupdateViewComponent : WViewComponent
    {
        public ProfileupdateViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ProfileupdateViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class ProfileupdateViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string HomeAddress1 { get; set; } = string.Empty;
        public string HomeAddress2 { get; set; } = string.Empty;
        public string HomeAddressZipCode { get; set; } = string.Empty;
        public string HomePhone { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string NewEmail { get; set; } = string.Empty;
        public string Nickname { get; set; } = string.Empty;
        public List<SelectOption> ObjectDataSourceCountriesOptions { get; set; } = new();
        public List<SelectOption> ObjectDataSourceUSStatesOptions { get; set; } = new();
        public string SelectedObjectDataSourceCountries { get; set; } = string.Empty;
        public string SelectedObjectDataSourceUSStates { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
        public string StatusText { get; set; } = string.Empty;
        public string WorkAddress1 { get; set; } = string.Empty;
        public string WorkAddress2 { get; set; } = string.Empty;
        public string WorkAddressZipCode { get; set; } = string.Empty;
        public string WorkDesignation { get; set; } = string.Empty;
        public string WorkPhone { get; set; } = string.Empty;
    }
    }
