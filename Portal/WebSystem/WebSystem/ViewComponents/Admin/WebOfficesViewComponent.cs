using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Office/location manager. Replaces WebOffices.ascx (Central/Misc).
    /// Usage: @await Component.InvokeAsync("WebOffices")
    /// </summary>
    public class WebOfficesViewComponent : WViewComponent
    {
        public WebOfficesViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int selectedOfficeId = 0)
        {
            var model = new WebOfficesViewModel
            {
                SelectedOfficeId = selectedOfficeId,
                Offices = new List<OfficeItem>()
            };

            return View(model);
        }
    }

    public class WebOfficesViewModel
    {
        public int SelectedOfficeId { get; set; }
        public List<OfficeItem> Offices { get; set; } = new List<OfficeItem>();
        public string ErrorMessage { get; set; }
    }

    public class OfficeItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public bool IsHeadquarters { get; set; }
    }
}
