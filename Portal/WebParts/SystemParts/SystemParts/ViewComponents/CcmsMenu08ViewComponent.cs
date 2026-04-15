using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from CCMS_Menu_08.ascx (SystemParts/Menu).
    /// </summary>
    public class CcmsMenu08ViewComponent : WViewComponent
    {
        public CcmsMenu08ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new CcmsMenu08ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class CcmsMenu08ViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<object> ObjectDataSource1Data { get; set; } = new();
    }
}
