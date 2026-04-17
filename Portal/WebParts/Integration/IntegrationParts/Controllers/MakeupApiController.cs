using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.Integration
{
    /// <summary>
    /// Legacy compatibility stub for MakeUp.ashx (make-up class handler).
    /// The legacy handler processed make-up class scheduling requests.
    /// Modern replacement: make-up class features use Integration ViewComponents and REST APIs.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MakeupApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(410, new { status = "gone", legacy = "MakeUp.ashx", message = "This legacy handler has been retired. Make-up class features use Integration ViewComponents and REST APIs." });
        }
    }
}
