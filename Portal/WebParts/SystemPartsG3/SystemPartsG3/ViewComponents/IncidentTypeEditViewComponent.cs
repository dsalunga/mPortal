using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Edit form for an incident type.
    /// Replaces TypeEditView.ascx (SystemPartsG3/Incident).
    /// Usage: @await Component.InvokeAsync("IncidentTypeEdit", new { objectId })
    /// </summary>
    public class IncidentTypeEditViewComponent : WViewComponent
    {
        public IncidentTypeEditViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new IncidentTypeEditViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                TypeId = 0,
                Name = string.Empty,
                Rank = 0,
                InstanceId = 0,
                UseSLA = false
            };

            return View(model);
        }
    }

    public class IncidentTypeEditViewModel
    {
        public int ObjectId { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
        public int InstanceId { get; set; }
        public bool UseSLA { get; set; }
    }
}
