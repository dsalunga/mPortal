using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.G2
{
    /// <summary>
    /// Legacy compatibility stub for FlashService.asmx (Flash banner SOAP service).
    /// The legacy ASMX service provided SOAP endpoints for Flash banner management.
    /// Modern replacement: Flash technology is obsolete; banner features use CSS/JS animations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FlashserviceApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(410, new { status = "gone", legacy = "FlashService.asmx", message = "This legacy Flash SOAP endpoint has been retired. Flash technology is obsolete; banners use CSS/JS." });
        }
    }
}
