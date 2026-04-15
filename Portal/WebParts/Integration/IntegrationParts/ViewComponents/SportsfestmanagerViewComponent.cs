using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from SportsfestManager.ascx (Apps/Integration/Profile).
    /// </summary>
    public class SportsfestmanagerViewComponent : WViewComponent
    {
        public SportsfestmanagerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new SportsfestmanagerViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class SportsfestmanagerViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<SportsfestmanagerItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class SportsfestmanagerItem
    {
        public string Age { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string EntryDate { get; set; } = string.Empty;
        public string GroupColor { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Locale { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ShirtSize { get; set; } = string.Empty;
        public string Sports { get; set; } = string.Empty;
        public string Suggestion { get; set; } = string.Empty;
    }
}
