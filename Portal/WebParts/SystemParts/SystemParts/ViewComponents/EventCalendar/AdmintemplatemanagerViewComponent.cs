using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from AdminTemplateManager.ascx (SystemParts/EventCalendar).
    /// </summary>
    public class AdmintemplatemanagerViewComponent : WViewComponent
    {
        public AdmintemplatemanagerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdmintemplatemanagerViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/EventCalendar/AdminTemplateManager/Default.cshtml", model);
        }
    }

        public class AdmintemplatemanagerViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<AdmintemplatemanagerItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class AdmintemplatemanagerItem
    {
        public string Actions { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public int ID { get; set; }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
