using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.G2
{
    /// <summary>
    /// Ported from Handler.ashx (AppBundle2/Download).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class HandlerApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // TODO: Implement endpoint logic from legacy Handler.ashx
            return Ok(new { status = "not_implemented", legacy = "Handler.ashx" });
        }
    }
}
