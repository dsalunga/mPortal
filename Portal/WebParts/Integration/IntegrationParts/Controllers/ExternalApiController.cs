using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.Integration
{
    /// <summary>
    /// Ported from External.asmx (Apps/Integration).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ExternalApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // TODO: Implement endpoint logic from legacy External.asmx
            return Ok(new { status = "not_implemented", legacy = "External.asmx" });
        }
    }
}
