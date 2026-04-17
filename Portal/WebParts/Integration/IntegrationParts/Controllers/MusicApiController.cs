using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.Integration
{
    /// <summary>
    /// Legacy compatibility stub for Music.svc (WCF music service).
    /// The legacy WCF service provided music catalog and playback data.
    /// Modern replacement: music features use REST APIs and Integration ViewComponents.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MusicApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(410, new { status = "gone", legacy = "Music.svc", message = "This legacy WCF endpoint has been retired. Music features use REST APIs and Integration ViewComponents." });
        }
    }
}
