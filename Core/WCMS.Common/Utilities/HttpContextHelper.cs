using Microsoft.AspNetCore.Http;

namespace WCMS.Common.Utilities
{
    /// <summary>
    /// Provides centralized access to IHttpContextAccessor for legacy static helpers
    /// that previously relied on System.Web.HttpContext.Current. This enables a gradual
    /// migration path away from SystemWebAdapters toward native ASP.NET Core APIs.
    /// 
    /// Usage: Call HttpContextHelper.Configure(accessor) from Program.cs startup,
    /// then use HttpContextHelper.Current instead of HttpContext.Current.
    /// </summary>
    public static class HttpContextHelper
    {
        private static IHttpContextAccessor _accessor;

        /// <summary>
        /// Configures the shared IHttpContextAccessor instance.
        /// Call once from Program.cs after building the service provider.
        /// </summary>
        public static void Configure(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <summary>
        /// Gets the current ASP.NET Core HttpContext via IHttpContextAccessor.
        /// Returns null when not in an HTTP request context.
        /// </summary>
        public static HttpContext Current => _accessor?.HttpContext;

        /// <summary>
        /// Returns true if running inside an HTTP request context.
        /// Replaces `System.Web.HttpContext.Current != null` checks.
        /// </summary>
        public static bool IsWebRequest => _accessor?.HttpContext != null;
    }
}
