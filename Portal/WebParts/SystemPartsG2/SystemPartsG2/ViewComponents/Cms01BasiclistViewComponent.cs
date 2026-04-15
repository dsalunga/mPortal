using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from CMS_01_BasicList.ascx (AppBundle2/BasicList).
    /// </summary>
    public class Cms01BasiclistViewComponent : WViewComponent
    {
        public Cms01BasiclistViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new Cms01BasiclistViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class Cms01BasiclistViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<Cms01BasiclistItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class Cms01BasiclistItem
    {
        public string Edit { get; set; } = string.Empty;
        public string Field1 { get; set; } = string.Empty;
        public string Field2 { get; set; } = string.Empty;
        public string Field3 { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Rank { get; set; } = string.Empty;
    }
}
