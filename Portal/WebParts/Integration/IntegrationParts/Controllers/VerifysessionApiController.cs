using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.Integration
{
    /// <summary>
    /// Ported from VerifySession.ashx (Apps/Integration/Streaming).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class VerifysessionApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // TODO: Implement endpoint logic from legacy VerifySession.ashx
            return Ok(new { status = "not_implemented", legacy = "VerifySession.ashx" });
        }
    }
}
