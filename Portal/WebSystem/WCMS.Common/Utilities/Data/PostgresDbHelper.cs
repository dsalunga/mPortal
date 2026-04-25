using System;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;
using Npgsql;

namespace WCMS.Common.Utilities
{
    /// <summary>
    /// PostgreSQL implementation of <see cref="IDbHelper"/>.
    /// Uses Npgsql for all database operations.
    /// </summary>
    public class PostgresDbHelper : IDbHelper
    {
        private static readonly Regex TableAfterKeywordRegex = new Regex(
            @"\b(FROM|JOIN|UPDATE|INTO)\s+([A-Za-z_][A-Za-z0-9_]*)\b",
            RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        private static readonly Regex DeleteFromRegex = new Regex(
            @"\bDELETE\s+FROM\s+([A-Za-z_][A-Za-z0-9_]*)\b",
            RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        private readonly string _connString;
        private readonly int _timeOut;

        public DatabaseProvider Provider => DatabaseProvider.PostgreSql;

        public PostgresDbHelper(string connectionString, int timeOut = 200)
        {
            _connString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _timeOut = timeOut;
        }

        public DbConnection CreateConnection() => new NpgsqlConnection(_connString);
        public DbConnection CreateConnection(string connectionString) => new NpgsqlConnection(connectionString);

        public DbParameter CreateParameter(string name, object value) => new NpgsqlParameter(name, value);

        public int ExecuteNonQuery(string connString, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            using var cmd = new NpgsqlCommand();
            using var conn = new NpgsqlConnection(connString);
            if (conn.State != ConnectionState.Open) conn.Open();
            using var trans = conn.BeginTransaction();
            try
            {
                PrepareCommand(cmd, conn, trans, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                trans.Commit();
                return val;
            }
            catch
            {
                trans.Rollback();
                throw;
            }
        }

        public int ExecuteNonQuery(CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
            => ExecuteNonQuery(_connString, cmdType, cmdText, cmdParms);

        public int ExecuteNonQuery(string cmdText, params DbParameter[] cmdParms)
            => ExecuteNonQuery(_connString, CommandType.StoredProcedure, cmdText, cmdParms);

        public int ExecuteNonQuery(DbConnection conn, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            using var cmd = new NpgsqlCommand();
            if (conn.State != ConnectionState.Open) conn.Open();
            using var trans = conn.BeginTransaction();
            try
            {
                PrepareCommand(cmd, (NpgsqlConnection)conn, (NpgsqlTransaction)trans, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                trans.Commit();
                return val;
            }
            catch
            {
                trans.Rollback();
                throw;
            }
        }

        public int ExecuteNonQuery(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            using var cmd = new NpgsqlCommand();
            try
            {
                PrepareCommand(cmd, (NpgsqlConnection)trans.Connection, (NpgsqlTransaction)trans, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                trans.Commit();
                return val;
            }
            catch
            {
                trans.Rollback();
                throw;
            }
        }

        public DbDataReader ExecuteReader(string connString, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            var cmd = new NpgsqlCommand();
            var conn = new NpgsqlConnection(connString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                var rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        public DbDataReader ExecuteReader(DbConnection conn, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            var cmd = new NpgsqlCommand();
            try
            {
                PrepareCommand(cmd, (NpgsqlConnection)conn, null, cmdType, cmdText, cmdParms);
                var rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        public DbDataReader ExecuteReader(CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
            => ExecuteReader(_connString, cmdType, cmdText, cmdParms);

        public DbDataReader ExecuteReader(string cmdText, params DbParameter[] cmdParms)
            => ExecuteReader(_connString, cmdText, cmdParms);

        public DbDataReader ExecuteReader(string connString, string cmdText, params DbParameter[] cmdParms)
            => ExecuteReader(connString, CommandType.StoredProcedure, cmdText, cmdParms);

        public DataSet ExecuteDataSet(string connString, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            using var conn = new NpgsqlConnection(connString);
            if (conn.State != ConnectionState.Open) conn.Open();
            using var trans = conn.BeginTransaction();
            using var cmd = new NpgsqlCommand();
            PrepareCommand(cmd, conn, trans, cmdType, cmdText, cmdParms);
            try
            {
                using var da = new NpgsqlDataAdapter(cmd);
                var ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                trans.Commit();
                return ds;
            }
            catch
            {
                trans.Rollback();
                throw;
            }
        }

        public DataSet ExecuteDataSet(CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
            => ExecuteDataSet(_connString, cmdType, cmdText, cmdParms);

        public DataSet ExecuteDataSet(string cmdText, params DbParameter[] cmdParms)
            => ExecuteDataSet(_connString, CommandType.StoredProcedure, cmdText, cmdParms);

        public DataSet ExecuteDataSetSchema(CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            using var conn = new NpgsqlConnection(_connString);
            if (conn.State != ConnectionState.Open) conn.Open();
            using var cmd = new NpgsqlCommand();
            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            using var da = new NpgsqlDataAdapter(cmd);
            var ds = new DataSet();
            da.FillSchema(ds, SchemaType.Mapped);
            da.Fill(ds);
            cmd.Parameters.Clear();
            return ds;
        }

        public void ExecuteUpdateDataSet(string cmdText, DataTable table)
        {
            using var conn = new NpgsqlConnection(_connString);
            if (conn.State != ConnectionState.Open) conn.Open();
            using var cmd = new NpgsqlCommand();
            PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, null);
            using var da = new NpgsqlDataAdapter(cmd);
            using var cmdBuilder = new NpgsqlCommandBuilder(da);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            da.Update(table);
            cmd.Parameters.Clear();
        }

        public object ExecuteScalar(string connString, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            using var cmd = new NpgsqlCommand();
            using var conn = new NpgsqlConnection(connString);
            if (conn.State != ConnectionState.Open) conn.Open();
            using var trans = conn.BeginTransaction();
            try
            {
                PrepareCommand(cmd, conn, trans, cmdType, cmdText, cmdParms);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                trans.Commit();
                return val;
            }
            catch
            {
                trans.Rollback();
                throw;
            }
        }

        public object ExecuteScalar(CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
            => ExecuteScalar(_connString, cmdType, cmdText, cmdParms);

        public object ExecuteScalar(string connString, string cmdText, params DbParameter[] cmdParms)
            => ExecuteScalar(connString, CommandType.StoredProcedure, cmdText, cmdParms);

        public object ExecuteScalar(string cmdText, params DbParameter[] cmdParms)
            => ExecuteScalar(_connString, cmdText, cmdParms);

        public object ExecuteScalar(DbConnection conn, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            using var cmd = new NpgsqlCommand();
            if (conn.State != ConnectionState.Open) conn.Open();
            using var trans = conn.BeginTransaction();
            try
            {
                PrepareCommand(cmd, (NpgsqlConnection)conn, (NpgsqlTransaction)trans, cmdType, cmdText, cmdParms);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                trans.Commit();
                return val;
            }
            catch
            {
                trans.Rollback();
                throw;
            }
        }

        private void PrepareCommand(NpgsqlCommand cmd, NpgsqlConnection conn, NpgsqlTransaction trans, CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open) conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = NormalizeSqlTextForPostgres(cmdType, cmdText);
            cmd.CommandTimeout = _timeOut;
            if (trans != null) cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (var parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        private static string NormalizeSqlTextForPostgres(CommandType cmdType, string cmdText)
        {
            if (cmdType != CommandType.Text || string.IsNullOrWhiteSpace(cmdText))
                return cmdText;

            var result = TableAfterKeywordRegex.Replace(cmdText, match =>
            {
                var keyword = match.Groups[1].Value;
                var table = match.Groups[2].Value;
                return keyword + " " + QuoteIdentifierIfNeeded(table);
            });

            result = DeleteFromRegex.Replace(result, match =>
            {
                var table = match.Groups[1].Value;
                return "DELETE FROM " + QuoteIdentifierIfNeeded(table);
            });

            return result;
        }

        private static string QuoteIdentifierIfNeeded(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
                return identifier;

            if (identifier.StartsWith("\"", StringComparison.Ordinal) &&
                identifier.EndsWith("\"", StringComparison.Ordinal))
                return identifier;

            foreach (var ch in identifier)
            {
                if (char.IsUpper(ch))
                {
                    return "\"" + identifier + "\"";
                }
            }

            return identifier;
        }
    }
}
