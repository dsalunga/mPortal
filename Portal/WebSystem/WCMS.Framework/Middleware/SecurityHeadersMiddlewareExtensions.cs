using Microsoft.AspNetCore.Builder;

namespace WCMS.Framework.Middleware
{
    /// <summary>
    /// Extension methods for adding security headers to the ASP.NET Core pipeline.
    /// Adds Content-Security-Policy, X-Content-Type-Options, X-Frame-Options,
    /// Referrer-Policy, and Permissions-Policy headers to all responses.
    /// </summary>
    public static class SecurityHeadersMiddlewareExtensions
    {
        public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
        {
            return app.Use(async (context, next) =>
            {
                var headers = context.Response.Headers;
                headers["X-Content-Type-Options"] = "nosniff";
                headers["X-Frame-Options"] = "SAMEORIGIN";
                headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
                headers["Permissions-Policy"] = "camera=(), microphone=(), geolocation=(self)";
                headers["X-XSS-Protection"] = "1; mode=block";

                await next();
            });
        }
    }
}
