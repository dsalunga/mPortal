using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.G2
{
    /// <summary>
    /// Ported from FlashService.asmx (AppBundle2/FlashBanner).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FlashserviceApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // TODO: Implement endpoint logic from legacy FlashService.asmx
            return Ok(new { status = "not_implemented", legacy = "FlashService.asmx" });
        }
    }
}
