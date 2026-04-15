using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from Login.ascx (Apps/Integration/Registration).
    /// </summary>
    public class RegistrationRegistrationLoginViewComponent : WViewComponent
    {
        public RegistrationRegistrationLoginViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new RegistrationRegistrationLoginViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class RegistrationRegistrationLoginViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string StatusMessage { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
