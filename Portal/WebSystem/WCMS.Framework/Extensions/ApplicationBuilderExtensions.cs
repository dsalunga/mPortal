using Microsoft.AspNetCore.Builder;
using WCMS.Framework.Middleware;

namespace WCMS.Framework.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseWcmsPageResolution(this IApplicationBuilder app)
        {
            return app.UseMiddleware<PageResolutionMiddleware>();
        }
    }
}
