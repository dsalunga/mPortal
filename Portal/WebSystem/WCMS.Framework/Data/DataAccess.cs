using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Reflection;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core
{
    public sealed class DataAccess
    {
        //private static readonly string _path = ConfigHelper.Get["DefaultDataProvider"];
        private static readonly string _xmlProviderPath = ConfigHelper.Get("WebObject.XmlProvider");

        /// <summary>
        /// Creates a specific provider instance based on the defined value in the configuration.
        /// It uses the default provider defined
        /// </summary>
        /// <typeparam name="T">Interface of the provider</typeparam>
        /// <returns></returns>
        public static T CreateProvider<T>()
        {
            string path = WConfig.DefaultDataProvider;
            Type dal = typeof(T);

            string className = path + "." + dal.Name.Substring(1);
            return (T)Assembly.Load(path).CreateInstance(className);
        }

        /// <summary>
        /// Creates an Xml provider for a specific class. Should support passing the object
        /// as parameter to help in resolving the correct provider.
        /// </summary>
        /// <typeparam name="T">interface of the provider</typeparam>
        /// <returns></returns>
        public static T CreateXmlProvider<T>()
        {
            Type dal = typeof(T);

            string className = _xmlProviderPath + "." + dal.Name.Substring(1);
            return (T)Assembly.Load(_xmlProviderPath).CreateInstance(className);
        }

        /// <summary>
        /// Create an instance of the WebObject provider based on configuration
        /// Might need to removed this something and support only the XML provider
        /// </summary>
        /// <returns></returns>
        public static IWebObjectProvider CreateWebObjectProvider()
        {
            string providerPath = ConfigHelper.Get("WebObject.DataProvider");
            IWebObjectProvider provider = (IWebObjectProvider)Activator.CreateInstance(Type.GetType(providerPath));

            return provider;
        }
    }
}
