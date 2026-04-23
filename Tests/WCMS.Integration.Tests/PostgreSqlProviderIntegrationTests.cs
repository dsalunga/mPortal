using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WCMS.Integration.Tests
{
    [TestClass]
    [TestCategory("PostgreSql")]
    public class PostgreSqlProviderIntegrationTests
    {
        private static WebApplicationFactory<Program> _factory;

        [ClassInitialize]
        public static async Task ClassInit(TestContext context)
        {
            await PostgreSqlTestHarness.EnsureStartedAsync();
            if (!PostgreSqlTestHarness.IsAvailable)
            {
                return;
            }

            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseSetting("WConfig:AllowCache", "false");
                    builder.UseSetting("WCMS:AllowCache", "false");
                    builder.UseSetting("WCMS:DatabaseProvider", "PostgreSql");
                    builder.UseSetting("ConnectionStrings:DefaultConnection", PostgreSqlTestHarness.ConnectionString);
                    builder.UseSetting("ConnectionStrings:ConnectionString", PostgreSqlTestHarness.ConnectionString);
                });
        }

        [ClassCleanup]
        public static async Task ClassCleanup()
        {
            _factory?.Dispose();
            await PostgreSqlTestHarness.DisposeAsync();
        }

        [TestMethod]
        public async Task PostgreSqlHealthEndpoint_ReturnsOkWhenContainerIsAvailable()
        {
            EnsureAvailabilityOrInconclusive();

            var client = _factory.CreateClient();
            var response = await client.GetAsync("/health");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task SystemInfo_ReportsPostgreSqlProvider()
        {
            EnsureAvailabilityOrInconclusive();

            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/system/info");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            StringAssert.Contains(content, "PostgreSql");
        }

        [TestMethod]
        public async Task AccountLogin_WithSeededFixtureUser_RedirectsWithoutLoginError_AndSetsAuthCookie()
        {
            EnsureAvailabilityOrInconclusive();

            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            var formData = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["userName"] = PostgreSqlTestHarness.FixtureUserName,
                ["password"] = PostgreSqlTestHarness.FixturePassword
            });

            var response = await client.PostAsync("/account/login", formData);
            Assert.AreEqual(HttpStatusCode.Redirect, response.StatusCode);

            var location = response.Headers.Location?.ToString() ?? string.Empty;
            Assert.IsFalse(location.Contains("LoginError="), "Fixture login should not redirect with LoginError.");

            Assert.IsTrue(response.Headers.TryGetValues("Set-Cookie", out var cookies),
                "Successful login should set authentication cookies.");
            var combinedCookies = string.Join(";", cookies);
            StringAssert.Contains(combinedCookies, ".AspNetCore.Cookies");
        }

        [TestMethod]
        public async Task CmsFallback_RootPath_RendersSeededPage()
        {
            EnsureAvailabilityOrInconclusive();

            var client = _factory.CreateClient();
            var response = await client.GetAsync("/");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            StringAssert.Contains(content, "wcms-runtime-page");
            StringAssert.Contains(content, $"data-page-id=\"{PostgreSqlTestHarness.FixturePageId}\"");
            StringAssert.Contains(content, "Message from a Theme");
            Assert.IsFalse(content.Contains("CMS Page ID:"), "Legacy placeholder HTML should not be used.");
        }

        private static void EnsureAvailabilityOrInconclusive()
        {
            if (!PostgreSqlTestHarness.IsAvailable || _factory == null)
            {
                Assert.Inconclusive("PostgreSQL container is not available: " + PostgreSqlTestHarness.UnavailableReason);
            }
        }
    }
}
