using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.Integration
{
    /// <summary>
    /// Ported from BibleService.asmx (Apps/Integration/BibleReader).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BibleserviceApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // TODO: Implement endpoint logic from legacy BibleService.asmx
            return Ok(new { status = "not_implemented", legacy = "BibleService.asmx" });
        }
    }
}
