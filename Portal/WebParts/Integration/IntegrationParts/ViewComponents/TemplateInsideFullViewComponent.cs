using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from template_inside_full.ascx (Themes/mcgi_website).
    /// </summary>
    public class TemplateInsideFullViewComponent : WViewComponent
    {
        public TemplateInsideFullViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new TemplateInsideFullViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class TemplateInsideFullViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string ContentHtml { get; set; } = string.Empty;
    }
}
