using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Job listings display.
    /// Replaces JobsList.ascx (SystemPartsG3/Jobs).
    /// Usage: @await Component.InvokeAsync("JobsList", new { objectId })
    /// </summary>
    public class JobsListViewComponent : WViewComponent
    {
        public JobsListViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new JobsListViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                Jobs = new List<JobListItem>(),
                TotalJobs = 0,
                CurrentPage = 1,
                PageSize = 10
            };

            return View("~/Views/Shared/Components/Jobs/JobsList/Default.cshtml", model);
        }
    }

    public class JobsListViewModel
    {
        public int ObjectId { get; set; }
        public List<JobListItem> Jobs { get; set; }
        public int TotalJobs { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }

    public class JobListItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string DatePosted { get; set; }
        public string DetailsUrl { get; set; }
    }
}
