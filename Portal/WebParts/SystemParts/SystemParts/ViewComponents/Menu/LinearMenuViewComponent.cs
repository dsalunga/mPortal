using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Single-level horizontal menu. Replaces LinearMenu.ascx (SystemParts/Menu).
    /// Usage: @await Component.InvokeAsync("LinearMenu", new { objectId, recordId })
    /// </summary>
    public class LinearMenuViewComponent : WViewComponent
    {
        public LinearMenuViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new LinearMenuViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                Items = new List<LinearMenuItem>(),
                BasePath = WcmsContext.BasePath
            };

            return View("~/Views/Shared/Components/Menu/LinearMenu/Default.cshtml", model);
        }
    }

    public class LinearMenuViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public string BasePath { get; set; }
        public List<LinearMenuItem> Items { get; set; }
    }

    public class LinearMenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public string CssClass { get; set; }
    }
}
