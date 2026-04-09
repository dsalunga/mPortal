using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// SQL query analyzer tool. Replaces QueryAnalyzer.ascx (Central/Tools).
    /// Usage: @await Component.InvokeAsync("QueryAnalyzer")
    /// </summary>
    public class QueryAnalyzerViewComponent : WViewComponent
    {
        public QueryAnalyzerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke()
        {
            var model = new QueryAnalyzerViewModel
            {
                ResultColumns = new List<string>(),
                ResultRows = new List<List<string>>()
            };

            return View(model);
        }
    }

    public class QueryAnalyzerViewModel
    {
        public string QueryText { get; set; }
        public List<string> ResultColumns { get; set; } = new List<string>();
        public List<List<string>> ResultRows { get; set; } = new List<List<string>>();
        public int RowsAffected { get; set; }
        public string ExecutionTime { get; set; }
        public bool HasResults { get; set; }
        public string ErrorMessage { get; set; }
    }
}
