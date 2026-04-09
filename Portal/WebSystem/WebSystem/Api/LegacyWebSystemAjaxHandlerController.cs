using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.Api
{
    /// <summary>
    /// Legacy-compatible replacement for /Content/Handlers/AjaxHandler.ashx.
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    public class LegacyWebSystemAjaxHandlerController : ControllerBase
    {
        [HttpGet("/Content/Handlers/AjaxHandler.ashx")]
        [HttpPost("/Content/Handlers/AjaxHandler.ashx")]
        public IActionResult Handle([FromQuery(Name = "Method")] string method)
        {
            method = GetValue("Method", method);

            switch (method)
            {
                case "SetDesignPanel":
                    SetDesignPanel();
                    return Content(string.Empty, "text/plain");

                case "GetText":
                    return Content(GetPageText(), "text/plain");

                case "Status":
                case "KeepAlive":
                    TouchHeartbeat();
                    return Content("OK", "text/plain");

                case "SessionValid":
                    return Content(IsSessionValid() ? "1" : "0", "text/plain");

                default:
                    return Content(string.Empty, "text/plain");
            }
        }

        private void SetDesignPanel()
        {
            var expanded = GetValue("Expanded");
            var left = GetValue("Left");
            var top = GetValue("Top");
            var init = GetValue("Init");

            if (!string.IsNullOrEmpty(expanded))
                WSession.Current.InDesignPanelExpanded = expanded == "1";

            if (!string.IsNullOrEmpty(left))
                WSession.Current.InDesignPanelLeft = DataUtil.GetInt32(left);

            if (!string.IsNullOrEmpty(top))
                WSession.Current.InDesignPanelTop = DataUtil.GetInt32(top);

            if (!string.IsNullOrEmpty(init))
                WSession.Current.IsDesignInitiated = init == "1";
        }

        private string GetPageText()
        {
            var pageId = DataUtil.GetId(Request, "PageId");
            var pageUrl = GetValue("Url");

            WPage page = null;
            if (pageId > 0)
                page = WPage.Get(pageId);
            else if (!string.IsNullOrEmpty(pageUrl))
                page = WebRewriter.ResolvePage(pageUrl);

            return page?.Name ?? string.Empty;
        }

        private void TouchHeartbeat()
        {
            HttpContext.Session.SetString("Heartbeat", DateTime.UtcNow.ToString("O"));
        }

        private bool IsSessionValid()
        {
            var userSession = WSession.Current.UserSession;
            if (userSession == null)
                return false;

            TouchHeartbeat();
            return true;
        }

        private string GetValue(string key, string queryValue = null)
        {
            if (!string.IsNullOrEmpty(queryValue))
                return queryValue;

            var query = Request.Query[key].ToString();
            if (!string.IsNullOrEmpty(query))
                return query;

            if (Request.HasFormContentType)
                return Request.Form[key].ToString();

            return string.Empty;
        }
    }
}
