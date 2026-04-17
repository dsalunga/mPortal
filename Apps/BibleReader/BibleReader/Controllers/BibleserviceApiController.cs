using Microsoft.AspNetCore.Mvc;

namespace BibleReader
{
    /// <summary>
    /// Legacy compatibility endpoint for BibleService.asmx.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BibleserviceApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(410, new
            {
                status = "gone",
                legacy = "BibleService.asmx",
                message = "This legacy SOAP endpoint has been retired. Use the modern Bible API routes."
            });
        }
    }
}
