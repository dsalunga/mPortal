using System;
using System.Data;
using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;
using SysConfig = System.Configuration;

namespace WCMS.Common.Utilities
{
    public abstract class ConfigHelper : ConfigUtil
    {

    }

    /// <summary>
    /// Summary description for ConfigHelper
    /// </summary>
    public abstract class ConfigUtil
    {
        private static IConfiguration _configuration;

        /// <summary>
        /// Sets the ASP.NET Core IConfiguration instance for use by the CMS framework.
        /// Call this at startup: ConfigUtil.SetConfiguration(builder.Configuration);
        /// </summary>
        public static void SetConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string Get(string key)
        {
            if (_configuration != null)
            {
                if (string.IsNullOrEmpty(key))
                    return _configuration[key];

                var value = _configuration[key];
                if (!string.IsNullOrEmpty(value))
                    return value;

                // Legacy keys used "." separators while IConfiguration uses ":" for sections.
                if (key.Contains("."))
                {
                    value = _configuration[key.Replace('.', ':')];
                    if (!string.IsNullOrEmpty(value))
                        return value;
                }
                else if (key.Contains(":"))
                {
                    value = _configuration[key.Replace(':', '.')];
                    if (!string.IsNullOrEmpty(value))
                        return value;
                }

                return value;
            }

            return SysConfig.ConfigurationManager.AppSettings[key];
        }

        public static string GetConnectionString(string name)
        {
            if (_configuration != null)
                return _configuration.GetConnectionString(name);

            return SysConfig.ConfigurationManager.ConnectionStrings[name]?.ConnectionString;
        }

        public static NameValueCollection AppSettings
        {
            get { return SysConfig.ConfigurationManager.AppSettings; }
        }

        public static SysConfig.ConnectionStringSettingsCollection ConnectionStrings
        {
            get { return SysConfig.ConfigurationManager.ConnectionStrings; }
        }

        public static int GetInt32(string name)
        {
            return Convert.ToInt32(Get(name));
        }

        public static bool GetBool(string name, bool defaultIfNull = false)
        {
            return DataUtil.GetBool(Get(name), defaultIfNull);
        }
    }
}
