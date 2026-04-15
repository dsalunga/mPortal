using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.Integration
{
    /// <summary>
    /// Ported from MakeUp.ashx (Apps/Integration).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MakeupApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // TODO: Implement endpoint logic from legacy MakeUp.ashx
            return Ok(new { status = "not_implemented", legacy = "MakeUp.ashx" });
        }
    }
}
