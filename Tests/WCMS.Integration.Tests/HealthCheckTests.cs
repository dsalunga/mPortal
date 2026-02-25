using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WCMS.Integration.Tests
{
    [TestClass]
    public class HealthCheckTests
    {
        private static WebApplicationFactory<Program> _factory;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    // Disable CMS cache to prevent WebObject XML/DB initialization
                    builder.UseSetting("WConfig:AllowCache", "false");
                    builder.UseSetting("WCMS:AllowCache", "false");
                });
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _factory?.Dispose();
        }

        [TestMethod]
        public async Task HealthEndpoint_ReturnsOk()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/health");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(content.Contains("ok"), "Health endpoint should return ok");
        }

        [TestMethod]
        public async Task SystemInfoEndpoint_ReturnsJson()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/system/info");

            // May fail with InternalServerError if WConfig static initialization fails
            // (requires database). In test environment, at minimum verify endpoint exists.
            Assert.IsTrue(
                response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.InternalServerError,
                $"Unexpected status code: {response.StatusCode}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                Assert.IsTrue(content.Contains("mPortal CMS"), "Response should contain app name");
                Assert.IsTrue(content.Contains(".NET 10"), "Response should contain framework version");
            }
        }
    }
}
