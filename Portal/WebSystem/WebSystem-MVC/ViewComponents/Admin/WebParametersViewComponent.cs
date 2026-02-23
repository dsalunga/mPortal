using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Parameter set manager. Replaces WebParameters.ascx (Central/Misc).
    /// Usage: @await Component.InvokeAsync("WebParameters")
    /// </summary>
    public class WebParametersViewComponent : WViewComponent
    {
        public WebParametersViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int selectedCategoryId = 0)
        {
            var model = new WebParametersViewModel
            {
                SelectedCategoryId = selectedCategoryId,
                Categories = new List<ParameterCategory>(),
                Parameters = new List<ParameterItem>()
            };

            return View(model);
        }
    }

    public class WebParametersViewModel
    {
        public int SelectedCategoryId { get; set; }
        public List<ParameterCategory> Categories { get; set; } = new List<ParameterCategory>();
        public List<ParameterItem> Parameters { get; set; } = new List<ParameterItem>();
        public string ErrorMessage { get; set; }
    }

    public class ParameterCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParameterCount { get; set; }
    }

    public class ParameterItem
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Category { get; set; }
        public string DataType { get; set; }
        public string Description { get; set; }
        public bool IsSystem { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
