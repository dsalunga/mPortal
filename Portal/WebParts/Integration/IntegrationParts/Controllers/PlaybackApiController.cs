using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.Integration
{
    /// <summary>
    /// Legacy compatibility stub for Playback.ashx (media streaming handler).
    /// The legacy handler served media file streams for the LessonReviewer profile.
    /// Modern replacement: media playback uses HTML5 audio/video with direct file serving.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PlaybackApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(410, new { status = "gone", legacy = "Playback.ashx", message = "This legacy media handler has been retired. Use HTML5 audio/video elements with direct file URLs." });
        }
    }
}
