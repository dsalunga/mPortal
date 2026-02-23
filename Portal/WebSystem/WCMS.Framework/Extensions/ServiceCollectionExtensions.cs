using Microsoft.Extensions.DependencyInjection;

namespace WCMS.Framework.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWcmsFramework(this IServiceCollection services)
        {
            services.AddScoped<IWSession, WSession>();
            services.AddScoped<IWContext, WContext>();
            return services;
        }
    }
}
