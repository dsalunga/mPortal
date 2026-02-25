using System;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace WCMS.Common.Utilities
{
    /// <summary>
    /// SQL Server implementation of <see cref="IDbHelper"/>.
    /// Wraps <see cref="SqlHelper"/> static methods with the database-agnostic interface.
    /// </summary>
    public class SqlServerDbHelper : IDbHelper
    {
        private readonly string _connString;
        private readonly int _timeOut;

        public DatabaseProvider Provider => DatabaseProvider.SqlServer;

        public SqlServerDbHelper(string connectionString, int timeOut = 200)
        {
            _connString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _timeOut = timeOut;
        }

        public DbConnection CreateConnection() => new SqlConnection(_connString);
        public DbConnection CreateConnection(string connectionString) => new SqlConnection(connectionString);

        public DbParameter CreateParameter(string name, object value) => new SqlParameter(name, value);

        public int ExecuteNonQuery(string connString, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            using var cmd = new SqlCommand();
            using var conn = new SqlConnection(connString);
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
            using var cmd = new SqlCommand();
            if (conn.State != ConnectionState.Open) conn.Open();
            using var trans = conn.BeginTransaction();
            try
            {
                PrepareCommand(cmd, (SqlConnection)conn, (SqlTransaction)trans, cmdType, cmdText, cmdParms);
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
            using var cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, (SqlConnection)trans.Connection, (SqlTransaction)trans, cmdType, cmdText, cmdParms);
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
            var cmd = new SqlCommand();
            var conn = new SqlConnection(connString);
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
            var cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, (SqlConnection)conn, null, cmdType, cmdText, cmdParms);
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
            using var cmd = new SqlCommand();
            using var conn = new SqlConnection(connString);
            if (conn.State != ConnectionState.Open) conn.Open();
            using var trans = conn.BeginTransaction();
            PrepareCommand(cmd, conn, trans, cmdType, cmdText, cmdParms);
            try
            {
                using var da = new SqlDataAdapter(cmd);
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
            using var cmd = new SqlCommand();
            using var conn = new SqlConnection(_connString);
            if (conn.State != ConnectionState.Open) conn.Open();
            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            using var da = new SqlDataAdapter(cmd);
            var ds = new DataSet();
            da.FillSchema(ds, SchemaType.Mapped);
            da.Fill(ds);
            cmd.Parameters.Clear();
            return ds;
        }

        public void ExecuteUpdateDataSet(string cmdText, DataTable table)
        {
            using var cmd = new SqlCommand();
            using var conn = new SqlConnection(_connString);
            if (conn.State != ConnectionState.Open) conn.Open();
            PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, null);
            using var da = new SqlDataAdapter(cmd);
            using var cmdBuilder = new SqlCommandBuilder(da);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            da.Update(table);
            cmd.Parameters.Clear();
        }

        public object ExecuteScalar(string connString, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            using var cmd = new SqlCommand();
            using var conn = new SqlConnection(connString);
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
            using var cmd = new SqlCommand();
            if (conn.State != ConnectionState.Open) conn.Open();
            using var trans = conn.BeginTransaction();
            try
            {
                PrepareCommand(cmd, (SqlConnection)conn, (SqlTransaction)trans, cmdType, cmdText, cmdParms);
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

        private void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open) conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = _timeOut;
            if (trans != null) cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (var parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
    }
}
