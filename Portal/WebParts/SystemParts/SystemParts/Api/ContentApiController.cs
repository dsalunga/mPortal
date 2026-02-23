using Microsoft.AspNetCore.Mvc;
using WCMS.WebSystem.WebParts.Content;

namespace WCMS.WebSystem.Api
{
    /// <summary>
    /// Replaces the legacy WCF ContentService (Service.svc).
    /// </summary>
    [ApiController]
    [Route("api/content")]
    public class ContentApiController : ControllerBase
    {
        [HttpGet("{id:int}")]
        public IActionResult GetContent(int id)
        {
            var item = id > 0 ? WebContent.Get(id) : null;
            return Ok(item != null ? item.Content : string.Empty);
        }

        [HttpGet("by-title/{title}")]
        public IActionResult GetContentByTitle(string title)
        {
            var item = string.IsNullOrEmpty(title) ? null : WebContent.Provider.Get(title);
            return Ok(item != null ? item.Content : string.Empty);
        }
    }
}
