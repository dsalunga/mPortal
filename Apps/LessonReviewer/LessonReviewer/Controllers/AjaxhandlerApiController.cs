using Microsoft.AspNetCore.Mvc;

namespace LessonReviewer
{
    /// <summary>
    /// Ported from AjaxHandler.ashx (Handlers).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AjaxhandlerApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // TODO: Implement endpoint logic from legacy AjaxHandler.ashx
            return Ok(new { status = "not_implemented", legacy = "AjaxHandler.ashx" });
        }
    }
}
