using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from WM_CreateQuestion_06.ascx (SystemParts/GenericList).
    /// </summary>
    public class WmCreatequestion06ViewComponent : WViewComponent
    {
        public WmCreatequestion06ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new WmCreatequestion06ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class WmCreatequestion06ViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string txtQLabel { get; set; } = string.Empty;
        public string txtQRanking { get; set; } = string.Empty;
        public bool chkRequired { get; set; }
        public bool chkHorizontal { get; set; }
    }
}
