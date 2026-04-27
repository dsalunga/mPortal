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
    /// User list with search, group filter, CRUD actions. Replaces WebUsers.ascx (Central/Security).
    /// Usage: @await Component.InvokeAsync("WebUsers")
    /// </summary>
    public class WebUsersViewComponent : WViewComponent
    {
        public WebUsersViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke(int groupId = 0, string search = null)
        {
            var resolvedGroupId = ResolveGroupId(groupId);
            var resolvedSearch = string.IsNullOrWhiteSpace(search) ? Request.Query["Search"].ToString() : search;
            var pageSize = ResolvePageSize();
            var currentPage = ResolveCurrentPage();

            var model = new WebUsersViewModel
            {
                GroupId = resolvedGroupId,
                SearchTerm = resolvedSearch,
                Users = new List<WebUserItem>(),
                Groups = new List<WebUserGroupItem>(),
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = 0
            };

            try
            {
                model.Groups = WebGroup.GetList()
                    .OrderBy(i => i.Name)
                    .Select(group => new WebUserGroupItem
                    {
                        Id = group.Id,
                        Name = group.Name
                    })
                    .ToList();

                var users = model.GroupId > 0
                    ? WebUser.GetList(model.GroupId).ToList()
                    : WebUser.GetList().ToList();

                if (!string.IsNullOrWhiteSpace(model.SearchTerm))
                {
                    var term = model.SearchTerm.Trim();
                    users = users.Where(user =>
                            Contains(user.UserName, term) ||
                            Contains(user.FirstName, term) ||
                            Contains(user.LastName, term) ||
                            Contains(user.Email, term))
                        .ToList();
                }

                users = users.OrderBy(user => user.UserName).ToList();
                model.TotalCount = users.Count;

                var skip = (model.CurrentPage - 1) * model.PageSize;
                if (skip >= model.TotalCount)
                {
                    model.CurrentPage = 1;
                    skip = 0;
                }

                model.Users = users
                    .Skip(skip)
                    .Take(model.PageSize)
                    .Select(user => new WebUserItem
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        IsActive = user.IsActive,
                        IsLockedOut = user.IsLoginLockedOut,
                        LastLoginDate = user.HaveNotLoggedIn ? null : user.LastLogin
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                model.ErrorMessage = $"Failed to load users: {ex.Message}";
            }

            return View(model);
        }

        private int ResolveGroupId(int groupId)
        {
            if (groupId > 0)
                return groupId;

            return DataUtil.GetId(Request, WebColumns.GroupId);
        }

        private int ResolveCurrentPage()
        {
            var page = DataUtil.GetInt32(Request, "Page", 1);
            return page < 1 ? 1 : page;
        }

        private int ResolvePageSize()
        {
            var pageSize = DataUtil.GetInt32(Request, "PageSize", 25);
            if (pageSize < 1)
                return 25;

            return pageSize > 200 ? 200 : pageSize;
        }

        private static bool Contains(string value, string term)
        {
            return !string.IsNullOrEmpty(value) && value.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }

    public class WebUsersViewModel
    {
        public int GroupId { get; set; }
        public string SearchTerm { get; set; }
        public List<WebUserItem> Users { get; set; } = new List<WebUserItem>();
        public List<WebUserGroupItem> Groups { get; set; } = new List<WebUserGroupItem>();
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;
        public string ErrorMessage { get; set; }
    }

    public class WebUserItem
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsLockedOut { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }

    public class WebUserGroupItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
