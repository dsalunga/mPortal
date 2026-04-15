using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from ConfigInquiriesList.ascx (SystemParts/Contact).
    /// </summary>
    public class ConfiginquirieslistViewComponent : WViewComponent
    {
        public ConfiginquirieslistViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ConfiginquirieslistViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class ConfiginquirieslistViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<ConfiginquirieslistItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class ConfiginquirieslistItem
    {
        public string Email { get; set; } = string.Empty;
        public int Id { get; set; }
        public string InqDateTime { get; set; } = string.Empty;
        public string InquiryType { get; set; } = string.Empty;
        public string SendTo { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string View { get; set; } = string.Empty;
    }
}
