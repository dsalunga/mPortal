using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.G2
{
    /// <summary>
    /// Legacy compatibility stub for Handler.ashx (Download module HTTP handler).
    /// The legacy handler served file downloads from the Download module.
    /// Modern replacement: file downloads use FileManager ViewComponents and static file middleware.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class HandlerApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(410, new { status = "gone", legacy = "Handler.ashx", message = "This legacy download handler has been retired. File downloads use FileManager ViewComponents and static file middleware." });
        }
    }
}
