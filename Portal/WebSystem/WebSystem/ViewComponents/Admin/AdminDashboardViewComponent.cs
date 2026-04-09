using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Central admin dashboard. Replaces AdminDashboard.ascx (Central root).
    /// Usage: @await Component.InvokeAsync("AdminDashboard")
    /// </summary>
    public class AdminDashboardViewComponent : WViewComponent
    {
        public AdminDashboardViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke()
        {
            var model = new AdminDashboardViewModel
            {
                RecentActivity = new List<DashboardActivityItem>(),
                QuickLinks = new List<DashboardQuickLink>()
            };

            return View(model);
        }
    }

    public class AdminDashboardViewModel
    {
        public int SiteCount { get; set; }
        public int PageCount { get; set; }
        public int UserCount { get; set; }
        public int TemplateCount { get; set; }
        public int WebPartCount { get; set; }
        public List<DashboardActivityItem> RecentActivity { get; set; } = new List<DashboardActivityItem>();
        public List<DashboardQuickLink> QuickLinks { get; set; } = new List<DashboardQuickLink>();
        public string ErrorMessage { get; set; }
    }

    public class DashboardActivityItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public DateTime ActivityDate { get; set; }
        public string ActivityType { get; set; }
    }

    public class DashboardQuickLink
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string IconClass { get; set; }
        public int BadgeCount { get; set; }
    }
}
