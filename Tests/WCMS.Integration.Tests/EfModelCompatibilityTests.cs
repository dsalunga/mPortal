using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCMS.WebSystem.Apps.BranchLocator.Data;
using WCMS.WebSystem.Apps.Integration.Data;

namespace WCMS.Integration.Tests
{
    [TestClass]
    [TestCategory("PostgreSql")]
    public class EfModelCompatibilityTests
    {
        [ClassInitialize]
        public static async Task ClassInit(TestContext context)
        {
            await PostgreSqlTestHarness.EnsureStartedAsync();
        }

        [TestMethod]
        public async Task IntegrationDbContext_GeneratesPostgreSqlCompatibleScript()
        {
            EnsureAvailabilityOrInconclusive();

            await using var context = new IntegrationDbContext(
                new DbContextOptionsBuilder<IntegrationDbContext>()
                    .UseNpgsql(PostgreSqlTestHarness.ConnectionString)
                    .Options);

            var script = context.Database.GenerateCreateScript();
            StringAssert.Contains(script, "MCCandidates");
            StringAssert.Contains(script, "MemberLinks");
        }

        [TestMethod]
        public async Task MusicDbContext_GeneratesPostgreSqlCompatibleScript()
        {
            EnsureAvailabilityOrInconclusive();

            await using var context = new MusicDbContext(
                new DbContextOptionsBuilder<MusicDbContext>()
                    .UseNpgsql(PostgreSqlTestHarness.ConnectionString)
                    .Options);

            var script = context.Database.GenerateCreateScript();
            StringAssert.Contains(script, "Musics");
            StringAssert.Contains(script, "MusicEntries");
        }

        [TestMethod]
        public async Task ExternalDbContext_GeneratesPostgreSqlCompatibleScript()
        {
            EnsureAvailabilityOrInconclusive();

            await using var context = new ExternalDbContext(
                new DbContextOptionsBuilder<ExternalDbContext>()
                    .UseNpgsql(PostgreSqlTestHarness.ConnectionString)
                    .Options);

            var script = context.Database.GenerateCreateScript();
            StringAssert.Contains(script, "Countries");
            StringAssert.Contains(script, "States");
        }

        [TestMethod]
        public async Task BranchLocatorDbContext_GeneratesPostgreSqlCompatibleScript()
        {
            EnsureAvailabilityOrInconclusive();

            await using var context = new BranchLocatorDbContext(
                new DbContextOptionsBuilder<BranchLocatorDbContext>()
                    .UseNpgsql(PostgreSqlTestHarness.ConnectionString)
                    .Options);

            var script = context.Database.GenerateCreateScript();
            StringAssert.Contains(script, "MChapters");
        }

        private static void EnsureAvailabilityOrInconclusive()
        {
            if (!PostgreSqlTestHarness.IsAvailable)
            {
                Assert.Inconclusive("PostgreSQL container is not available: " + PostgreSqlTestHarness.UnavailableReason);
            }
        }
    }
}
