using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Master page manager. Replaces WebMasterPages.ascx (Central/WebSites).
    /// Usage: @await Component.InvokeAsync("WebMasterPages")
    /// </summary>
    public class WebMasterPagesViewComponent : WViewComponent
    {
        public WebMasterPagesViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int selectedMasterPageId = 0)
        {
            var siteId = ResolveSiteId();
            var model = new WebMasterPagesViewModel
            {
                SelectedMasterPageId = ResolveSelectedMasterPageId(selectedMasterPageId),
                MasterPages = new List<MasterPageItem>()
            };

            try
            {
                var site = siteId > 0 ? WSite.Get(siteId) : null;
                var masterPages = GetMasterPages(siteId);

                model.MasterPages = masterPages
                    .OrderBy(i => i.Name)
                    .Select(masterPage => new MasterPageItem
                    {
                        Id = masterPage.Id,
                        Name = masterPage.Name,
                        Description = masterPage.GetParameterValue(WebColumns.Description, string.Empty) ?? string.Empty,
                        FileName = masterPage.Template?.FileName ?? string.Empty,
                        IsDefault = site != null && site.DefaultMasterPageId == masterPage.Id,
                        IsActive = true,
                        ModifiedDate = null
                    })
                    .ToList();

                if (model.SelectedMasterPageId < 1 && model.MasterPages.Count > 0)
                    model.SelectedMasterPageId = model.MasterPages[0].Id;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Failed to load master pages: {ex.Message}";
            }

            return View(model);
        }

        private int ResolveSelectedMasterPageId(int selectedMasterPageId)
        {
            if (selectedMasterPageId > 0)
                return selectedMasterPageId;

            return DataUtil.GetId(Request, WebColumns.MasterPageId);
        }

        private int ResolveSiteId()
        {
            return DataUtil.GetId(Request, WebColumns.SiteId);
        }

        private static List<WebMasterPage> GetMasterPages(int siteId)
        {
            if (siteId > 0)
                return WebMasterPage.GetList(siteId)?.ToList() ?? new List<WebMasterPage>();

            var all = new List<WebMasterPage>();
            var sites = WSite.GetList()?.ToList() ?? new List<WSite>();
            foreach (var site in sites)
            {
                var siteMasterPages = WebMasterPage.GetList(site.Id)?.ToList();
                if (siteMasterPages != null && siteMasterPages.Count > 0)
                    all.AddRange(siteMasterPages);
            }

            return all.GroupBy(i => i.Id).Select(i => i.First()).ToList();
        }
    }

    public class WebMasterPagesViewModel
    {
        public int SelectedMasterPageId { get; set; }
        public List<MasterPageItem> MasterPages { get; set; } = new List<MasterPageItem>();
        public string ErrorMessage { get; set; }
    }

    public class MasterPageItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
