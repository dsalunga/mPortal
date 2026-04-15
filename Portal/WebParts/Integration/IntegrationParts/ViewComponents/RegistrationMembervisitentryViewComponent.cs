using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from MemberVisitEntry.ascx (Apps/Integration/Registration).
    /// </summary>
    public class RegistrationMembervisitentryViewComponent : WViewComponent
    {
        public RegistrationMembervisitentryViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new RegistrationMembervisitentryViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class RegistrationMembervisitentryViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string ActionTaken { get; set; } = string.Empty;
        public string ActualReport { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<SelectOption> CboGroupOptions { get; set; } = new();
        public List<SelectOption> CboTagOptions { get; set; } = new();
        public List<SelectOption> CboTimesVisitedOptions { get; set; } = new();
        public string ContactNo { get; set; } = string.Empty;
        public string DateVisited { get; set; } = string.Empty;
        public string Member { get; set; } = string.Empty;
        public string MembershipDate { get; set; } = string.Empty;
        public string SelectedCboGroup { get; set; } = string.Empty;
        public string SelectedCboTag { get; set; } = string.Empty;
        public string SelectedCboTimesVisited { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
    }
