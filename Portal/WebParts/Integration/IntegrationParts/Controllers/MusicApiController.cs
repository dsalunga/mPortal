using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.Integration
{
    /// <summary>
    /// Ported from Music.svc (Apps/Integration).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MusicApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // TODO: Implement endpoint logic from legacy Music.svc
            return Ok(new { status = "not_implemented", legacy = "Music.svc" });
        }
    }
}
