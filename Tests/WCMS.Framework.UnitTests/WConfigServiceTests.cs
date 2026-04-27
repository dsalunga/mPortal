using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCMS.Framework.Configuration;

namespace WCMS.Framework.UnitTests
{
    [TestClass]
    public class WConfigServiceTests
    {
        private static WConfigService CreateService(WConfigOptions options)
        {
            var monitor = new TestOptionsMonitor(options);
            return new WConfigService(monitor);
        }

        [TestMethod]
        public void CurrentConfig_ReturnsOptions()
        {
            var options = new WConfigOptions { Environment = "TEST", SystemName = "UnitTest" };
            var service = CreateService(options);

            Assert.AreEqual("TEST", service.Environment);
            Assert.AreEqual("UnitTest", service.SystemName);
        }

        [TestMethod]
        public void DefaultValues_AreCorrect()
        {
            var service = CreateService(new WConfigOptions());

            Assert.AreEqual("DEV", service.Environment);
            Assert.AreEqual(false, service.AllowCache);
            Assert.AreEqual(true, service.AgentEnabled);
            Assert.AreEqual(0, service.DefaultSite);
        }

        [TestMethod]
        public void AllProperties_ReflectOptions()
        {
            var options = new WConfigOptions
            {
                Environment = "PROD",
                SystemName = "mPortal",
                BaseAddress = "https://example.com",
                AllowCache = true,
                AgentEnabled = false,
                EnableLogging = true,
                DefaultSite = 42,
                DefaultDataProvider = "SqlServer"
            };
            var service = CreateService(options);

            Assert.AreEqual("PROD", service.Environment);
            Assert.AreEqual("mPortal", service.SystemName);
            Assert.AreEqual("https://example.com", service.BaseAddress);
            Assert.AreEqual(true, service.AllowCache);
            Assert.AreEqual(false, service.AgentEnabled);
            Assert.AreEqual(true, service.EnableLogging);
            Assert.AreEqual(42, service.DefaultSite);
            Assert.AreEqual("SqlServer", service.DefaultDataProvider);
        }

        /// <summary>Simple IOptionsMonitor stub for testing.</summary>
        private class TestOptionsMonitor : IOptionsMonitor<WConfigOptions>
        {
            public TestOptionsMonitor(WConfigOptions value) => CurrentValue = value;
            public WConfigOptions CurrentValue { get; }
            public WConfigOptions Get(string name) => CurrentValue;
            public System.IDisposable OnChange(System.Action<WConfigOptions, string> listener) => null;
        }
    }
}
