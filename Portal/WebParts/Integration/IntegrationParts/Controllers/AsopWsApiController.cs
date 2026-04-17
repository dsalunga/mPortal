using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.Integration
{
    /// <summary>
    /// Legacy compatibility stub for ASOP-WS.asmx (SOAP web service for music competition).
    /// The legacy ASMX service provided SOAP endpoints for ASOP music competition data.
    /// Modern replacement: ASOP integration uses REST APIs via Integration ViewComponents.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AsopWsApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(410, new { status = "gone", legacy = "ASOP-WS.asmx", message = "This legacy SOAP endpoint has been retired. ASOP integration uses REST APIs and ViewComponents." });
        }
    }
}
