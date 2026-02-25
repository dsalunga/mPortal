using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Extensions;

namespace WCMS.Framework.Tests
{
    [TestClass]
    public class ServiceCollectionExtensionsTests
    {
        [TestMethod]
        public void AddWcmsFramework_RegistersIWSession()
        {
            var services = new ServiceCollection();
            services.AddWcmsFramework();

            var descriptor = FindService(services, typeof(IWSession));
            Assert.IsNotNull(descriptor, "IWSession should be registered");
            Assert.AreEqual(ServiceLifetime.Scoped, descriptor.Lifetime);
        }

        [TestMethod]
        public void AddWcmsFramework_RegistersIWContext()
        {
            var services = new ServiceCollection();
            services.AddWcmsFramework();

            var descriptor = FindService(services, typeof(IWContext));
            Assert.IsNotNull(descriptor, "IWContext should be registered");
            Assert.AreEqual(ServiceLifetime.Scoped, descriptor.Lifetime);
        }

        [TestMethod]
        public void AddWcmsFramework_ReturnsServiceCollection()
        {
            var services = new ServiceCollection();
            var result = services.AddWcmsFramework();
            Assert.AreSame(services, result);
        }

        [TestMethod]
        public void AddWcmsDatabase_SqlServer_RegistersIDbHelper()
        {
            // Reset DbHelper state
            ResetDbHelper();

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["WCMS:DatabaseProvider"] = "SqlServer",
                    ["ConnectionStrings:ConnectionString"] = "Server=test;Database=test;"
                })
                .Build();

            var services = new ServiceCollection();
            services.AddWcmsDatabase(config);

            var descriptor = FindService(services, typeof(IDbHelper));
            Assert.IsNotNull(descriptor, "IDbHelper should be registered");
            Assert.AreEqual(ServiceLifetime.Singleton, descriptor.Lifetime);
            Assert.IsTrue(DbHelper.IsConfigured);
            Assert.AreEqual(DatabaseProvider.SqlServer, DbHelper.Provider);
        }

        [TestMethod]
        public void AddWcmsDatabase_PostgreSql_RegistersIDbHelper()
        {
            ResetDbHelper();

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["WCMS:DatabaseProvider"] = "PostgreSql",
                    ["ConnectionStrings:ConnectionString"] = "Host=localhost;Database=test;"
                })
                .Build();

            var services = new ServiceCollection();
            services.AddWcmsDatabase(config);

            var descriptor = FindService(services, typeof(IDbHelper));
            Assert.IsNotNull(descriptor, "IDbHelper should be registered");
            Assert.AreEqual(DatabaseProvider.PostgreSql, DbHelper.Provider);
        }

        [TestMethod]
        public void AddWcmsDatabase_DefaultProvider_UsesSqlServer()
        {
            ResetDbHelper();

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["ConnectionStrings:ConnectionString"] = "Server=test;Database=test;"
                })
                .Build();

            var services = new ServiceCollection();
            services.AddWcmsDatabase(config);

            Assert.AreEqual(DatabaseProvider.SqlServer, DbHelper.Provider);
        }

        private static ServiceDescriptor FindService(IServiceCollection services, Type serviceType)
        {
            foreach (var descriptor in services)
            {
                if (descriptor.ServiceType == serviceType)
                    return descriptor;
            }
            return null;
        }

        private static void ResetDbHelper()
        {
            var field = typeof(DbHelper).GetField("_instance",
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            field?.SetValue(null, null);
        }
    }
}
