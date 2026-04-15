using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.G2
{
    /// <summary>
    /// Ported from WebService.asmx (AppBundle2/Social).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class WebserviceApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // TODO: Implement endpoint logic from legacy WebService.asmx
            return Ok(new { status = "not_implemented", legacy = "WebService.asmx" });
        }
    }
}
