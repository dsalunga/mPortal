using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from GroupEdit.ascx (Apps/Integration/MasterList).
    /// </summary>
    public class MasterlistGroupeditViewComponent : WViewComponent
    {
        public MasterlistGroupeditViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new MasterlistGroupeditViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class MasterlistGroupeditViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string Conductors { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Managers { get; set; } = string.Empty;
        public string Mentors { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
        public string Parent { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }
}
