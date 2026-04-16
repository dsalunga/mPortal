using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.G2.ViewComponents
{
    /// <summary>
    /// Ported from CMS_ListingPage.ascx (AppBundle2/SiteList).
    /// </summary>
    public class SitelistCmsListingpageViewComponent : WViewComponent
    {
        public SitelistCmsListingpageViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new SitelistCmsListingpageViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View("~/Views/Shared/Components/AppBundle2/SiteList/CMS_ListingPage/SitelistCmsListingpage/Default.cshtml", model);
        }
    }

        public class SitelistCmsListingpageViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public List<SelectOption> CboPaddingOptions { get; set; } = new();
        public List<SelectOption> CboRepeatColumnsOptions { get; set; } = new();
        public List<SelectOption> CboSitesOptions { get; set; } = new();
        public string HeaderText { get; set; } = string.Empty;
        public string Height { get; set; } = string.Empty;
        public string SelectedCboPadding { get; set; } = string.Empty;
        public string SelectedCboRepeatColumns { get; set; } = string.Empty;
        public string SelectedCboSites { get; set; } = string.Empty;
        public bool SortName { get; set; } = false;
        public string StatusMessage { get; set; } = string.Empty;
        public string Width { get; set; } = string.Empty;
    }
    }
