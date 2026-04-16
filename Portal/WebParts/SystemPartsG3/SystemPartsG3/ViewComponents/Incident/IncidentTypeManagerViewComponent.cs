using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Lists incident types with filtering.
    /// Replaces TypeManagerView.ascx (SystemPartsG3/Incident).
    /// Usage: @await Component.InvokeAsync("IncidentTypeManager", new { objectId })
    /// </summary>
    public class IncidentTypeManagerViewComponent : WViewComponent
    {
        public IncidentTypeManagerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new IncidentTypeManagerViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                Types = new List<IncidentTypeItem>(),
                CanEdit = WcmsSession.IsLoggedIn,
                FilterText = string.Empty
            };

            return View("~/Views/Shared/Components/Incident/TypeManagerView/Default.cshtml", model);
        }
    }

    public class IncidentTypeManagerViewModel
    {
        public int ObjectId { get; set; }
        public List<IncidentTypeItem> Types { get; set; }
        public bool CanEdit { get; set; }
        public string FilterText { get; set; }
    }

    public class IncidentTypeItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
        public int InstanceId { get; set; }
        public bool UseSLA { get; set; }
    }
}
