using Microsoft.Extensions.Options;

namespace WCMS.Framework.Configuration
{
    /// <summary>
    /// Scoped service that provides access to <see cref="WConfigOptions"/> via DI.
    /// Automatically picks up runtime configuration changes from WebRegistry
    /// through <see cref="IOptionsMonitor{WConfigOptions}"/>.
    /// </summary>
    public class WConfigService
    {
        private readonly IOptionsMonitor<WConfigOptions> _optionsMonitor;

        public WConfigService(IOptionsMonitor<WConfigOptions> optionsMonitor)
        {
            _optionsMonitor = optionsMonitor;
        }

        /// <summary>Gets the current configuration snapshot.</summary>
        public WConfigOptions CurrentConfig => _optionsMonitor.CurrentValue;

        public string Environment => CurrentConfig.Environment;
        public string SystemName => CurrentConfig.SystemName;
        public string BaseAddress => CurrentConfig.BaseAddress;
        public bool AllowCache => CurrentConfig.AllowCache;
        public bool AgentEnabled => CurrentConfig.AgentEnabled;
        public bool EnableLogging => CurrentConfig.EnableLogging;
        public int DefaultSite => CurrentConfig.DefaultSite;
        public string DefaultDataProvider => CurrentConfig.DefaultDataProvider;
    }
}
