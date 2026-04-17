using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.Integration
{
    /// <summary>
    /// Legacy compatibility stub for BibleService.asmx (SOAP Bible data service).
    /// The legacy ASMX service provided SOAP endpoints for Bible reader data.
    /// Modern replacement: BibleReader app has its own BibleApiController REST API.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BibleserviceApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(410, new { status = "gone", legacy = "BibleService.asmx", message = "This legacy SOAP endpoint has been retired. Use the BibleReader app REST API (BibleApiController)." });
        }
    }
}
