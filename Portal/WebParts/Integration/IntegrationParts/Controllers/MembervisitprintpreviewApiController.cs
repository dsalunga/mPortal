using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.Integration
{
    /// <summary>
    /// Ported from MemberVisitPrintPreview.ashx (Apps/Integration).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MembervisitprintpreviewApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // TODO: Implement endpoint logic from legacy MemberVisitPrintPreview.ashx
            return Ok(new { status = "not_implemented", legacy = "MemberVisitPrintPreview.ashx" });
        }
    }
}
