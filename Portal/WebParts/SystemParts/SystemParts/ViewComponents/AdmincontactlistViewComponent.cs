using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from AdminContactList.ascx (SystemParts/Contact).
    /// </summary>
    public class AdmincontactlistViewComponent : WViewComponent
    {
        public AdmincontactlistViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdmincontactlistViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class AdmincontactlistViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<object> ObjectDataSourceContactsData { get; set; } = new();
    }
}
