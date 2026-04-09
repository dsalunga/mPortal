using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Utilities;

namespace WCMS.WebSystem.Api
{
    /// <summary>
    /// Replaces the legacy ASMX FrameworkService (FxService.asmx).
    /// Login endpoint is public; other endpoints require authentication.
    /// </summary>
    [ApiController]
    [Route("api/framework")]
    [Authorize]
    public class FrameworkApiController : ControllerBase
    {
        private readonly IWSession _wSession;

        public FrameworkApiController(IWSession wSession)
        {
            _wSession = wSession;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (!string.IsNullOrEmpty(request.UserName) && !string.IsNullOrEmpty(request.Password))
            {
                var user = AccountHelper.ValidateLogin(request.UserName, request.Password);
                if (user != null && user.IsActive)
                {
                    if (request.LoginSession)
                        _wSession.Login(user.Id, true);

                    return Ok(new WSUserInfo(user));
                }
            }
            else if (_wSession.IsLoggedIn)
            {
                return Ok(new WSUserInfo(_wSession.User));
            }

            return Ok((WSUserInfo)null);
        }

        [HttpPost("logoff")]
        public IActionResult LogOff()
        {
            WSession.LogOff();
            return Ok(true);
        }

        [HttpPost("comment")]
        public IActionResult PostComment([FromBody] PostCommentRequest request)
        {
            var poster = WebUser.Get(request.UserId);
            if (poster != null)
            {
                var commentDate = DateTime.Now;
                var comment = new WebComment();
                comment.ObjectId = request.TargetObjectId;
                comment.RecordId = request.TargetRecordId;
                comment.Content = request.CommentText;
                comment.DateCreated = commentDate;
                comment.UserId = request.UserId;
                comment.Update();

                return Ok(new
                {
                    commentId = comment.Id,
                    posterId = request.UserId,
                    posterName = poster.FirstAndLastName,
                    posterImage = poster.GetPhotoPath("37x37"),
                    message = request.CommentText,
                    datePosted = commentDate
                });
            }

            return Ok((object)null);
        }

        [HttpDelete("comment/{id:int}")]
        public IActionResult DeleteComment(int id)
        {
            if (id > 0)
            {
                var comment = WebComment.Provider.Get(id);
                if (comment != null)
                    return Ok(comment.Delete());
            }

            return Ok(false);
        }
    }

    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool LoginSession { get; set; }
    }

    public class PostCommentRequest
    {
        public string CommentText { get; set; }
        public int UserId { get; set; }
        public int TargetObjectId { get; set; }
        public int TargetRecordId { get; set; }
    }
}
