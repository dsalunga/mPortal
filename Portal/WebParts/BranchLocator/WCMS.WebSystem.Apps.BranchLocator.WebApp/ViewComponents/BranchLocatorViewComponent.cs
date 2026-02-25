using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using WCMS.WebSystem.Apps.BranchLocator;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Branch/chapter locator search UI.
    /// Replaces _Dashboard.cshtml and _SearchResults.cshtml (BranchLocator).
    /// Usage: @await Component.InvokeAsync("BranchLocator")
    /// </summary>
    public class BranchLocatorViewComponent : WViewComponent
    {
        public BranchLocatorViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke()
        {
            var model = new BranchLocatorViewModel();

            var chapterParents = MChapter.Provider.GetList(-1);
            if (chapterParents != null)
            {
                model.ChapterParents = chapterParents.ToList();
            }

            var loggedIn = WcmsSession.IsLoggedIn;
            var isAdmin = WcmsSession.IsAdministrator;
            var keyword = WcmsContext.Get("search");
            var lat = WcmsContext.Get("lat");
            var lng = WcmsContext.Get("lng");

            model.SearchKeyword = keyword;
            model.BasePath = WcmsContext.BasePath;

            if (!string.IsNullOrEmpty(lat) && !string.IsNullOrEmpty(lng))
            {
                model.Latitude = double.Parse(lat);
                model.Longitude = double.Parse(lng);
                model.HasLocation = true;

                var chapterType = (int)ChapterTypes.Regular;
                model.NearbyChapters = (from c in MChapter.Provider.GetList()
                                        where c.ChapterType == chapterType
                                            && FALHelper.IsAllowed(c, loggedIn, isAdmin)
                                            && !(c.Latitude == 0 && c.Longitude == 0)
                                        orderby Math.Sqrt(
                                            Math.Pow(69.1 * (c.Latitude - model.Latitude), 2) +
                                            Math.Pow(69.1 * (model.Longitude - c.Longitude) * Math.Cos(c.Latitude / 57.3), 2))
                                        select c).Take(10).ToList();
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                var chapterType = (int)ChapterTypes.Regular;
                model.SearchResults = (from c in MChapter.Provider.GetList()
                                       where FALHelper.IsAllowed(c, loggedIn, isAdmin)
                                           && (DataHelper.HasMatch(c.Name, keyword)
                                            || DataHelper.HasMatch(c.Address, keyword)
                                            || DataHelper.HasMatch(c.Email, keyword)
                                            || DataHelper.HasMatch(c.Telephone, keyword)
                                            || DataHelper.HasMatch(c.MoreInfo, keyword))
                                           && c.ChapterType == chapterType
                                       select c).ToList();
            }

            return View(model);
        }
    }

    public class BranchLocatorViewModel
    {
        public string SearchKeyword { get; set; }
        public string BasePath { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool HasLocation { get; set; }
        public List<MChapter> ChapterParents { get; set; } = new List<MChapter>();
        public List<MChapter> NearbyChapters { get; set; } = new List<MChapter>();
        public List<MChapter> SearchResults { get; set; }
    }
}
