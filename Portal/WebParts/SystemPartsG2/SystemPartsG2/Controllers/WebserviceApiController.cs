using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.G2
{
    /// <summary>
    /// Legacy compatibility stub for WebService.asmx (Social module SOAP service).
    /// The legacy ASMX service provided SOAP endpoints for social module features.
    /// Modern replacement: social features use REST APIs and ViewComponents.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class WebserviceApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(410, new { status = "gone", legacy = "WebService.asmx", message = "This legacy SOAP endpoint has been retired. Social features use REST APIs and ViewComponents." });
        }
    }
}
