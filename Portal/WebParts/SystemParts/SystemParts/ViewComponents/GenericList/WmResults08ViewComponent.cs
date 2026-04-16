using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from WM_Results_08.ascx (SystemParts/GenericList).
    /// </summary>
    public class WmResults08ViewComponent : WViewComponent
    {
        public WmResults08ViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new WmResults08ViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/GenericList/WM_Results_08/Default.cshtml", model);
        }
    }

        public class WmResults08ViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public List<WmResults08Item> Items { get; set; } = new();
        public int PageSize { get; set; } = 20;
        public int TotalItems { get; set; }
    }

    public class WmResults08Item
    {
        public string Answers { get; set; } = string.Empty;
        public string DateTimeTaken { get; set; } = string.Empty;
        public int Id { get; set; }
        public string ResponseID { get; set; } = string.Empty;
    }
}
