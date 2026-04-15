using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.Integration
{
    /// <summary>
    /// Ported from ASOP-WS.asmx (Apps/Integration/MusicCompetition).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AsopWsApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // TODO: Implement endpoint logic from legacy ASOP-WS.asmx
            return Ok(new { status = "not_implemented", legacy = "ASOP-WS.asmx" });
        }
    }
}
