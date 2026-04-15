using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from FullView.ascx (SystemParts/Photo/Controls).
    /// </summary>
    public class FullviewViewComponent : WViewComponent
    {
        public FullviewViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new FullviewViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class FullviewViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string lCategory { get; set; } = string.Empty;
        public string lThumbs { get; set; } = string.Empty;
        public string lNums { get; set; } = string.Empty;
        public string lPrev { get; set; } = string.Empty;
        public string lNext { get; set; } = string.Empty;
    }
}
