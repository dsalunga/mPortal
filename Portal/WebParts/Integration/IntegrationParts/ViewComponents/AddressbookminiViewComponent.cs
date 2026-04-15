using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from AddressBookMini.ascx (Apps/Integration/Profile).
    /// </summary>
    public class AddressbookminiViewComponent : WViewComponent
    {
        public AddressbookminiViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AddressbookminiViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class AddressbookminiViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string EralOutputHtml { get; set; } = string.Empty;
    }
}
