using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from AdminTemplateComposer.ascx (SystemParts/Article).
    /// </summary>
    public class AdmintemplatecomposerViewComponent : WViewComponent
    {
        public AdmintemplatecomposerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdmintemplatecomposerViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class AdmintemplatecomposerViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<object> grvContentData { get; set; } = new();
        public string txtName { get; set; } = string.Empty;
        public string hidValue { get; set; } = string.Empty;
    }
}
