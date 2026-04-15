using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.Integration
{
    /// <summary>
    /// Ported from DataSync.svc (Apps/Integration/Registration).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DatasyncApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // TODO: Implement endpoint logic from legacy DataSync.svc
            return Ok(new { status = "not_implemented", legacy = "DataSync.svc" });
        }
    }
}
