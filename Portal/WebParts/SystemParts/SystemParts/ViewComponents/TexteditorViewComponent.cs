using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from TextEditor.ascx (SystemParts/FileManager).
    /// </summary>
    public class TexteditorViewComponent : WViewComponent
    {
        public TexteditorViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new TexteditorViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class TexteditorViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string lblStatus { get; set; } = string.Empty;
        public string txtContent { get; set; } = string.Empty;
    }
}
