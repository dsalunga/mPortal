using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.Api
{
    /// <summary>
    /// Legacy-compatible replacement for:
    /// /Content/Admin/Handlers/Handler.ashx?Section=SectionTemplate&ID={id}
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    public class LegacyAdminHandlerController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<LegacyAdminHandlerController> _logger;

        public LegacyAdminHandlerController(
            IWebHostEnvironment hostEnvironment,
            ILogger<LegacyAdminHandlerController> logger)
        {
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }

        [HttpGet("/Content/Admin/Handlers/Handler.ashx")]
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
        public IActionResult Get([FromQuery(Name = "Section")] string section, [FromQuery(Name = "ID")] string id)
        {
            Response.ContentType = "image/jpeg";
            Response.Headers.CacheControl = "public";

            byte[] imageBytes = null;
            if (string.Equals(section, "SectionTemplate", StringComparison.OrdinalIgnoreCase)
                && int.TryParse(id, NumberStyles.Integer, CultureInfo.InvariantCulture, out var sectionTemplateId))
            {
                imageBytes = TryGetSectionTemplateThumbnail(sectionTemplateId);
            }

            imageBytes ??= TryGetFallbackThumbnail();
            if (imageBytes == null || imageBytes.Length == 0)
                return NotFound();

            return File(imageBytes, "image/jpeg");
        }

        private byte[] TryGetSectionTemplateThumbnail(int sectionTemplateId)
        {
            if (sectionTemplateId <= 0)
                return null;

            foreach (var query in GetSectionTemplateThumbnailQueries())
            {
                try
                {
                    var value = DbHelper.ExecuteScalar(
                        CommandType.Text,
                        query,
                        DbHelper.CreateParameter("@CommonSectionItemTemplateID", sectionTemplateId));

                    if (value is byte[] bytes && bytes.Length > 0)
                        return bytes;

                    if (value is string imagePath && !string.IsNullOrWhiteSpace(imagePath))
                    {
                        var pathImage = TryReadImage(imagePath);
                        if (pathImage != null && pathImage.Length > 0)
                            return pathImage;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogDebug(ex, "Legacy handler query failed. Query={Query}", query);
                }
            }

            return null;
        }

        private IEnumerable<string> GetSectionTemplateThumbnailQueries()
        {
            if (DbHelper.Provider == DatabaseProvider.PostgreSql)
            {
                return new[]
                {
                    "SELECT \"Thumbnail\" FROM \"CommonSectionItemTemplates\" WHERE \"CommonSectionItemTemplateID\" = @CommonSectionItemTemplateID",
                    "SELECT \"Thumbnail\" FROM \"CMS\".\"CommonSectionItemTemplates\" WHERE \"CommonSectionItemTemplateID\" = @CommonSectionItemTemplateID",
                    "SELECT \"Thumbnail\" FROM \"CMS.CommonSectionItemTemplates\" WHERE \"CommonSectionItemTemplateID\" = @CommonSectionItemTemplateID"
                };
            }

            return new[]
            {
                "SELECT Thumbnail FROM CMS.CommonSectionItemTemplates WHERE CommonSectionItemTemplateID = @CommonSectionItemTemplateID",
                "SELECT Thumbnail FROM CommonSectionItemTemplates WHERE CommonSectionItemTemplateID = @CommonSectionItemTemplateID"
            };
        }

        private byte[] TryGetFallbackThumbnail()
        {
            var fallbackPaths = new[]
            {
                "/_Sections/Thumb.jpg",
                "/Content/Assets/Images/PartThumb.jpg"
            };

            foreach (var fallbackPath in fallbackPaths)
            {
                var fallbackImage = TryReadImage(fallbackPath);
                if (fallbackImage != null && fallbackImage.Length > 0)
                    return fallbackImage;
            }

            return null;
        }

        private byte[] TryReadImage(string pathOrUrl)
        {
            if (string.IsNullOrWhiteSpace(pathOrUrl))
                return null;

            var normalized = pathOrUrl.Replace('\\', '/');
            normalized = normalized.TrimStart('~');
            normalized = normalized.TrimStart('/');

            var candidateRoots = new[]
            {
                _hostEnvironment.WebRootPath,
                _hostEnvironment.ContentRootPath
            };

            foreach (var root in candidateRoots)
            {
                if (string.IsNullOrWhiteSpace(root))
                    continue;

                var absolutePath = Path.GetFullPath(Path.Combine(root, normalized));
                if (!absolutePath.StartsWith(root, StringComparison.OrdinalIgnoreCase))
                    continue;

                if (!System.IO.File.Exists(absolutePath))
                    continue;

                try
                {
                    return System.IO.File.ReadAllBytes(absolutePath);
                }
                catch (Exception ex)
                {
                    _logger.LogDebug(ex, "Unable to load legacy handler image path. Path={Path}", absolutePath);
                    return null;
                }
            }

            return null;
        }
    }
}
