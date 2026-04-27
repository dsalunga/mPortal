using System;
using System.IO;
using System.Threading.Tasks;
using Npgsql;
using Testcontainers.PostgreSql;

namespace WCMS.WebSystem.IntegrationTests
{
    internal static class PostgreSqlTestHarness
    {
        private static readonly object Sync = new object();
        private static PostgreSqlContainer _container;
        private static bool _initialized;

        public static bool IsAvailable { get; private set; }
        public static string UnavailableReason { get; private set; } = string.Empty;
        public static string ConnectionString { get; private set; } = string.Empty;
        public const string FixtureUserName = "itest.user";
        public const string FixturePassword = "itest-pass";
        public const int FixturePageId = 1;

        public static async Task EnsureStartedAsync()
        {
            lock (Sync)
            {
                if (_initialized)
                {
                    return;
                }

                _initialized = true;
            }

            try
            {
                _container = new PostgreSqlBuilder()
                    .WithImage("postgres:17-alpine")
                    .WithDatabase("mportal_test")
                    .WithUsername("postgres")
                    .WithPassword("postgres")
                    .Build();

                await _container.StartAsync();
                ConnectionString = _container.GetConnectionString();
                await ApplyBaselineDataAsync(ConnectionString);
                IsAvailable = true;
            }
            catch (Exception ex)
            {
                IsAvailable = false;
                UnavailableReason = ex.Message;
            }
        }

        private static async Task ApplyBaselineDataAsync(string connectionString)
        {
            if (string.Equals(Environment.GetEnvironmentVariable("WCMS_SKIP_PG_SEED_INIT"), "1", StringComparison.Ordinal))
            {
                return;
            }

            var scriptsDir = ResolvePostgreSqlScriptsDir();
            var scripts = new[]
            {
                "schema.sql",
                "schema-integration.sql",
                "schema-biblereader.sql",
                "seed-data.sql",
                "seed-test-fixtures.sql"
            };

            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            foreach (var file in scripts)
            {
                var path = Path.Combine(scriptsDir, file);
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException("Missing PostgreSQL initialization script.", path);
                }

                var sql = await File.ReadAllTextAsync(path);
                if (string.IsNullOrWhiteSpace(sql))
                {
                    continue;
                }

                await using var command = new NpgsqlCommand(sql, connection);
                await command.ExecuteNonQueryAsync();
            }
        }

        private static string ResolvePostgreSqlScriptsDir()
        {
            var dir = new DirectoryInfo(AppContext.BaseDirectory);
            while (dir != null)
            {
                var candidate = Path.Combine(dir.FullName, "Portal", "Assets", "Database", "PostgreSQL");
                if (Directory.Exists(candidate))
                {
                    return candidate;
                }

                dir = dir.Parent;
            }

            throw new DirectoryNotFoundException(
                "Could not locate Portal/Assets/Database/PostgreSQL from test runtime directory.");
        }

        public static async Task DisposeAsync()
        {
            if (_container != null)
            {
                await _container.DisposeAsync();
            }
        }
    }
}
