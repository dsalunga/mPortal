using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Site search View Component. Replaces Search.ascx (SystemParts/Search).
    /// Usage: @await Component.InvokeAsync("Search")
    /// </summary>
    public class SearchViewComponent : WViewComponent
    {
        public SearchViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke()
        {
            var model = new SearchViewModel
            {
                Query = WcmsContext.Get("q"),
                BasePath = WcmsContext.BasePath
            };

            if (!string.IsNullOrEmpty(model.Query))
            {
                model.Results = new List<SearchResultItem>();

                // Search pages by title/content containing the query
                using (var reader = SqlHelper.ExecuteReader("WebPage_Search",
                    new Microsoft.Data.SqlClient.SqlParameter("@Query", model.Query),
                    new Microsoft.Data.SqlClient.SqlParameter("@SiteId", WcmsContext.Site?.Id ?? -1)))
                {
                    while (reader.Read())
                    {
                        model.Results.Add(new SearchResultItem
                        {
                            Title = reader["Title"]?.ToString(),
                            Url = reader["Url"]?.ToString(),
                            Description = reader["Description"]?.ToString()
                        });
                    }
                }
            }

            return View(model);
        }
    }

    public class SearchViewModel
    {
        public string Query { get; set; }
        public string BasePath { get; set; }
        public List<SearchResultItem> Results { get; set; }
    }

    public class SearchResultItem
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }
}
