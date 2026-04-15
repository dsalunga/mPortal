using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from AdminLocation.ascx (SystemParts/EventCalendar).
    /// </summary>
    public class AdminlocationViewComponent : WViewComponent
    {
        public AdminlocationViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdminlocationViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class AdminlocationViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string txtName { get; set; } = string.Empty;
        public bool chkBookable { get; set; }
    }
}
