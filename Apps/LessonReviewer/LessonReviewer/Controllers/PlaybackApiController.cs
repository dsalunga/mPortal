using Microsoft.AspNetCore.Mvc;

namespace LessonReviewer
{
    /// <summary>
    /// Ported from Playback.ashx (Handlers).
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
