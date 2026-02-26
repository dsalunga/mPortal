using System.Data;
using System.Data.Common;

namespace WCMS.Common.Utilities
{
    /// <summary>
    /// Database-agnostic interface for common data operations.
    /// Implementations wrap provider-specific ADO.NET classes (SQL Server, PostgreSQL, etc.).
    /// </summary>
    public interface IDbHelper
    {
        DatabaseProvider Provider { get; }

        DbConnection CreateConnection();
        DbConnection CreateConnection(string connectionString);
        DbParameter CreateParameter(string name, object value);

        int ExecuteNonQuery(string connString, CommandType cmdType, string cmdText, params DbParameter[] cmdParms);
        int ExecuteNonQuery(CommandType cmdType, string cmdText, params DbParameter[] cmdParms);
        int ExecuteNonQuery(string cmdText, params DbParameter[] cmdParms);
        int ExecuteNonQuery(DbConnection conn, CommandType cmdType, string cmdText, params DbParameter[] cmdParms);
        int ExecuteNonQuery(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] cmdParms);

        DbDataReader ExecuteReader(string connString, CommandType cmdType, string cmdText, params DbParameter[] cmdParms);
        DbDataReader ExecuteReader(DbConnection conn, CommandType cmdType, string cmdText, params DbParameter[] cmdParms);
        DbDataReader ExecuteReader(CommandType cmdType, string cmdText, params DbParameter[] cmdParms);
        DbDataReader ExecuteReader(string cmdText, params DbParameter[] cmdParms);
        DbDataReader ExecuteReader(string connString, string cmdText, params DbParameter[] cmdParms);

        DataSet ExecuteDataSet(string connString, CommandType cmdType, string cmdText, params DbParameter[] cmdParms);
        DataSet ExecuteDataSet(CommandType cmdType, string cmdText, params DbParameter[] cmdParms);
        DataSet ExecuteDataSet(string cmdText, params DbParameter[] cmdParms);

        DataSet ExecuteDataSetSchema(CommandType cmdType, string cmdText, params DbParameter[] cmdParms);
        void ExecuteUpdateDataSet(string cmdText, DataTable table);

        object ExecuteScalar(string connString, CommandType cmdType, string cmdText, params DbParameter[] cmdParms);
        object ExecuteScalar(CommandType cmdType, string cmdText, params DbParameter[] cmdParms);
        object ExecuteScalar(string connString, string cmdText, params DbParameter[] cmdParms);
        object ExecuteScalar(string cmdText, params DbParameter[] cmdParms);
        object ExecuteScalar(DbConnection conn, CommandType cmdType, string cmdText, params DbParameter[] cmdParms);
    }
}
