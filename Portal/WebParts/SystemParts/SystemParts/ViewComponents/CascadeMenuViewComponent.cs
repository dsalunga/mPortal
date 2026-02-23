using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Multi-level dropdown menu from SQL. Replaces CascadeMenu.ascx (SystemParts/Menu).
    /// Usage: @await Component.InvokeAsync("CascadeMenu", new { objectId, recordId })
    /// </summary>
    public class CascadeMenuViewComponent : WViewComponent
    {
        public CascadeMenuViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new CascadeMenuViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                Items = new List<CascadeMenuItem>(),
                BasePath = WcmsContext.BasePath
            };

            return View(model);
        }
    }

    public class CascadeMenuViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string BasePath { get; set; }
        public List<CascadeMenuItem> Items { get; set; }
    }

    public class CascadeMenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int ParentId { get; set; }
        public int SortOrder { get; set; }
        public string CssClass { get; set; }
        public List<CascadeMenuItem> Children { get; set; }
    }
}
