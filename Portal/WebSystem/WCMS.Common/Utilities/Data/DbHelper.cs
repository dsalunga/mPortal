using System;
using System.Data;
using System.Data.Common;

namespace WCMS.Common.Utilities
{
    /// <summary>
    /// Static factory and access point for the configured <see cref="IDbHelper"/>.
    /// Replaces direct <see cref="SqlHelper"/> usage for cross-database support.
    /// Configure once at startup via <see cref="Configure"/> or <see cref="ConfigureFromSettings"/>.
    /// </summary>
    public static class DbHelper
    {
        private static IDbHelper _instance;

        /// <summary>
        /// The configured database helper instance. Throws if not configured.
        /// </summary>
        public static IDbHelper Instance
        {
            get
            {
                if (_instance == null)
                    throw new InvalidOperationException(
                        "DbHelper is not configured. Call DbHelper.Configure() or DbHelper.ConfigureFromSettings() at startup.");
                return _instance;
            }
        }

        /// <summary>
        /// The active database provider type.
        /// </summary>
        public static DatabaseProvider Provider => Instance.Provider;

        /// <summary>
        /// The default connection string.
        /// </summary>
        public static string ConnString { get; private set; }

        /// <summary>
        /// Whether the DbHelper has been configured.
        /// </summary>
        public static bool IsConfigured => _instance != null;

        /// <summary>
        /// Configure DbHelper with a specific <see cref="IDbHelper"/> implementation.
        /// </summary>
        public static void Configure(IDbHelper helper, string connectionString)
        {
            _instance = helper ?? throw new ArgumentNullException(nameof(helper));
            ConnString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        /// <summary>
        /// Configure DbHelper from a provider enum and connection string.
        /// </summary>
        public static void ConfigureFromSettings(DatabaseProvider provider, string connectionString, int timeOut = 200)
        {
            ConnString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _instance = provider switch
            {
                DatabaseProvider.SqlServer => new SqlServerDbHelper(connectionString, timeOut),
                DatabaseProvider.PostgreSql => new PostgresDbHelper(connectionString, timeOut),
                _ => throw new ArgumentOutOfRangeException(nameof(provider), provider, "Unsupported database provider")
            };
        }

        /// <summary>
        /// Parse a provider name string to <see cref="DatabaseProvider"/> enum.
        /// Defaults to <see cref="DatabaseProvider.SqlServer"/> if null/empty.
        /// </summary>
        public static DatabaseProvider ParseProvider(string providerName)
        {
            if (string.IsNullOrWhiteSpace(providerName))
                return DatabaseProvider.SqlServer;

            return providerName.Trim().ToLowerInvariant() switch
            {
                "postgresql" or "postgres" or "npgsql" or "pgsql" => DatabaseProvider.PostgreSql,
                "sqlserver" or "mssql" or "sql" => DatabaseProvider.SqlServer,
                _ => Enum.TryParse<DatabaseProvider>(providerName, ignoreCase: true, out var parsed)
                    ? parsed
                    : DatabaseProvider.SqlServer
            };
        }

        // --- Convenience static methods delegating to Instance ---

        public static DbConnection CreateConnection() => Instance.CreateConnection();
        public static DbConnection CreateConnection(string connectionString) => Instance.CreateConnection(connectionString);
        public static DbParameter CreateParameter(string name, object value) => Instance.CreateParameter(name, value);

        public static int ExecuteNonQuery(string connString, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
            => Instance.ExecuteNonQuery(connString, cmdType, cmdText, cmdParms);

        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
            => Instance.ExecuteNonQuery(cmdType, cmdText, cmdParms);

        public static int ExecuteNonQuery(string cmdText, params DbParameter[] cmdParms)
            => Instance.ExecuteNonQuery(cmdText, cmdParms);

        public static DbDataReader ExecuteReader(string connString, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
            => Instance.ExecuteReader(connString, cmdType, cmdText, cmdParms);

        public static DbDataReader ExecuteReader(CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
            => Instance.ExecuteReader(cmdType, cmdText, cmdParms);

        public static DbDataReader ExecuteReader(string cmdText, params DbParameter[] cmdParms)
            => Instance.ExecuteReader(cmdText, cmdParms);

        public static DbDataReader ExecuteReader(string connString, string cmdText, params DbParameter[] cmdParms)
            => Instance.ExecuteReader(connString, cmdText, cmdParms);

        public static object ExecuteScalar(string connString, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
            => Instance.ExecuteScalar(connString, cmdType, cmdText, cmdParms);

        public static object ExecuteScalar(CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
            => Instance.ExecuteScalar(cmdType, cmdText, cmdParms);

        public static object ExecuteScalar(string cmdText, params DbParameter[] cmdParms)
            => Instance.ExecuteScalar(cmdText, cmdParms);

        public static DataSet ExecuteDataSet(CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
            => Instance.ExecuteDataSet(cmdType, cmdText, cmdParms);

        public static DataSet ExecuteDataSetSchema(CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
            => Instance.ExecuteDataSetSchema(cmdType, cmdText, cmdParms);

        public static void ExecuteUpdateDataSet(string cmdText, DataTable table)
            => Instance.ExecuteUpdateDataSet(cmdText, table);
    }
}
