using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.Integration
{
    /// <summary>
    /// Legacy compatibility stub for VerifySession.ashx (streaming session verification handler).
    /// The legacy handler verified session tokens for media streaming access.
    /// Modern replacement: session verification uses ASP.NET Core authentication middleware.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class VerifysessionApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(410, new { status = "gone", legacy = "VerifySession.ashx", message = "This legacy session handler has been retired. Session verification uses ASP.NET Core authentication middleware." });
        }
    }
}
