using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace BibleReader.ViewComponents
{
    /// <summary>
    /// Ported from Setup.aspx ().
    /// </summary>
    public class SetupViewComponent : WViewComponent
    {
        public SetupViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new SetupViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

    public class SetupViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
    }
}
