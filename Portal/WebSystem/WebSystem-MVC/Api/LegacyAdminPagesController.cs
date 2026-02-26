using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.Api
{
    /// <summary>
    /// Legacy-compatible redirects for removed WebForms admin pages.
    /// Preserves query string and redirects callers into modern /Central routes.
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    public class LegacyAdminPagesController : ControllerBase
    {
        [HttpGet("/Content/Admin/WebBinding.aspx")]
        [HttpGet("/Content/Admin/WebBindings.aspx")]
        public IActionResult WebBindings()
        {
            return RedirectToCentral("/Central/Site/WebSiteHeaders/");
        }

        [HttpGet("/Content/Admin/WebEvents.aspx")]
        public IActionResult WebEvents()
        {
            return RedirectToCentral("/Central/Dashboard/");
        }

        [HttpGet("/Content/Admin/WebLogs.aspx")]
        public IActionResult WebLogs()
        {
            return RedirectToCentral("/Central/Tools/SessionDiagnostics/");
        }

        [HttpGet("/Content/Admin/WebOpen.aspx")]
        public IActionResult WebOpen()
        {
            return RedirectToCentral("/Central/WebOpen/");
        }

        private IActionResult RedirectToCentral(string targetPath)
        {
            var query = Request.QueryString.HasValue ? Request.QueryString.Value : string.Empty;
            return Redirect($"{targetPath}{query}");
        }
    }
}
