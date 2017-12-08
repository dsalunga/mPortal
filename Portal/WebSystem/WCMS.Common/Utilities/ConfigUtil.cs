using System;
using System.Data;
using System.Configuration;

using System.Collections.Specialized;

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
        public static string Get(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static NameValueCollection AppSettings
        {
            get { return ConfigurationManager.AppSettings; }
        }

        public static ConnectionStringSettingsCollection ConnectionStrings
        {
            get { return ConfigurationManager.ConnectionStrings; }
        }

        public static int GetInt32(string name)
        {
            return Convert.ToInt32(Get(name));
        }

        public static bool GetBool(string name, bool defaultIfNull = false)
        {
            return DataHelper.GetBool(Get(name), defaultIfNull);
        }

        //public static string Get2(string name)
        //{
        //    return ConfigurationManager.AppSettings[name];
        //}
    }
}