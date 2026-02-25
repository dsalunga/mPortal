using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// UI for triggering background tasks/jobs. Replaces TriggerTask.ascx (Common).
    /// Usage: @await Component.InvokeAsync("TriggerTask")
    /// </summary>
    public class TriggerTaskViewComponent : WViewComponent
    {
        public TriggerTaskViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke()
        {
            var model = new TriggerTaskViewModel
            {
                IsAdministrator = WcmsSession.IsAdministrator
            };

            if (model.IsAdministrator && WebJob.Provider != null)
            {
                model.Jobs = WebJob.Provider.GetList()
                    .OrderBy(j => j.Name)
                    .ToList();
            }

            return View(model);
        }
    }

    public class TriggerTaskViewModel
    {
        public bool IsAdministrator { get; set; }
        public List<WebJob> Jobs { get; set; } = new List<WebJob>();
        public string StatusMessage { get; set; }
    }
}
