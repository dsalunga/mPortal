using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.Integration
{
    /// <summary>
    /// Ported from Playback.ashx (Apps/Integration/Profile/LessonReviewer).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PlaybackApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // TODO: Implement endpoint logic from legacy Playback.ashx
            return Ok(new { status = "not_implemented", legacy = "Playback.ashx" });
        }
    }
}
