using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Lists incident categories with add/edit/delete actions.
    /// Replaces CategoryManagerView.ascx (SystemPartsG3/Incident).
    /// Usage: @await Component.InvokeAsync("IncidentCategoryManager", new { objectId })
    /// </summary>
    public class IncidentCategoryManagerViewComponent : WViewComponent
    {
        public IncidentCategoryManagerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new IncidentCategoryManagerViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                Categories = new List<IncidentCategoryItem>(),
                CanEdit = WcmsSession.IsLoggedIn
            };

            return View("~/Views/Shared/Components/Incident/CategoryManagerView/Default.cshtml", model);
        }
    }

    public class IncidentCategoryManagerViewModel
    {
        public int ObjectId { get; set; }
        public List<IncidentCategoryItem> Categories { get; set; }
        public bool CanEdit { get; set; }
    }

    public class IncidentCategoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int GroupId { get; set; }
        public int Rank { get; set; }
        public int InstanceId { get; set; }
    }
}
