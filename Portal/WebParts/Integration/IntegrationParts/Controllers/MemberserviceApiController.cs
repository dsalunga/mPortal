using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.Integration
{
    /// <summary>
    /// Legacy compatibility stub for MemberService.asmx (SOAP member profile service).
    /// The legacy ASMX service provided SOAP endpoints for member profile operations.
    /// Modern replacement: member profile operations use WebUser REST APIs and ViewComponents.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MemberserviceApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(410, new { status = "gone", legacy = "MemberService.asmx", message = "This legacy SOAP endpoint has been retired. Use WebUser REST APIs and profile ViewComponents." });
        }
    }
}
