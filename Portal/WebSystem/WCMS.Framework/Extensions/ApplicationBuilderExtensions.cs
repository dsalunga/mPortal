using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WCMS.Framework.Middleware;

namespace WCMS.Framework.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configures the WSession bridge so that WSession.Current resolves via DI.
        /// Call after building the app and before mapping routes.
        /// </summary>
        public static IApplicationBuilder UseWcmsFramework(this IApplicationBuilder app)
        {
            var accessor = app.ApplicationServices
                .GetRequiredService<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
            WSession.Configure(accessor);
            return app;
        }

        public static IApplicationBuilder UseWcmsPageResolution(this IApplicationBuilder app)
        {
            return app.UseMiddleware<PageResolutionMiddleware>();
        }

        public static IApplicationBuilder UseWcmsPageRendering(this IApplicationBuilder app)
        {
            return app.UseMiddleware<PageRenderingMiddleware>();
        }
    }
}
