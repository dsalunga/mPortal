using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from ConfigExternalMembers.ascx (Apps/Integration/Registration).
    /// </summary>
    public class ConfigexternalmembersViewComponent : WViewComponent
    {
        public ConfigexternalmembersViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ConfigexternalmembersViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class ConfigexternalmembersViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<ConfigexternalmembersItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class ConfigexternalmembersItem
    {
        public string Edit { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ExternalIDNo { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public int Id { get; set; }
        public string TemporaryIDNo { get; set; } = string.Empty;
    }
}
