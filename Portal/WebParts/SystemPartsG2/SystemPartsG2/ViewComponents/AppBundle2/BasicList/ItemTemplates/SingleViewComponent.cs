using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from Single.ascx (AppBundle2/BasicList/ItemTemplates).
    /// </summary>
    public class SingleViewComponent : WViewComponent
    {
        public SingleViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new SingleViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/AppBundle2/BasicList/ItemTemplates/Single/Single/Default.cshtml", model);
        }
    }

        public class SingleViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<SingleItem> Items { get; set; } = new();
    }

    public class SingleItem
    {
        public string Field1 { get; set; } = string.Empty;
    }
}
