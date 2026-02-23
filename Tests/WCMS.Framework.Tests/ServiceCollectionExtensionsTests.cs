using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        private static ServiceDescriptor FindService(IServiceCollection services, Type serviceType)
        {
            foreach (var descriptor in services)
            {
                if (descriptor.ServiceType == serviceType)
                    return descriptor;
            }
            return null;
        }
    }
}
