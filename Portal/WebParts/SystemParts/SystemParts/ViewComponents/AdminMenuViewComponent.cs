using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Menu item admin CRUD. Replaces AdminMenu.ascx (SystemParts/Menu).
    /// Usage: @await Component.InvokeAsync("AdminMenu", new { objectId, recordId })
    /// </summary>
    public class AdminMenuViewComponent : WViewComponent
    {
        public AdminMenuViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new AdminMenuViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                Items = new List<AdminMenuItemModel>(),
                IsLoggedIn = WcmsSession.IsLoggedIn
            };

            return View(model);
        }
    }

    public class AdminMenuViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public bool IsLoggedIn { get; set; }
        public List<AdminMenuItemModel> Items { get; set; }
    }

    public class AdminMenuItemModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int ParentId { get; set; }
        public int SortOrder { get; set; }
        public string Target { get; set; }
        public string CssClass { get; set; }
        public bool IsVisible { get; set; }
    }
}
