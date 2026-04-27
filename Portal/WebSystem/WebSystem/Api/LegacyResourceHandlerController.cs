using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Api
{
    /// <summary>
    /// Legacy-compatible replacement for Resource.ashx.
    /// Serves CSS/JS text resources referenced by persisted CMS content.
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    public class LegacyResourceHandlerController : ControllerBase
    {
        private readonly ILogger<LegacyResourceHandlerController> _logger;

        public LegacyResourceHandlerController(ILogger<LegacyResourceHandlerController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/content/handlers/Resource.ashx")]
        [HttpGet("/handlers/Resource.ashx")]
        [ResponseCache(Duration = 15552000, Location = ResponseCacheLocation.Any)]
        public IActionResult Get([FromQuery(Name = "Id")] int id = -1, [FromQuery(Name = "Name")] string name = "")
        {
            WebTextResource resource = null;

            if (id > 0)
            {
                try
                {
                    resource = WebTextResource.Get(id);
                }
                catch (Exception ex)
                {
                    _logger.LogDebug(ex, "Failed loading WebTextResource by id {Id}.", id);
                }
            }

            if (resource == null && !string.IsNullOrWhiteSpace(name))
            {
                try
                {
                    resource = WebTextResource.Provider.Get(name);
                }
                catch (Exception ex)
                {
                    _logger.LogDebug(ex, "Failed loading WebTextResource by name {Name}.", name);
                }
            }

            if (resource == null)
                return NotFound();

            var content = resource.Content ?? string.Empty;
            if (content.Length == 0)
            {
                try
                {
                    var physicalPath = resource.BuildAbsolutePhysicalPath();
                    if (!string.IsNullOrWhiteSpace(physicalPath) && System.IO.File.Exists(physicalPath))
                        content = System.IO.File.ReadAllText(physicalPath);
                }
                catch (Exception ex)
                {
                    _logger.LogDebug(ex, "Failed loading WebTextResource file fallback. ResourceId={Id}", resource.Id);
                }
            }

            Response.Headers.CacheControl = "public,max-age=15552000";
            return Content(content, ResolveContentType(resource), Encoding.UTF8);
        }

        private static string ResolveContentType(WebTextResource resource)
        {
            var declaredType = resource.ContentType?.Value;
            if (!string.IsNullOrWhiteSpace(declaredType))
                return declaredType;

            var hint = $"{resource.Title} {resource.PhysicalPath}".ToLowerInvariant();
            if (hint.Contains(".css"))
                return "text/css";
            if (hint.Contains(".js"))
                return "application/javascript";

            return "text/plain";
        }
    }
}
