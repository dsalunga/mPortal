using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Ported from CMS_ListingPage.ascx (SystemParts/Menu).
    /// </summary>
    public class CmsListingpageViewComponent : WViewComponent
    {
        public CmsListingpageViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new CmsListingpageViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/Menu/CMS_ListingPage/Default.cshtml", model);
        }
    }

        public class CmsListingpageViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<SelectOption> CboPaddingOptions { get; set; } = new();
        public List<SelectOption> CboRepeatColumnsOptions { get; set; } = new();
        public List<SelectOption> CboTypeOptions { get; set; } = new();
        public string HeaderText { get; set; } = string.Empty;
        public string Height { get; set; } = string.Empty;
        public string SelectedCboPadding { get; set; } = string.Empty;
        public string SelectedCboRepeatColumns { get; set; } = string.Empty;
        public string SelectedCboType { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
        public string Width { get; set; } = string.Empty;
    }
    }
