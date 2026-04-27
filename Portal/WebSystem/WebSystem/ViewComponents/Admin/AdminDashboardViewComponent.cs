using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Diagnostics;
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

            try
            {
                var sites = WSite.GetList()?.ToList() ?? new List<WSite>();
                model.SiteCount = sites.Count(i => i.Active == WConstants.Active);

                var pageIds = new HashSet<int>();
                foreach (var site in sites)
                {
                    var pages = WPage.GetList(site.Id);
                    if (pages == null)
                        continue;

                    foreach (var page in pages)
                    {
                        if (page != null && page.Active == WConstants.Active)
                            pageIds.Add(page.Id);
                    }
                }
                model.PageCount = pageIds.Count;

                model.UserCount = (WebUser.GetList() ?? Enumerable.Empty<WebUser>())
                    .Count(i => i.IsActive);

                model.TemplateCount = (WebTemplate.Provider.GetList() ?? Enumerable.Empty<WebTemplate>())
                    .Count();

                model.WebPartCount = (WPart.GetList() ?? Enumerable.Empty<WPart>())
                    .Count(i => i.IsActive);

                model.QuickLinks = BuildQuickLinks(model);
                model.RecentActivity = LoadRecentActivity();
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Dashboard data is temporarily unavailable: " + ex.Message;
            }

            return View(model);
        }

        private static List<DashboardQuickLink> BuildQuickLinks(AdminDashboardViewModel model)
        {
            var links = new List<DashboardQuickLink>
            {
                new DashboardQuickLink
                {
                    Name = "Web Sites",
                    Url = CentralPages.WebSites,
                    IconClass = "bi-globe",
                    BadgeCount = model.SiteCount
                },
                new DashboardQuickLink
                {
                    Name = "Web Pages",
                    Url = CentralPages.WebPages,
                    IconClass = "bi-file-earmark",
                    BadgeCount = model.PageCount
                },
                new DashboardQuickLink
                {
                    Name = "Users",
                    Url = CentralPages.WebUsers,
                    IconClass = "bi-people",
                    BadgeCount = model.UserCount
                },
                new DashboardQuickLink
                {
                    Name = "Templates",
                    Url = CentralPages.WebTemplates,
                    IconClass = "bi-layout-text-window",
                    BadgeCount = model.TemplateCount
                },
                new DashboardQuickLink
                {
                    Name = "Applications",
                    Url = CentralPages.WebParts,
                    IconClass = "bi-puzzle",
                    BadgeCount = model.WebPartCount
                },
                new DashboardQuickLink
                {
                    Name = "Registry",
                    Url = CentralPages.WebRegistry,
                    IconClass = "bi-list-check"
                }
            };

            return links;
        }

        private static List<DashboardActivityItem> LoadRecentActivity()
        {
            try
            {
                var events = EventLog.Provider.GetList();
                if (events == null)
                    return new List<DashboardActivityItem>();

                return events
                    .OrderByDescending(i => i.EventDate)
                    .Take(10)
                    .Select(i => new DashboardActivityItem
                    {
                        Id = i.Id,
                        Description = string.IsNullOrWhiteSpace(i.Content) ? i.EventName : i.Content,
                        UserName = i.User?.UserName ?? "system",
                        ActivityDate = i.EventDate,
                        ActivityType = string.IsNullOrWhiteSpace(i.EventName) ? "Event" : i.EventName
                    })
                    .ToList();
            }
            catch
            {
                return new List<DashboardActivityItem>();
            }
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
