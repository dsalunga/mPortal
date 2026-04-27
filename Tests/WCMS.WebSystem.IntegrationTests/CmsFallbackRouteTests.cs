extern alias WebSystemApp;

using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppProgram = WebSystemApp::Program;

namespace WCMS.WebSystem.IntegrationTests
{
    [TestClass]
    public class CmsFallbackRouteTests
    {
        private static WebApplicationFactory<AppProgram> _factory;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            _factory = new WebApplicationFactory<AppProgram>()
                .WithWebHostBuilder(builder =>
                {
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
        public async Task CmsFallback_UnresolvedRoute_ReturnsNotFoundWithoutPlaceholderHtml()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/__integration-test-page-not-found");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            Assert.IsFalse(content.Contains("CMS Page ID:"), "Legacy placeholder fallback HTML should not be returned.");
        }
    }
}
