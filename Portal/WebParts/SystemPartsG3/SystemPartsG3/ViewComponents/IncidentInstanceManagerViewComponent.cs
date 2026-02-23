using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Lists incident instances.
    /// Replaces InstanceManagerView.ascx (SystemPartsG3/Incident).
    /// Usage: @await Component.InvokeAsync("IncidentInstanceManager", new { objectId })
    /// </summary>
    public class IncidentInstanceManagerViewComponent : WViewComponent
    {
        public IncidentInstanceManagerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new IncidentInstanceManagerViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                Instances = new List<IncidentInstanceItem>(),
                CanEdit = WcmsSession.IsLoggedIn
            };

            return View(model);
        }
    }

    public class IncidentInstanceManagerViewModel
    {
        public int ObjectId { get; set; }
        public List<IncidentInstanceItem> Instances { get; set; }
        public bool CanEdit { get; set; }
    }

    public class IncidentInstanceItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IncidentPrefix { get; set; }
        public string BaseGroup { get; set; }
        public string SupportGroupPath { get; set; }
        public int SLAHighDuration { get; set; }
        public int SLALowDuration { get; set; }
        public int SLANormalDuration { get; set; }
        public double SLAWarningPercentage { get; set; }
    }
}
