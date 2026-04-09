using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Event log viewer. Replaces EventLogManager.ascx (Central/Tools).
    /// Usage: @await Component.InvokeAsync("EventLogManager")
    /// </summary>
    public class EventLogManagerViewComponent : WViewComponent
    {
        public EventLogManagerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(string level = null, string search = null)
        {
            var model = new EventLogManagerViewModel
            {
                FilterLevel = level,
                SearchTerm = search,
                Entries = new List<EventLogEntry>(),
                AvailableLevels = new List<string> { "All", "Info", "Warning", "Error", "Critical" },
                CurrentPage = 1,
                PageSize = 50,
                TotalCount = 0
            };

            return View(model);
        }
    }

    public class EventLogManagerViewModel
    {
        public string FilterLevel { get; set; }
        public string SearchTerm { get; set; }
        public List<EventLogEntry> Entries { get; set; } = new List<EventLogEntry>();
        public List<string> AvailableLevels { get; set; } = new List<string>();
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;
        public string ErrorMessage { get; set; }
    }

    public class EventLogEntry
    {
        public long Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
        public string UserName { get; set; }
        public string IpAddress { get; set; }
    }
}
