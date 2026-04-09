using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
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
                SiteId = siteId,
                Identities = new List<WebIdentityItem>()
            };

            return View(model);
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
