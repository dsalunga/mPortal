using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using WCMS.Framework.Middleware;

namespace WCMS.Framework.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWcmsFramework(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IWSession, WSession>();
            services.AddScoped<IWContext, WContext>();
            return services;
        }

        /// <summary>
        /// Registers the CMS theme-based view location expander for dynamic layout selection.
        /// Call after AddRazorPages()/AddControllersWithViews().
        /// </summary>
        public static IServiceCollection AddWcmsThemeSupport(this IServiceCollection services)
        {
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ThemeViewLocationExpander());
            });
            return services;
        }
    }
}
