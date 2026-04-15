using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from ConfigMenuItems.ascx (SystemParts/Menu).
    /// </summary>
    public class ConfigmenuitemsViewComponent : WViewComponent
    {
        public ConfigmenuitemsViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new ConfigmenuitemsViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class ConfigmenuitemsViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<ConfigmenuitemsItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class ConfigmenuitemsItem
    {
        public string Active { get; set; } = string.Empty;
        public string Edit { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Permission { get; set; } = string.Empty;
        public string Rank { get; set; } = string.Empty;
        public string Target { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }
}
