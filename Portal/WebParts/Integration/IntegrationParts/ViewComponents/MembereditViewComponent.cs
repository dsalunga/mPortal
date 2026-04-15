using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from MemberEdit.ascx (Apps/Integration/MasterList).
    /// </summary>
    public class MembereditViewComponent : WViewComponent
    {
        public MembereditViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new MembereditViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class MembereditViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public bool AssignedCouncillor { get; set; } = false;
        public List<SelectOption> CboOfficerPositionOptions { get; set; } = new();
        public List<SelectOption> CboVoiceDesignationOptions { get; set; } = new();
        public string Email { get; set; } = string.Empty;
        public string ExternalID { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public bool GroupConductor { get; set; } = false;
        public bool GroupManager { get; set; } = false;
        public bool GroupMentor { get; set; } = false;
        public string HomeAddress1 { get; set; } = string.Empty;
        public string HomeAddress2 { get; set; } = string.Empty;
        public string HomeAddressZipCode { get; set; } = string.Empty;
        public string HomePhone { get; set; } = string.Empty;
        public bool IsOfficer { get; set; } = false;
        public string LastName { get; set; } = string.Empty;
        public string MembershipDate { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public List<SelectOption> ObjectDataSourceCountriesOptions { get; set; } = new();
        public string SelectedCboOfficerPosition { get; set; } = string.Empty;
        public string SelectedCboVoiceDesignation { get; set; } = string.Empty;
        public string SelectedObjectDataSourceCountries { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
    }
