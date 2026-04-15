using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.Integration
{
    /// <summary>
    /// Ported from MemberService.asmx (Apps/Integration/Profile).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MemberserviceApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // TODO: Implement endpoint logic from legacy MemberService.asmx
            return Ok(new { status = "not_implemented", legacy = "MemberService.asmx" });
        }
    }
}
