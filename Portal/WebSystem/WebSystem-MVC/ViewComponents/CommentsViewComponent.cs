using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Displays and allows posting comments on CMS objects. Replaces Comments.ascx (Common).
    /// Usage: @await Component.InvokeAsync("Comments")
    /// </summary>
    public class CommentsViewComponent : WViewComponent
    {
        public CommentsViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke()
        {
            var objectId = WcmsContext.ObjectId;
            var recordId = WcmsContext.RecordId;

            var model = new CommentsViewModel
            {
                ObjectId = objectId,
                RecordId = recordId,
                IsLoggedIn = WcmsSession.IsLoggedIn,
                CurrentUserName = WcmsSession.User?.FullName,
                CurrentUserId = WcmsSession.UserId
            };

            if (WebComment.Provider != null)
            {
                model.Comments = WebComment.Provider.GetList(
                    objectId: objectId, recordId: recordId)
                    .OrderByDescending(c => c.DateCreated)
                    .ToList();
            }

            return View(model);
        }
    }

    public class CommentsViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public bool IsLoggedIn { get; set; }
        public string CurrentUserName { get; set; }
        public int CurrentUserId { get; set; }
        public List<WebComment> Comments { get; set; } = new List<WebComment>();
    }
}
