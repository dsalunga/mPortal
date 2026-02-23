using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Address book manager. Replaces WebAddresses.ascx (Central/Misc).
    /// Usage: @await Component.InvokeAsync("WebAddresses")
    /// </summary>
    public class WebAddressesViewComponent : WViewComponent
    {
        public WebAddressesViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int selectedGroupId = 0)
        {
            var model = new WebAddressesViewModel
            {
                SelectedGroupId = selectedGroupId,
                Groups = new List<AddressGroupItem>(),
                Addresses = new List<AddressItem>()
            };

            return View(model);
        }
    }

    public class WebAddressesViewModel
    {
        public int SelectedGroupId { get; set; }
        public List<AddressGroupItem> Groups { get; set; } = new List<AddressGroupItem>();
        public List<AddressItem> Addresses { get; set; } = new List<AddressItem>();
        public string ErrorMessage { get; set; }
    }

    public class AddressGroupItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AddressCount { get; set; }
    }

    public class AddressItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string GroupName { get; set; }
        public bool IsActive { get; set; }
    }
}
