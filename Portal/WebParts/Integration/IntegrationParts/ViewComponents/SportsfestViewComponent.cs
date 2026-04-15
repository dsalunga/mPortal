using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

using System.Collections.Generic;
namespace WCMS.WebSystem.WebParts.Integration.ViewComponents
{
    /// <summary>
    /// Ported from Sportsfest.ascx (Apps/Integration/Profile).
    /// </summary>
    public class SportsfestViewComponent : WViewComponent
    {
        public SportsfestViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {
            if (objectId > 0)
            {
                WcmsContext.Set("ObjectId", objectId.ToString());
                WcmsContext.Set("RecordId", recordId.ToString());
            }

            var model = new SportsfestViewModel
            {
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId
            };

            return View(model);
        }
    }

        public class SportsfestViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public bool Agree1 { get; set; } = false;
        public bool Agree2 { get; set; } = false;
        public List<SelectOption> CboAgeOptions { get; set; } = new();
        public List<SelectOption> CboGroupColorOptions { get; set; } = new();
        public List<SelectOption> CboShirtSizeOptions { get; set; } = new();
        public string Locale { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public List<SelectOption> ObjectDataSourceCountriesOptions { get; set; } = new();
        public List<SelectOption> RblSet1Options { get; set; } = new();
        public List<SelectOption> RblSet2Options { get; set; } = new();
        public string SelectedCboAge { get; set; } = string.Empty;
        public string SelectedCboGroupColor { get; set; } = string.Empty;
        public string SelectedCboShirtSize { get; set; } = string.Empty;
        public string SelectedObjectDataSourceCountries { get; set; } = string.Empty;
        public string SelectedRblSet1 { get; set; } = string.Empty;
        public string SelectedRblSet2 { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
        public string Suggestion { get; set; } = string.Empty;
    }
    }
