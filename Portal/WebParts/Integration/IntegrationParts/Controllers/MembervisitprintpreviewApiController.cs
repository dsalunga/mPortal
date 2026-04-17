using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.Integration
{
    /// <summary>
    /// Legacy compatibility stub for MemberVisitPrintPreview.ashx.
    /// The legacy HTTP handler generated print-preview HTML for member visit records.
    /// Modern replacement: print-friendly views are rendered client-side via ViewComponents.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MembervisitprintpreviewApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(410, new { status = "gone", legacy = "MemberVisitPrintPreview.ashx", message = "This legacy endpoint has been retired. Use the member visit ViewComponent for print-friendly rendering." });
        }
    }
}
