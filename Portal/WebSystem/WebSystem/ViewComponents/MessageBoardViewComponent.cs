using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace WCMS.WebSystem.ViewComponents
{
    /// <summary>
    /// Shows a message board/wall for posting messages. Replaces MessageBoard.ascx (Common).
    /// Usage: @await Component.InvokeAsync("MessageBoard")
    /// </summary>
    public class MessageBoardViewComponent : WViewComponent
    {
        public MessageBoardViewComponent(IWContext context) : base(context) { }

        public IViewComponentResult Invoke()
        {
            var model = new MessageBoardViewModel
            {
                ObjectId = WcmsContext.ObjectId,
                RecordId = WcmsContext.RecordId,
                IsLoggedIn = WcmsSession.IsLoggedIn,
                CurrentUserName = WcmsSession.User?.FullName,
                CurrentUserId = WcmsSession.UserId
            };

            return View(model);
        }
    }

    public class MessageBoardViewModel
    {
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public bool IsLoggedIn { get; set; }
        public string CurrentUserName { get; set; }
        public int CurrentUserId { get; set; }
        public List<MessageBoardPost> Posts { get; set; } = new List<MessageBoardPost>();
    }

    public class MessageBoardPost
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public int AuthorUserId { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
