using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace BibleReader.ViewComponents
{
    /// <summary>
    /// Ported from ChangePassword.aspx (Account).
    /// </summary>
    public class AccountChangepasswordViewComponent : WViewComponent
    {
        public AccountChangepasswordViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AccountChangepasswordViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class AccountChangepasswordViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
    }
}
