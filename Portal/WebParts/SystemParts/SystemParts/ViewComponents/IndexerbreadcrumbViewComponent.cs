using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from IndexerBreadcrumb.ascx (SystemParts/FileManager/Controls).
    /// </summary>
    public class IndexerbreadcrumbViewComponent : WViewComponent
    {
        public IndexerbreadcrumbViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new IndexerbreadcrumbViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class IndexerbreadcrumbViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string LRootHtml { get; set; } = string.Empty;
    }
}
