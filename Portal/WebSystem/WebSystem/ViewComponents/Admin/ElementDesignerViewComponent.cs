using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Page element configurator. Replaces ElementDesigner.ascx (Central/Controls).
    /// Usage: @await Component.InvokeAsync("ElementDesigner")
    /// </summary>
    public class ElementDesignerViewComponent : WViewComponent
    {
        public ElementDesignerViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int elementId = 0)
        {
            var model = new ElementDesignerViewModel
            {
                ElementId = elementId,
                AvailableElementTypes = new List<ElementTypeInfo>(),
                Parameters = new List<ElementParameter>()
            };

            return View(model);
        }
    }

    public class ElementDesignerViewModel
    {
        public int ElementId { get; set; }
        public string ElementName { get; set; }
        public string ElementType { get; set; }
        public int PanelId { get; set; }
        public int SortOrder { get; set; }
        public bool IsVisible { get; set; } = true;
        public List<ElementTypeInfo> AvailableElementTypes { get; set; } = new List<ElementTypeInfo>();
        public List<ElementParameter> Parameters { get; set; } = new List<ElementParameter>();
        public string ErrorMessage { get; set; }
    }

    public class ElementTypeInfo
    {
        public string TypeName { get; set; }
        public string DisplayName { get; set; }
        public string Category { get; set; }
    }

    public class ElementParameter
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
        public string InputType { get; set; }
    }
}
