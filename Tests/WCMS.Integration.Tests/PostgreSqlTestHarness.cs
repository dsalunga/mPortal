using System;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;

namespace WCMS.Integration.Tests
{
    internal static class PostgreSqlTestHarness
    {
        private static readonly object Sync = new object();
        private static PostgreSqlContainer _container;
        private static bool _initialized;

        public static bool IsAvailable { get; private set; }
        public static string UnavailableReason { get; private set; } = string.Empty;
        public static string ConnectionString { get; private set; } = string.Empty;

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
                IsAvailable = true;
            }
            catch (Exception ex)
            {
                IsAvailable = false;
                UnavailableReason = ex.Message;
            }
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
