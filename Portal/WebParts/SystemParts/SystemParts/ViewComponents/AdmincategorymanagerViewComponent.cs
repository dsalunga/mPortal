using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from AdminCategoryManager.ascx (SystemParts/EventCalendar).
    /// </summary>
    public class AdmincategorymanagerViewComponent : WViewComponent
    {
        public AdmincategorymanagerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdmincategorymanagerViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class AdmincategorymanagerViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string txtSearch { get; set; } = string.Empty;
        public List<object> ObjectDataSource1Data { get; set; } = new();
    }
}
