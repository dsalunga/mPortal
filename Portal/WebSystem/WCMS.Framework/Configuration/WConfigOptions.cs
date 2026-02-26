using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WCMS.Framework.Net;

namespace WCMS.Framework.Configuration
{
    /// <summary>
    /// Strongly-typed options mirroring <see cref="WConfig"/> properties.
    /// Bind to the "WConfig" configuration section via IOptions&lt;WConfigOptions&gt;.
    /// </summary>
    public class WConfigOptions
    {
        public string Environment { get; set; } = "DEV";
        public string DatabaseProvider { get; set; } = "SqlServer";
        public string SystemName { get; set; }
        public string BaseAddress { get; set; }
        public string TempFolder { get; set; }
        public string PageExt { get; set; }
        public string DefaultLoginPage { get; set; }
        public bool AllowCache { get; set; }
        public bool AgentEnabled { get; set; } = true;
        public bool EnableLogging { get; set; }
        public bool AutoLogin { get; set; }
        public bool EnableInlineEditor { get; set; }
        public bool PanelExpanded { get; set; }
        public int DefaultSite { get; set; }
        public string DefaultDataProvider { get; set; }
        public string FileCachePath { get; set; } = @"C:\WCMS-Cache";
        public string AttachmentBasePath { get; set; }
        public string UserPhotoPath { get; set; }
        public SmsConfig SMSConfig { get; set; } = new SmsConfig();
        public string HttpSmsUrl { get; set; }
        public string SubjectPrefix { get; set; }
        public int MinDiskFreeMB { get; set; } = 10240;
        public bool ResourcesExternalMode { get; set; }
        public bool EnablePerfLogging { get; set; }
    }

    /// <summary>
    /// Extension methods for registering <see cref="WConfigOptions"/> in the DI container.
    /// </summary>
    public static class WConfigOptionsExtensions
    {
        /// <summary>
        /// Registers <see cref="WConfigOptions"/> from the "WConfig" configuration section
        /// and connects WebRegistryConfigurationProvider for runtime changes via
        /// <see cref="Microsoft.Extensions.Options.IOptionsMonitor{TOptions}"/>.
        /// </summary>
        public static IServiceCollection AddWcmsConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<WConfigOptions>(config.GetSection("WConfig"));
            services.AddScoped<WConfigService>();
            return services;
        }
    }
}
