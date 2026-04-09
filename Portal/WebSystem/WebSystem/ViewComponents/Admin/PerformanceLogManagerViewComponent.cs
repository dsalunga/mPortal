using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Performance log viewer. Replaces PerformanceLogManager.ascx (Central/Tools).
    /// Usage: @await Component.InvokeAsync("PerformanceLogManager")
    /// </summary>
    public class PerformanceLogManagerViewComponent : WViewComponent
    {
        public PerformanceLogManagerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(string search = null)
        {
            var model = new PerformanceLogManagerViewModel
            {
                SearchTerm = search,
                Entries = new List<PerformanceLogEntry>(),
                CurrentPage = 1,
                PageSize = 50,
                TotalCount = 0
            };

            return View(model);
        }
    }

    public class PerformanceLogManagerViewModel
    {
        public string SearchTerm { get; set; }
        public List<PerformanceLogEntry> Entries { get; set; } = new List<PerformanceLogEntry>();
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;
        public double AverageResponseTime { get; set; }
        public double MaxResponseTime { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class PerformanceLogEntry
    {
        public long Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }
        public double ResponseTimeMs { get; set; }
        public int StatusCode { get; set; }
        public string UserName { get; set; }
        public string IpAddress { get; set; }
        public long MemoryUsageBytes { get; set; }
    }
}
