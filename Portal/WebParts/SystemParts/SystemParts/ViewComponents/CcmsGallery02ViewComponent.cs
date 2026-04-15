using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from CCMS_Gallery_02.ascx (SystemParts/Photo).
    /// </summary>
    public class CcmsGallery02ViewComponent : WViewComponent
    {
        public CcmsGallery02ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new CcmsGallery02ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class CcmsGallery02ViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<CcmsGallery02Item> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class CcmsGallery02Item
    {
        public string Active { get; set; } = string.Empty;
        public string Caption { get; set; } = string.Empty;
        public string DateCreated { get; set; } = string.Empty;
        public string Edit { get; set; } = string.Empty;
        public int Id { get; set; }
        public string PhotoName { get; set; } = string.Empty;
        public string Preview { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
