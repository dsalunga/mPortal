using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;

namespace WCMS.WebSystem.IntegrationTests
{
    [TestClass]
    [TestCategory("DataMigration")]
    public class DataMigrationValidationTests
    {
        private static readonly string SqlServerConn =
            Environment.GetEnvironmentVariable("SQLSERVER_TEST_CONNECTION_STRING");

        private static readonly string PostgreSqlConn =
            Environment.GetEnvironmentVariable("POSTGRES_TEST_CONNECTION_STRING");

        [TestMethod]
        public async Task SqlServerAndPostgreSql_RowCounts_AreComparableForCoreTables()
        {
            if (string.IsNullOrWhiteSpace(SqlServerConn) || string.IsNullOrWhiteSpace(PostgreSqlConn))
            {
                Assert.Inconclusive(
                    "Set SQLSERVER_TEST_CONNECTION_STRING and POSTGRES_TEST_CONNECTION_STRING to run migration parity tests.");
            }

            var tablePairs = new List<(string SqlServerTable, string PostgreSqlTable)>
            {
                ("WebSite", "WebSite"),
                ("WebPage", "WebPage"),
                ("WebPageElement", "WebPageElement")
            };

            await using var sqlConnection = new SqlConnection(SqlServerConn);
            await using var pgConnection = new NpgsqlConnection(PostgreSqlConn);
            await sqlConnection.OpenAsync();
            await pgConnection.OpenAsync();

            foreach (var pair in tablePairs)
            {
                var sqlCount = await ExecuteScalarAsync(sqlConnection, $"SELECT COUNT(*) FROM [{pair.SqlServerTable}]");
                var pgCount = await ExecuteScalarAsync(pgConnection, $"SELECT COUNT(*) FROM \"{pair.PostgreSqlTable}\"");

                Assert.AreEqual(sqlCount, pgCount,
                    $"Row count mismatch for table pair {pair.SqlServerTable} / {pair.PostgreSqlTable}");
            }
        }

        private static async Task<long> ExecuteScalarAsync(DbConnection connection, string sql)
        {
            await using var command = connection.CreateCommand();
            command.CommandText = sql;
            var value = await command.ExecuteScalarAsync();
            return Convert.ToInt64(value);
        }
    }
}
