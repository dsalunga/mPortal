using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from ImportExport.ascx (SystemParts/Menu).
    /// </summary>
    public class ImportexportViewComponent : WViewComponent
    {
        public ImportexportViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ImportexportViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class ImportexportViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<SelectListItem> ObjectDataSource1Items { get; set; } = new();
        public string ObjectDataSource1Selected { get; set; } = string.Empty;
        public string lblStatus { get; set; } = string.Empty;
    }
}
