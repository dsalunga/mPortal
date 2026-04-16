using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Edit form for an incident category.
    /// Replaces CategoryEditView.ascx (SystemPartsG3/Incident).
    /// Usage: @await Component.InvokeAsync("IncidentCategoryEdit", new { objectId })
    /// </summary>
    public class IncidentCategoryEditViewComponent : WViewComponent
    {
        public IncidentCategoryEditViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new IncidentCategoryEditViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                CategoryId = 0,
                Name = string.Empty,
                Description = string.Empty,
                GroupId = 0,
                Rank = 0,
                InstanceId = 0
            };

            return View("~/Views/Shared/Components/Incident/CategoryEditView/Default.cshtml", model);
        }
    }

    public class IncidentCategoryEditViewModel
    {
        public int ObjectId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int GroupId { get; set; }
        public int Rank { get; set; }
        public int InstanceId { get; set; }
    }
}
