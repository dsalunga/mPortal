using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Utilities;

namespace WCMS.WebSystem.Api
{
    /// <summary>
    /// Legacy-compatible replacement for /Content/Parts/Common/FxService.asmx.
    /// Returns ASP.NET AJAX style payloads: { d: ... }.
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    public class LegacyFxServiceController : ControllerBase
    {
        [HttpGet("/Content/Parts/Common/FxService.asmx/HelloWorld")]
        [HttpPost("/Content/Parts/Common/FxService.asmx/HelloWorld")]
        public IActionResult HelloWorld()
        {
            return new JsonResult(new { d = "Hello World" });
        }

        [HttpGet("/Content/Parts/Common/FxService.asmx/Login")]
        [HttpPost("/Content/Parts/Common/FxService.asmx/Login")]
        public IActionResult Login([FromQuery] string userName, [FromQuery] string password, [FromQuery] bool loginSession, [FromBody] FxLoginRequest request)
        {
            var effectiveUserName = GetValue("userName", userName, request?.UserName);
            var effectivePassword = GetValue("password", password, request?.Password);
            var effectiveLoginSession = loginSession || request?.LoginSession == true || GetValue("loginSession") == "true";

            WSUserInfo result = null;
            if (!string.IsNullOrEmpty(effectiveUserName) && !string.IsNullOrEmpty(effectivePassword))
            {
                var user = AccountHelper.ValidateLogin(effectiveUserName, effectivePassword);
                if (user != null && user.IsActive)
                {
                    if (effectiveLoginSession)
                        WSession.Current.Login(user.Id, true);

                    result = new WSUserInfo(user);
                }
            }
            else if (WSession.Current.IsLoggedIn)
            {
                result = new WSUserInfo(WSession.Current.User);
            }

            return new JsonResult(new { d = result });
        }

        [HttpGet("/Content/Parts/Common/FxService.asmx/LogOff")]
        [HttpPost("/Content/Parts/Common/FxService.asmx/LogOff")]
        public IActionResult LogOff()
        {
            WSession.LogOff();
            return new JsonResult(new { d = true });
        }

        [HttpPost("/Content/Parts/Common/FxService.asmx/PostComment")]
        public IActionResult PostComment([FromBody] FxPostCommentRequest request)
        {
            var commentText = request?.CommentText ?? GetValue("commentText");
            var userId = request?.UserId > 0 ? request.UserId : DataUtil.GetInt32(GetValue("userId"));
            var targetObjectId = request?.TargetObjectId > 0 ? request.TargetObjectId : DataUtil.GetInt32(GetValue("targetObjectId"));
            var targetRecordId = request?.TargetRecordId > 0 ? request.TargetRecordId : DataUtil.GetInt32(GetValue("targetRecordId"));

            var poster = WebUser.Get(userId);
            if (poster == null)
                return new JsonResult(new { d = (string)null });

            var commentDate = DateTime.Now;
            var comment = new WebComment
            {
                ObjectId = targetObjectId,
                RecordId = targetRecordId,
                Content = commentText,
                DateCreated = commentDate,
                UserId = userId
            };
            comment.Update();

            var templatePath = PathMapper.MapPath("~/Content/Parts/Common/Templates/CommentItem.template.htm");
            var template = FileHelper.ReadFile(templatePath);

            var values = new NamedValueProvider();
            values.Add("POSTER_USER_ID", userId);
            values.Add("DELETE_PANEL", "<div class=\"commentDeleteHandle\" style=\"float: right; padding: 5px\"><a class=\"commentDeleteButton\"></a></div>");
            values.Add("COMMENT_ID", comment.Id);
            values.Add("COMMENT_MESSAGE", commentText);
            values.Add("POSTER_NAME", poster.FirstAndLastName);
            values.Add("DATE_POSTED", string.Format("{0}{1}", commentDate.ToString("MMMM d a\t hh:mm"), commentDate.ToString("tt").ToLowerInvariant()));
            values.Add("POSTER_IMAGE_URL", poster.GetPhotoPath("37x37"));

            var html = Substituter.Substitute(template, values);
            return new JsonResult(new { d = html });
        }

        [HttpGet("/Content/Parts/Common/FxService.asmx/DeleteComment")]
        [HttpPost("/Content/Parts/Common/FxService.asmx/DeleteComment")]
        public IActionResult DeleteComment([FromQuery] int id, [FromBody] FxDeleteCommentRequest request)
        {
            var effectiveId = id > 0 ? id : request?.Id ?? DataUtil.GetInt32(GetValue("id"));
            var deleted = false;

            if (effectiveId > 0)
            {
                var comment = WebComment.Provider.Get(effectiveId);
                if (comment != null)
                    deleted = comment.Delete();
            }

            return new JsonResult(new { d = deleted });
        }

        private string GetValue(string key, params string[] directValues)
        {
            if (directValues != null)
            {
                foreach (var value in directValues)
                {
                    if (!string.IsNullOrEmpty(value))
                        return value;
                }
            }

            var query = Request.Query[key].ToString();
            if (!string.IsNullOrEmpty(query))
                return query;

            if (Request.HasFormContentType)
                return Request.Form[key].ToString();

            return string.Empty;
        }
    }

    public class FxLoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool LoginSession { get; set; }
    }

    public class FxPostCommentRequest
    {
        public string CommentText { get; set; }
        public int UserId { get; set; }
        public int TargetObjectId { get; set; }
        public int TargetRecordId { get; set; }
    }

    public class FxDeleteCommentRequest
    {
        public int Id { get; set; }
    }
}
