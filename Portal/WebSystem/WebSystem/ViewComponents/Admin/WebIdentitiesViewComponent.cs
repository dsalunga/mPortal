using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Site identity/domain manager. Replaces WebIdentities.ascx (Central/WebSites).
    /// Usage: @await Component.InvokeAsync("WebIdentities")
    /// </summary>
    public class WebIdentitiesViewComponent : WViewComponent
    {
        public WebIdentitiesViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int siteId = 0)
        {
            var model = new WebIdentitiesViewModel
            {
                SiteId = ResolveSiteId(siteId),
                Identities = new List<WebIdentityItem>()
            };

            try
            {
                if (model.SiteId < 1)
                {
                    model.SiteId = WSite.GetList()?.OrderBy(i => i.Rank).Select(i => i.Id).FirstOrDefault() ?? 0;
                }

                var site = model.SiteId > 0 ? WSite.Get(model.SiteId) : null;
                model.SiteName = site?.Name;

                var identities = model.SiteId > 0
                    ? WebSiteIdentity.Provider.GetList(model.SiteId)?.ToList() ?? new List<WebSiteIdentity>()
                    : WebSiteIdentity.Provider.GetList()?.ToList() ?? new List<WebSiteIdentity>();

                model.Identities = identities.Select(identity => new WebIdentityItem
                    {
                        Id = identity.Id,
                        DomainName = BuildDomain(identity),
                        Protocol = identity.ProtocolId == 1 ? "HTTPS" : "HTTP",
                        IsDefault = site != null && identity.Id == site.PrimaryIdentityId,
                        IsActive = true,
                        SslEnabled = identity.ProtocolId == 1,
                        ModifiedDate = null
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Failed to load identities: {ex.Message}";
            }

            return View(model);
        }

        private int ResolveSiteId(int siteId)
        {
            if (siteId > 0)
                return siteId;

            return DataUtil.GetId(Request, WebColumns.SiteId);
        }

        private static string BuildDomain(WebSiteIdentity identity)
        {
            var host = identity.HostName ?? string.Empty;
            if (identity.Port > 0 && identity.Port != 80 && identity.Port != 443)
                host = $"{host}:{identity.Port}";

            return $"{host}{identity.UrlPath}";
        }
    }

    public class WebIdentitiesViewModel
    {
        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public List<WebIdentityItem> Identities { get; set; } = new List<WebIdentityItem>();
        public string ErrorMessage { get; set; }
    }

    public class WebIdentityItem
    {
        public int Id { get; set; }
        public string DomainName { get; set; }
        public string Protocol { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public bool SslEnabled { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
