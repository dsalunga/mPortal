using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Agent task editor. Replaces TaskEditor.ascx (Central/Agent).
    /// Usage: @await Component.InvokeAsync("TaskEditor")
    /// </summary>
    public class TaskEditorViewComponent : WViewComponent
    {
        public TaskEditorViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int taskId = 0)
        {
            var model = new TaskEditorViewModel
            {
                TaskId = taskId,
                AvailableTaskTypes = new List<string> { "SQL", "HTTP", "Email", "FileSync", "Custom" },
                AvailableSchedules = new List<string> { "Once", "Hourly", "Daily", "Weekly", "Monthly" }
            };

            return View(model);
        }
    }

    public class TaskEditorViewModel
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public string TaskType { get; set; }
        public string Schedule { get; set; }
        public string CommandText { get; set; }
        public bool IsEnabled { get; set; }
        public List<string> AvailableTaskTypes { get; set; } = new List<string>();
        public List<string> AvailableSchedules { get; set; } = new List<string>();
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
    }
}
