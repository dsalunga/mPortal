using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.Api
{
    /// <summary>
    /// Legacy-compatible redirects for removed Central WebForms user controls (.ascx).
    /// Preserves query string and redirects callers into modern /Central routes.
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    public class LegacyCentralControlRoutesController : ControllerBase
    {
        [HttpGet("/Content/Parts/Central/Security/UserActivities.ascx")]
        public IActionResult UserActivities()
        {
            return RedirectToCentral("/Central/Security/WebUsers/");
        }

        [HttpGet("/Content/Parts/Central/Controls/UserProfileForm.ascx")]
        public IActionResult UserProfileForm()
        {
            return RedirectToCentral("/Central/Security/UserProfile/");
        }

        [HttpGet("/Content/Parts/Central/Controls/UserRolesForm.ascx")]
        public IActionResult UserRolesForm()
        {
            return RedirectToCentral("/Central/Security/WebUserRoles/");
        }

        [HttpGet("/Content/Parts/Central/Misc/WebResourceManager.ascx")]
        public IActionResult WebResourceManager()
        {
            return RedirectToCentral("/Central/Tools/WebResourceManager/");
        }

        [HttpGet("/Content/Parts/Central/Tools/ImportExportHome.ascx")]
        [HttpGet("/Content/Parts/Central/Tools/ImportExportPage.ascx")]
        public IActionResult ImportExport()
        {
            return RedirectToCentral("/Central/Tools/WebDataStoreManager/");
        }

        [HttpGet("/Content/Parts/Central/Tools/ImportExportParameterSets.ascx")]
        public IActionResult ImportExportParameterSets()
        {
            return RedirectToCentral("/Central/Misc/WebParameterSets/");
        }

        [HttpGet("/Content/Parts/Central/Tools/MessageQueueManager.ascx")]
        public IActionResult MessageQueueManager()
        {
            return RedirectToCentral("/Central/Tools/MessageQueueManager/");
        }

        [HttpGet("/Content/Parts/Central/Tools/SmtpAnalyzer.ascx")]
        public IActionResult SmtpAnalyzer()
        {
            return RedirectToCentral("/Central/Tools/SmtpAnalyzer/");
        }

        [HttpGet("/Content/Parts/Central/WebOpen.ascx")]
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
