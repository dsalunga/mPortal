using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.Api
{
    /// <summary>
    /// Legacy-compatible replacement for /Handlers/AjaxHandler.ashx.
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    public class LegacyAjaxHandlerController : ControllerBase
    {
        [HttpGet("/Handlers/AjaxHandler.ashx")]
        [HttpPost("/Handlers/AjaxHandler.ashx")]
        public IActionResult Handle([FromQuery(Name = "Method")] string method, [FromQuery(Name = "VarName")] string varName)
        {
            if (string.IsNullOrEmpty(method) && Request.HasFormContentType)
                method = Request.Form["Method"].ToString();

            if (string.IsNullOrEmpty(varName) && Request.HasFormContentType)
                varName = Request.Form["VarName"].ToString();

            switch (method)
            {
                case "Status":
                    if (!string.IsNullOrEmpty(varName))
                        return Content($"var {varName} = \"OK\";", "application/javascript");

                    return Content(string.Empty, "application/javascript");

                case "KeepAlive":
                    return Content("OK", "text/plain");

                default:
                    return Content(string.Empty, "text/plain");
            }
        }
    }
}
