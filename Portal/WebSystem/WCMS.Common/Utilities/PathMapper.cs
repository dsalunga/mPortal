using System;
using System.IO;

namespace WCMS.Common.Utilities
{
    /// <summary>
    /// Cross-platform path mapper replacing System.Web Server.MapPath().
    /// Configure at startup: PathMapper.Configure(builder.Environment.ContentRootPath, builder.Environment.WebRootPath);
    /// </summary>
    public static class PathMapper
    {
        private static string _contentRootPath;
        private static string _webRootPath;

        /// <summary>
        /// Sets the root paths for the application. Call this at startup.
        /// </summary>
        public static void Configure(string contentRootPath, string webRootPath = null)
        {
            _contentRootPath = contentRootPath;
            _webRootPath = webRootPath ?? contentRootPath;
        }

        /// <summary>
        /// Maps a virtual path (e.g., "~/App_Data/Config.xml") to a physical path.
        /// Replaces HttpContext.Current.Server.MapPath() for ASP.NET Core.
        /// </summary>
        public static string MapPath(string virtualPath)
        {
            if (string.IsNullOrEmpty(virtualPath))
                return _contentRootPath ?? Directory.GetCurrentDirectory();

            // Already an absolute path
            if (Path.IsPathRooted(virtualPath) || virtualPath.Contains(":") || virtualPath.StartsWith(@"\\"))
                return FileHelper.EvalPath(virtualPath);

            // Strip ~/ or ~ prefix
            var path = virtualPath;
            if (path.StartsWith("~/") || path.StartsWith("~\\"))
                path = path.Substring(2);
            else if (path.StartsWith("~"))
                path = path.Substring(1);

            // Normalize separators
            path = path.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);

            var root = _contentRootPath ?? Directory.GetCurrentDirectory();
            return Path.Combine(root, path);
        }

        /// <summary>
        /// Returns the content root path (equivalent to Server.MapPath("~")).
        /// </summary>
        public static string ContentRootPath => _contentRootPath ?? Directory.GetCurrentDirectory();

        /// <summary>
        /// Returns the web root path (wwwroot).
        /// </summary>
        public static string WebRootPath => _webRootPath ?? Directory.GetCurrentDirectory();
    }
}
