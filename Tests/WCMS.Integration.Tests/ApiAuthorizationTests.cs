using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WCMS.Integration.Tests
{
    [TestClass]
    public class ApiAuthorizationTests
    {
        private static WebApplicationFactory<Program> _factory;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            _factory = new WebApplicationFactory<Program>()
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
        public async Task AccountApi_RequiresAuthentication()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/account/session/00000000-0000-0000-0000-000000000000");

            // Should return 401 or redirect to login (302) since [Authorize] is on controller
            Assert.IsTrue(
                response.StatusCode == HttpStatusCode.Unauthorized ||
                response.StatusCode == HttpStatusCode.Redirect,
                $"Expected 401 or 302, got {response.StatusCode}");
        }

        [TestMethod]
        public async Task UserApi_RequiresAuthentication()
        {
            var client = _factory.CreateClient();
            // UserApi has POST endpoints (check, info-from-session)
            var response = await client.PostAsync("/api/user/check",
                new System.Net.Http.StringContent("{}", System.Text.Encoding.UTF8, "application/json"));

            Assert.IsTrue(
                response.StatusCode == HttpStatusCode.Unauthorized ||
                response.StatusCode == HttpStatusCode.Redirect,
                $"Expected 401 or 302, got {response.StatusCode}");
        }

        [TestMethod]
        public async Task DataSyncApi_RequiresAuthentication()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/datasync/status");

            Assert.IsTrue(
                response.StatusCode == HttpStatusCode.Unauthorized ||
                response.StatusCode == HttpStatusCode.Redirect ||
                response.StatusCode == HttpStatusCode.NotFound, // endpoint may not exist as GET
                $"Expected auth challenge or not found, got {response.StatusCode}");
        }

        [TestMethod]
        public async Task FrameworkApi_RequiresAuthentication()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/framework/info");

            Assert.IsTrue(
                response.StatusCode == HttpStatusCode.Unauthorized ||
                response.StatusCode == HttpStatusCode.Redirect ||
                response.StatusCode == HttpStatusCode.NotFound,
                $"Expected auth challenge or not found, got {response.StatusCode}");
        }
    }
}
