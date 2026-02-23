using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Data sync management. Replaces DataSyncDashboard.ascx (Central/Tools).
    /// Usage: @await Component.InvokeAsync("DataSyncDashboard")
    /// </summary>
    public class DataSyncDashboardViewComponent : WViewComponent
    {
        public DataSyncDashboardViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke()
        {
            var model = new DataSyncDashboardViewModel
            {
                SyncJobs = new List<DataSyncJobItem>()
            };

            return View(model);
        }
    }

    public class DataSyncDashboardViewModel
    {
        public List<DataSyncJobItem> SyncJobs { get; set; } = new List<DataSyncJobItem>();
        public string ErrorMessage { get; set; }
    }

    public class DataSyncJobItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SourceType { get; set; }
        public string TargetType { get; set; }
        public string Status { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime? LastSyncTime { get; set; }
        public DateTime? NextSyncTime { get; set; }
        public int RecordsSynced { get; set; }
        public string LastError { get; set; }
    }
}
