using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace LessonReviewer.ViewComponents
{
    /// <summary>
    /// Ported from Login.aspx (Admin).
    /// </summary>
    public class AdminLoginViewComponent : WViewComponent
    {
        public AdminLoginViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdminLoginViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class AdminLoginViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
    }
}
