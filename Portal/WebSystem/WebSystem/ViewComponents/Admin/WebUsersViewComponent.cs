using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
            var model = new WebUsersViewModel
            {
                GroupId = groupId,
                SearchTerm = search,
                Users = new List<WebUserItem>(),
                Groups = new List<WebUserGroupItem>(),
                CurrentPage = 1,
                PageSize = 25,
                TotalCount = 0
            };

            return View(model);
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
