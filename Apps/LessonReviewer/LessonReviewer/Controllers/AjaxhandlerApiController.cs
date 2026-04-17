using Microsoft.AspNetCore.Mvc;

namespace LessonReviewer
{
    /// <summary>
    /// Legacy compatibility endpoint for AjaxHandler.ashx.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AjaxhandlerApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(410, new
            {
                status = "gone",
                legacy = "AjaxHandler.ashx",
                message = "This legacy HTTP handler has been retired. Use modern LessonReviewer API routes."
            });
        }
    }
}
