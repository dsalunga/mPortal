using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Agent task scheduler dashboard. Replaces AgentDashboard.ascx (Central/Agent).
    /// Usage: @await Component.InvokeAsync("AgentDashboard")
    /// </summary>
    public class AgentDashboardViewComponent : WViewComponent
    {
        public AgentDashboardViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke()
        {
            var model = new AgentDashboardViewModel
            {
                Tasks = new List<AgentTaskItem>()
            };

            return View(model);
        }
    }

    public class AgentDashboardViewModel
    {
        public List<AgentTaskItem> Tasks { get; set; } = new List<AgentTaskItem>();
        public bool IsAgentRunning { get; set; }
        public DateTime? LastRunTime { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class AgentTaskItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Schedule { get; set; }
        public string Status { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime? LastRunTime { get; set; }
        public DateTime? NextRunTime { get; set; }
        public string LastResult { get; set; }
    }
}
