using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from MemberSessions.ascx (Apps/Integration/LessonReviewer).
    /// </summary>
    public class MembersessionsViewComponent : WViewComponent
    {
        public MembersessionsViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new MembersessionsViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class MembersessionsViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<MembersessionsItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class MembersessionsItem
    {
        public int Id { get; set; }
        public string IdleTime { get; set; } = string.Empty;
        public string MakeUpStartDate { get; set; } = string.Empty;
        public string MakeUpTime { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string PageId { get; set; } = string.Empty;
        public string SessionTime { get; set; } = string.Empty;
    }
}
