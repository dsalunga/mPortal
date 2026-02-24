using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

using WCMS.Framework.Core;

namespace WCMS.Framework.Extensions
{
    /// <summary>
    /// ASP.NET Core configuration provider that exposes WebRegistry entries
    /// as key-value pairs in the configuration system.
    /// Keys are derived from the registry path hierarchy using ':' as separator.
    /// </summary>
    public class WebRegistryConfigurationProvider : ConfigurationProvider
    {
        public override void Load()
        {
            var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            try
            {
                var rootNodes = WebRegistry.GetByParentId(0);
                if (rootNodes != null)
                {
                    foreach (var node in rootNodes)
                    {
                        LoadNode(data, node, "WebRegistry");
                    }
                }
            }
            catch
            {
                // Database may not be available during startup; silently skip
            }

            Data = data;
        }

        private void LoadNode(Dictionary<string, string> data, WebRegistry node, string prefix)
        {
            var key = string.IsNullOrEmpty(prefix)
                ? node.Key
                : prefix + ":" + node.Key;

            if (!string.IsNullOrEmpty(node.Value))
            {
                data[key] = node.Value;
            }

            var children = WebRegistry.GetByParentId(node.Id);
            if (children != null)
            {
                foreach (var child in children)
                {
                    LoadNode(data, child, key);
                }
            }
        }

        /// <summary>
        /// Triggers a reload of registry data from the database.
        /// Call this when WebRegistry.Updated fires.
        /// </summary>
        public void Reload()
        {
            Load();
            OnReload();
        }
    }

    /// <summary>
    /// Configuration source for WebRegistry.
    /// </summary>
    public class WebRegistryConfigurationSource : IConfigurationSource
    {
        internal WebRegistryConfigurationProvider Provider { get; private set; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            Provider = new WebRegistryConfigurationProvider();
            return Provider;
        }
    }

    /// <summary>
    /// Extension methods for adding WebRegistry to ASP.NET Core configuration.
    /// </summary>
    public static class WebRegistryConfigurationExtensions
    {
        /// <summary>
        /// Adds WebRegistry as a configuration source.
        /// Usage: builder.Configuration.AddWebRegistry();
        /// </summary>
        public static IConfigurationBuilder AddWebRegistry(this IConfigurationBuilder builder)
        {
            var source = new WebRegistryConfigurationSource();
            builder.Add(source);
            return builder;
        }
    }
}
