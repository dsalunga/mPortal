using System;
using System.Data;
using System.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCMS.Common.Utilities;

namespace WCMS.Framework.UnitTests
{
    [TestClass]
    public class DbHelperTests
    {
        [TestCleanup]
        public void Cleanup()
        {
            // Reset DbHelper state between tests using reflection
            var field = typeof(DbHelper).GetField("_instance",
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            field?.SetValue(null, null);
        }

        [TestMethod]
        public void ParseProvider_SqlServer_ReturnsSqlServer()
        {
            Assert.AreEqual(DatabaseProvider.SqlServer, DbHelper.ParseProvider("SqlServer"));
        }

        [TestMethod]
        public void ParseProvider_PostgreSql_ReturnsPostgreSql()
        {
            Assert.AreEqual(DatabaseProvider.PostgreSql, DbHelper.ParseProvider("PostgreSql"));
        }

        [TestMethod]
        public void ParseProvider_Postgres_ReturnsPostgreSql()
        {
            Assert.AreEqual(DatabaseProvider.PostgreSql, DbHelper.ParseProvider("postgres"));
        }

        [TestMethod]
        public void ParseProvider_Npgsql_ReturnsPostgreSql()
        {
            Assert.AreEqual(DatabaseProvider.PostgreSql, DbHelper.ParseProvider("npgsql"));
        }

        [TestMethod]
        public void ParseProvider_Pgsql_ReturnsPostgreSql()
        {
            Assert.AreEqual(DatabaseProvider.PostgreSql, DbHelper.ParseProvider("pgsql"));
        }

        [TestMethod]
        public void ParseProvider_Mssql_ReturnsSqlServer()
        {
            Assert.AreEqual(DatabaseProvider.SqlServer, DbHelper.ParseProvider("mssql"));
        }

        [TestMethod]
        public void ParseProvider_NullOrEmpty_DefaultsSqlServer()
        {
            Assert.AreEqual(DatabaseProvider.SqlServer, DbHelper.ParseProvider(null));
            Assert.AreEqual(DatabaseProvider.SqlServer, DbHelper.ParseProvider(""));
            Assert.AreEqual(DatabaseProvider.SqlServer, DbHelper.ParseProvider("   "));
        }

        [TestMethod]
        public void ParseProvider_Unknown_DefaultsSqlServer()
        {
            Assert.AreEqual(DatabaseProvider.SqlServer, DbHelper.ParseProvider("unknown"));
        }

        [TestMethod]
        public void ParseProvider_CaseInsensitive()
        {
            Assert.AreEqual(DatabaseProvider.PostgreSql, DbHelper.ParseProvider("POSTGRESQL"));
            Assert.AreEqual(DatabaseProvider.SqlServer, DbHelper.ParseProvider("SQLSERVER"));
            Assert.AreEqual(DatabaseProvider.PostgreSql, DbHelper.ParseProvider("PostgreSQL"));
        }

        [TestMethod]
        public void ConfigureFromSettings_SqlServer_ConfiguresCorrectly()
        {
            DbHelper.ConfigureFromSettings(DatabaseProvider.SqlServer, "Server=test;Database=test;", 100);

            Assert.IsTrue(DbHelper.IsConfigured);
            Assert.AreEqual(DatabaseProvider.SqlServer, DbHelper.Provider);
            Assert.AreEqual("Server=test;Database=test;", DbHelper.ConnString);
        }

        [TestMethod]
        public void ConfigureFromSettings_PostgreSql_ConfiguresCorrectly()
        {
            DbHelper.ConfigureFromSettings(DatabaseProvider.PostgreSql, "Host=localhost;Database=test;", 100);

            Assert.IsTrue(DbHelper.IsConfigured);
            Assert.AreEqual(DatabaseProvider.PostgreSql, DbHelper.Provider);
            Assert.AreEqual("Host=localhost;Database=test;", DbHelper.ConnString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConfigureFromSettings_NullConnectionString_Throws()
        {
            DbHelper.ConfigureFromSettings(DatabaseProvider.SqlServer, null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Instance_NotConfigured_Throws()
        {
            _ = DbHelper.Instance;
        }

        [TestMethod]
        public void IsConfigured_BeforeConfigure_ReturnsFalse()
        {
            Assert.IsFalse(DbHelper.IsConfigured);
        }

        [TestMethod]
        public void CreateParameter_SqlServer_ReturnsSqlParameter()
        {
            DbHelper.ConfigureFromSettings(DatabaseProvider.SqlServer, "Server=test;Database=test;");
            var param = DbHelper.CreateParameter("@Id", 42);

            Assert.IsNotNull(param);
            Assert.AreEqual("@Id", param.ParameterName);
            Assert.AreEqual(42, param.Value);
            Assert.IsInstanceOfType(param, typeof(Microsoft.Data.SqlClient.SqlParameter));
        }

        [TestMethod]
        public void CreateParameter_PostgreSql_ReturnsNpgsqlParameter()
        {
            DbHelper.ConfigureFromSettings(DatabaseProvider.PostgreSql, "Host=localhost;Database=test;");
            var param = DbHelper.CreateParameter("@Id", 42);

            Assert.IsNotNull(param);
            Assert.AreEqual("@Id", param.ParameterName);
            Assert.AreEqual(42, param.Value);
            Assert.IsInstanceOfType(param, typeof(Npgsql.NpgsqlParameter));
        }

        [TestMethod]
        public void CreateConnection_SqlServer_ReturnsSqlConnection()
        {
            DbHelper.ConfigureFromSettings(DatabaseProvider.SqlServer, "Server=test;Database=test;");
            using var conn = DbHelper.CreateConnection();
            Assert.IsInstanceOfType(conn, typeof(Microsoft.Data.SqlClient.SqlConnection));
        }

        [TestMethod]
        public void CreateConnection_PostgreSql_ReturnsNpgsqlConnection()
        {
            DbHelper.ConfigureFromSettings(DatabaseProvider.PostgreSql, "Host=localhost;Database=test;");
            using var conn = DbHelper.CreateConnection();
            Assert.IsInstanceOfType(conn, typeof(Npgsql.NpgsqlConnection));
        }

        [TestMethod]
        public void Configure_WithIDbHelper_SetsInstance()
        {
            var helper = new SqlServerDbHelper("Server=test;Database=test;");
            DbHelper.Configure(helper, "Server=test;Database=test;");

            Assert.IsTrue(DbHelper.IsConfigured);
            Assert.AreSame(helper, DbHelper.Instance);
        }
    }

    [TestClass]
    public class DbSyntaxTests
    {
        [TestMethod]
        public void QuoteIdentifier_SqlServer_UsesBrackets()
        {
            var result = DbSyntax.QuoteIdentifier("ColumnName", DatabaseProvider.SqlServer);
            Assert.AreEqual("[ColumnName]", result);
        }

        [TestMethod]
        public void QuoteIdentifier_PostgreSql_UsesDoubleQuotes()
        {
            var result = DbSyntax.QuoteIdentifier("ColumnName", DatabaseProvider.PostgreSql);
            Assert.AreEqual("\"ColumnName\"", result);
        }

        [TestMethod]
        public void ParameterPrefix_IsAt()
        {
            Assert.AreEqual("@", DbSyntax.ParameterPrefix);
        }
    }

    [TestClass]
    public class DatabaseProviderEnumTests
    {
        [TestMethod]
        public void DatabaseProvider_HasSqlServer()
        {
            Assert.IsTrue(Enum.IsDefined(typeof(DatabaseProvider), DatabaseProvider.SqlServer));
        }

        [TestMethod]
        public void DatabaseProvider_HasPostgreSql()
        {
            Assert.IsTrue(Enum.IsDefined(typeof(DatabaseProvider), DatabaseProvider.PostgreSql));
        }
    }
}
