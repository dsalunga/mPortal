using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.Integration
{
    /// <summary>
    /// Ported from Member.asmx (Apps/Integration).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MemberApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // TODO: Implement endpoint logic from legacy Member.asmx
            return Ok(new { status = "not_implemented", legacy = "Member.asmx" });
        }
    }
}
