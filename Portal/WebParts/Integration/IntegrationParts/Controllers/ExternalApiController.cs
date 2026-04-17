using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.Integration
{
    /// <summary>
    /// Legacy compatibility stub for External.asmx (SOAP external integration service).
    /// The legacy ASMX service provided SOAP endpoints for external system integration.
    /// Modern replacement: external integrations use ExternalHelper and REST APIs.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ExternalApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(410, new { status = "gone", legacy = "External.asmx", message = "This legacy SOAP endpoint has been retired. External integrations use ExternalHelper and REST APIs." });
        }
    }
}
