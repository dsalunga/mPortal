using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from CCMS_Category.ascx (SystemParts/Photo).
    /// </summary>
    public class CcmsCategoryViewComponent : WViewComponent
    {
        public CcmsCategoryViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new CcmsCategoryViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/Photo/CCMS_Category/Default.cshtml", model);
        }
    }

        public class CcmsCategoryViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<CcmsCategoryItem> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class CcmsCategoryItem
    {
        public string Actions { get; set; } = string.Empty;
        public int Id { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
