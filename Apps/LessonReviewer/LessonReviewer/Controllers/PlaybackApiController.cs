using Microsoft.AspNetCore.Mvc;

namespace LessonReviewer
{
    /// <summary>
    /// Legacy compatibility endpoint for Playback.ashx.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PlaybackApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(410, new
            {
                status = "gone",
                legacy = "Playback.ashx",
                message = "This legacy playback handler has been retired. Use modern LessonReviewer media APIs."
            });
        }
    }
}
