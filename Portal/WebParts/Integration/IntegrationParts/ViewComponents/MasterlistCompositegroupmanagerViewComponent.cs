using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from CompositeGroupManager.ascx (Apps/Integration/MasterList).
    /// </summary>
    public class MasterlistCompositegroupmanagerViewComponent : WViewComponent
    {
        public MasterlistCompositegroupmanagerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new MasterlistCompositegroupmanagerViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class MasterlistCompositegroupmanagerViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<MasterlistCompositegroupmanagerItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class MasterlistCompositegroupmanagerItem
    {
        public string Actions { get; set; } = string.Empty;
        public string Active { get; set; } = string.Empty;
        public string DateModified { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public int Id { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
