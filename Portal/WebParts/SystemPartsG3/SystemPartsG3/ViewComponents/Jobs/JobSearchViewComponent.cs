using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Job listing search with filters.
    /// Replaces JobSearch.ascx (SystemPartsG3/Jobs).
    /// Usage: @await Component.InvokeAsync("JobSearch", new { objectId })
    /// </summary>
    public class JobSearchViewComponent : WViewComponent
    {
        public JobSearchViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
            }

            var model = new JobSearchViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                SearchKeyword = string.Empty,
                SearchLocation = string.Empty,
                SearchCategory = string.Empty,
                Categories = new List<string>(),
                Results = new List<JobSearchResultItem>(),
                TotalResults = 0,
                CurrentPage = 1,
                PageSize = 10
            };

            return View("~/Views/Shared/Components/Jobs/JobSearch/Default.cshtml", model);
        }
    }

    public class JobSearchViewModel
    {
        public int ObjectId { get; set; }
        public string SearchKeyword { get; set; }
        public string SearchLocation { get; set; }
        public string SearchCategory { get; set; }
        public List<string> Categories { get; set; }
        public List<JobSearchResultItem> Results { get; set; }
        public int TotalResults { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }

    public class JobSearchResultItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string DatePosted { get; set; }
    }
}
