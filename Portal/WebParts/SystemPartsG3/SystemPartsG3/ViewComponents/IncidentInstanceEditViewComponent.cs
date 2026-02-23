using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Edit form for an incident instance.
    /// Replaces InstanceEditView.ascx (SystemPartsG3/Incident).
    /// Usage: @await Component.InvokeAsync("IncidentInstanceEdit", new { objectId })
    /// </summary>
    public class IncidentInstanceEditViewComponent : WViewComponent
    {
        public IncidentInstanceEditViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new IncidentInstanceEditViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                InstanceId = 0,
                Name = string.Empty,
                IncidentPrefix = string.Empty,
                BaseGroup = string.Empty,
                SupportGroupPath = string.Empty,
                SLAHighDuration = 0,
                SLALowDuration = 0,
                SLANormalDuration = 0,
                SLAWarningPercentage = 0.0
            };

            return View(model);
        }
    }

    public class IncidentInstanceEditViewModel
    {
        public int ObjectId { get; set; }
        public int InstanceId { get; set; }
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
