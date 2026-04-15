using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from AdminContactDetails.ascx (SystemParts/Contact).
    /// </summary>
    public class AdmincontactdetailsViewComponent : WViewComponent
    {
        public AdmincontactdetailsViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdmincontactdetailsViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class AdmincontactdetailsViewModel
    {
public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string txtName { get; set; } = string.Empty;
        public string txtEmail { get; set; } = string.Empty;
        public string txtSubject { get; set; } = string.Empty;
        public string txtRank { get; set; } = string.Empty;
        public bool chkActive { get; set; }
    }
}
