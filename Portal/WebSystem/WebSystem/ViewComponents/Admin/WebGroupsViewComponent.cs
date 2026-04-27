using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
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
                SelectedGroupId = ResolveSelectedGroupId(selectedGroupId),
                Groups = new List<WebGroupNode>()
            };

            try
            {
                var groups = WebGroup.GetList()?.OrderBy(i => i.Name).ToList() ?? new List<WebGroup>();
                model.Groups = BuildTree(groups, -1);

                if (model.SelectedGroupId < 1 && model.Groups.Count > 0)
                    model.SelectedGroupId = model.Groups[0].Id;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Failed to load groups: {ex.Message}";
            }

            return View(model);
        }

        private int ResolveSelectedGroupId(int selectedGroupId)
        {
            if (selectedGroupId > 0)
                return selectedGroupId;

            return DataUtil.GetId(Request, WebColumns.GroupId);
        }

        private List<WebGroupNode> BuildTree(List<WebGroup> allGroups, int parentId)
        {
            return allGroups
                .Where(group => group.ParentId == parentId)
                .OrderBy(group => group.Name)
                .Select(group =>
                {
                    int userCount;
                    try
                    {
                        userCount = group.Users.Count();
                    }
                    catch
                    {
                        userCount = 0;
                    }

                    return new WebGroupNode
                    {
                        Id = group.Id,
                        Name = group.Name,
                        Description = group.Description,
                        ParentId = group.ParentId,
                        IsActive = true,
                        UserCount = userCount,
                        Children = BuildTree(allGroups, group.Id)
                    };
                })
                .ToList();
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
