using System.Net;
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

        private static void EnsureAvailabilityOrInconclusive()
        {
            if (!PostgreSqlTestHarness.IsAvailable || _factory == null)
            {
                Assert.Inconclusive("PostgreSQL container is not available: " + PostgreSqlTestHarness.UnavailableReason);
            }
        }
    }
}
