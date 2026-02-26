using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WCMS.Common.Utilities;
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
            services.AddSingleton<UserSessionManager>(sp =>
            {
                var distributedCache = sp.GetService<IDistributedCache>();
                return new UserSessionManager(distributedCache);
            });
            return services;
        }

        /// <summary>
        /// Configures the database provider (SQL Server or PostgreSQL) and registers the
        /// <see cref="IDbHelper"/> singleton from the "WCMS:DatabaseProvider" and "ConnectionStrings"
        /// configuration sections.
        /// </summary>
        public static IServiceCollection AddWcmsDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var providerName = configuration["WCMS:DatabaseProvider"];
            var provider = DbHelper.ParseProvider(providerName);

            var connString = configuration.GetConnectionString("ConnectionString")
                ?? configuration.GetConnectionString("DefaultConnection")
                ?? string.Empty;

            var timeOutStr = configuration["TimeOut"];
            int timeOut = string.IsNullOrEmpty(timeOutStr) ? 200 : int.Parse(timeOutStr);

            DbHelper.ConfigureFromSettings(provider, connString, timeOut);
            services.AddSingleton<IDbHelper>(DbHelper.Instance);

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
