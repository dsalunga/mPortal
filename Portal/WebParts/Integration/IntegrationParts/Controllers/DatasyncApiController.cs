using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.WebParts.Integration
{
    /// <summary>
    /// Legacy compatibility stub for DataSync.svc (WCF data synchronization service).
    /// The legacy WCF service handled data synchronization for registration workflows.
    /// Modern replacement: IDataSync interface with plain C# implementation (no WCF).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DatasyncApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(410, new { status = "gone", legacy = "DataSync.svc", message = "This legacy WCF endpoint has been retired. Data synchronization uses the IDataSync interface directly." });
        }
    }
}
