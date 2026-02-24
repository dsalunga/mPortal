using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;
using WCMS.WebSystem.Apps.BranchLocator;

namespace WCMS.WebSystem.WebParts.ViewComponents
{
    /// <summary>
    /// Map view for displaying branch/chapter locations.
    /// Renders an interactive map with markers for each chapter.
    /// Usage: @await Component.InvokeAsync("BranchMap")
    /// </summary>
    public class BranchMapViewComponent : WViewComponent
    {
        public BranchMapViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke()
        {
            var model = new BranchMapViewModel();

            var loggedIn = WcmsSession.IsLoggedIn;
            var isAdmin = WcmsSession.IsAdministrator;
            var lat = WcmsContext.Get("lat");
            var lng = WcmsContext.Get("lng");

            model.BasePath = WcmsContext.BasePath;

            if (!string.IsNullOrEmpty(lat) && !string.IsNullOrEmpty(lng))
            {
                model.CenterLatitude = double.Parse(lat);
                model.CenterLongitude = double.Parse(lng);
                model.HasCenter = true;

                var chapterType = (int)ChapterTypes.Regular;
                model.MapMarkers = (from c in MChapter.Provider.GetList()
                                    where c.ChapterType == chapterType
                                        && FALHelper.IsAllowed(c, loggedIn, isAdmin)
                                        && !(c.Latitude == 0 && c.Longitude == 0)
                                    orderby Math.Sqrt(
                                        Math.Pow(69.1 * (c.Latitude - model.CenterLatitude), 2) +
                                        Math.Pow(69.1 * (model.CenterLongitude - c.Longitude) * Math.Cos(c.Latitude / 57.3), 2))
                                    select new BranchMapMarker
                                    {
                                        Id = c.Id,
                                        Name = c.Name,
                                        Address = string.IsNullOrEmpty(c.Address) ? "Please contact the assigned coordinator." : c.Address,
                                        Latitude = c.Latitude,
                                        Longitude = c.Longitude
                                    }).Take(10).ToList();
            }

            return View(model);
        }
    }

    public class BranchMapViewModel
    {
        public string BasePath { get; set; }
        public double CenterLatitude { get; set; }
        public double CenterLongitude { get; set; }
        public bool HasCenter { get; set; }
        public List<BranchMapMarker> MapMarkers { get; set; } = new List<BranchMapMarker>();
    }

    public class BranchMapMarker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
