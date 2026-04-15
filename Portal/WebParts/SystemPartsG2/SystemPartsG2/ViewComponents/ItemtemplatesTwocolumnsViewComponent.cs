using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from TwoColumns.ascx (AppBundle2/BasicList/ItemTemplates).
    /// </summary>
    public class ItemtemplatesTwocolumnsViewComponent : WViewComponent
    {
        public ItemtemplatesTwocolumnsViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ItemtemplatesTwocolumnsViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class ItemtemplatesTwocolumnsViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
    }
}
