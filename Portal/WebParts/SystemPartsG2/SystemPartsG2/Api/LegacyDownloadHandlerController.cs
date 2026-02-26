using System;
using System.Data;
using System.Globalization;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.Api
{
    /// <summary>
    /// Legacy-compatible replacement for:
    /// /_Sections/Download/Handler.ashx?ID={id}&Force=true|false
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    public class LegacyDownloadHandlerController : ControllerBase
    {
        private const string LegacyUploadFolder = "Assets/Uploads/Image/SECTIONS/Download";

        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<LegacyDownloadHandlerController> _logger;
        private readonly FileExtensionContentTypeProvider _contentTypeProvider = new();

        public LegacyDownloadHandlerController(
            IWebHostEnvironment hostEnvironment,
            ILogger<LegacyDownloadHandlerController> logger)
        {
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }

        [HttpGet("/_Sections/Download/Handler.ashx")]
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
        public IActionResult Get([FromQuery(Name = "ID")] string id, [FromQuery(Name = "Force")] string force)
        {
            Response.Headers.CacheControl = "public";

            if (!int.TryParse(id, NumberStyles.Integer, CultureInfo.InvariantCulture, out var downloadId) || downloadId <= 0)
                return NotFound();

            var fileName = TryGetDownloadFileName(downloadId);
            if (string.IsNullOrWhiteSpace(fileName))
                return NotFound();

            var resolvedFilePath = TryResolveDownloadPath(fileName);
            if (string.IsNullOrWhiteSpace(resolvedFilePath) || !System.IO.File.Exists(resolvedFilePath))
                return NotFound();

            if (string.Equals(force, "true", StringComparison.OrdinalIgnoreCase))
                Response.Headers.ContentDisposition = $"attachment; filename={Path.GetFileName(fileName)}";

            if (!_contentTypeProvider.TryGetContentType(fileName, out var contentType))
                contentType = "application/octet-stream";

            return PhysicalFile(resolvedFilePath, contentType);
        }

        private string TryGetDownloadFileName(int downloadId)
        {
            try
            {
                var query = DbHelper.Provider == DatabaseProvider.PostgreSql
                    ? "SELECT \"Filename\" FROM \"Download\" WHERE \"DownloadID\" = @DownloadID"
                    : "SELECT Filename FROM Download.Downloads WHERE DownloadID = @DownloadID";

                var value = DbHelper.ExecuteScalar(
                    CommandType.Text,
                    query,
                    DbHelper.CreateParameter("@DownloadID", downloadId));

                return value?.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, "Legacy download handler query failed. DownloadID={DownloadID}", downloadId);
                return null;
            }
        }

        private string TryResolveDownloadPath(string fileName)
        {
            var safeFileName = Path.GetFileName(fileName);
            if (string.IsNullOrWhiteSpace(safeFileName))
                return null;

            var roots = new[]
            {
                _hostEnvironment.WebRootPath,
                _hostEnvironment.ContentRootPath
            };

            foreach (var root in roots)
            {
                if (string.IsNullOrWhiteSpace(root))
                    continue;

                var folderPath = Path.GetFullPath(Path.Combine(root, LegacyUploadFolder));
                if (!folderPath.StartsWith(root, StringComparison.OrdinalIgnoreCase))
                    continue;

                var candidatePath = Path.GetFullPath(Path.Combine(folderPath, safeFileName));
                if (!candidatePath.StartsWith(folderPath, StringComparison.OrdinalIgnoreCase))
                    continue;

                if (System.IO.File.Exists(candidatePath))
                    return candidatePath;
            }

            return null;
        }
    }
}
