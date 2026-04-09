using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Group management tree. Replaces WebGroups.ascx (Central/Security).
    /// Usage: @await Component.InvokeAsync("WebGroups")
    /// </summary>
    public class WebGroupsViewComponent : WViewComponent
    {
        public WebGroupsViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int selectedGroupId = 0)
        {
            var model = new WebGroupsViewModel
            {
                SelectedGroupId = selectedGroupId,
                Groups = new List<WebGroupNode>()
            };

            return View(model);
        }
    }

    public class WebGroupsViewModel
    {
        public int SelectedGroupId { get; set; }
        public List<WebGroupNode> Groups { get; set; } = new List<WebGroupNode>();
        public string ErrorMessage { get; set; }
    }

    public class WebGroupNode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ParentId { get; set; }
        public bool IsActive { get; set; }
        public int UserCount { get; set; }
        public List<WebGroupNode> Children { get; set; } = new List<WebGroupNode>();
    }
}
