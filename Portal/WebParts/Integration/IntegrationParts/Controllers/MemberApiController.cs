using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.Integration
{
    /// <summary>
    /// Legacy compatibility stub for Member.asmx (SOAP member service).
    /// The legacy ASMX service provided SOAP endpoints for member data operations.
    /// Modern replacement: member operations use WebUser/WebSession REST APIs.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MemberApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(410, new { status = "gone", legacy = "Member.asmx", message = "This legacy SOAP endpoint has been retired. Use the modern WebUser/WebSession REST APIs." });
        }
    }
}
