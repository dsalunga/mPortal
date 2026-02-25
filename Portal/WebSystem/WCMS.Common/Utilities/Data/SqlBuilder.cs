using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace WCMS.Common.Utilities
{
    /// <summary>
    /// Builds cross-database parameterized SQL statements for common CRUD operations.
    /// Uses <see cref="DbSyntax"/> for provider-aware identifier quoting and
    /// <see cref="DbHelper"/> for parameter creation.
    /// </summary>
    public class SqlBuilder
    {
        private readonly string _table;
        private readonly List<(string Column, string Param, object Value)> _columns = new();
        private readonly List<(string Column, string Param, object Value)> _where = new();

        public SqlBuilder(string table)
        {
            _table = table;
        }

        /// <summary>
        /// Add a column-value pair to the INSERT/UPDATE set clause.
        /// </summary>
        public SqlBuilder Set(string column, string paramName, object value)
        {
            _columns.Add((column, paramName, value));
            return this;
        }

        /// <summary>
        /// Add a WHERE condition.
        /// </summary>
        public SqlBuilder Where(string column, string paramName, object value)
        {
            _where.Add((column, paramName, value));
            return this;
        }

        /// <summary>
        /// Builds a SELECT * FROM table WHERE ... statement.
        /// If no WHERE clauses, returns SELECT * FROM table.
        /// </summary>
        public (string Sql, DbParameter[] Parameters) BuildSelect()
        {
            var sb = new StringBuilder();
            sb.Append("SELECT * FROM ").Append(_table);
            var parms = new List<DbParameter>();

            if (_where.Count > 0)
            {
                sb.Append(" WHERE ");
                for (int i = 0; i < _where.Count; i++)
                {
                    if (i > 0) sb.Append(" AND ");
                    var w = _where[i];
                    sb.Append(DbSyntax.QuoteIdentifier(w.Column)).Append(" = @").Append(w.Param);
                    parms.Add(DbHelper.CreateParameter("@" + w.Param, w.Value));
                }
            }

            return (sb.ToString(), parms.ToArray());
        }

        /// <summary>
        /// Builds a SELECT with specific columns.
        /// </summary>
        public (string Sql, DbParameter[] Parameters) BuildSelect(params string[] columns)
        {
            var sb = new StringBuilder();
            sb.Append("SELECT ");
            sb.Append(string.Join(", ", columns.Select(c => DbSyntax.QuoteIdentifier(c))));
            sb.Append(" FROM ").Append(_table);
            var parms = new List<DbParameter>();

            if (_where.Count > 0)
            {
                sb.Append(" WHERE ");
                for (int i = 0; i < _where.Count; i++)
                {
                    if (i > 0) sb.Append(" AND ");
                    var w = _where[i];
                    sb.Append(DbSyntax.QuoteIdentifier(w.Column)).Append(" = @").Append(w.Param);
                    parms.Add(DbHelper.CreateParameter("@" + w.Param, w.Value));
                }
            }

            return (sb.ToString(), parms.ToArray());
        }

        /// <summary>
        /// Builds a SELECT COUNT(1) FROM table WHERE ... statement.
        /// </summary>
        public (string Sql, DbParameter[] Parameters) BuildCount()
        {
            var sb = new StringBuilder();
            sb.Append("SELECT COUNT(1) FROM ").Append(_table);
            var parms = new List<DbParameter>();

            if (_where.Count > 0)
            {
                sb.Append(" WHERE ");
                for (int i = 0; i < _where.Count; i++)
                {
                    if (i > 0) sb.Append(" AND ");
                    var w = _where[i];
                    sb.Append(DbSyntax.QuoteIdentifier(w.Column)).Append(" = @").Append(w.Param);
                    parms.Add(DbHelper.CreateParameter("@" + w.Param, w.Value));
                }
            }

            return (sb.ToString(), parms.ToArray());
        }

        /// <summary>
        /// Builds a SELECT MAX(column) FROM table WHERE ... statement.
        /// </summary>
        public (string Sql, DbParameter[] Parameters) BuildMax(string column)
        {
            var sb = new StringBuilder();
            sb.Append("SELECT MAX(").Append(DbSyntax.QuoteIdentifier(column)).Append(") FROM ").Append(_table);
            var parms = new List<DbParameter>();

            if (_where.Count > 0)
            {
                sb.Append(" WHERE ");
                for (int i = 0; i < _where.Count; i++)
                {
                    if (i > 0) sb.Append(" AND ");
                    var w = _where[i];
                    sb.Append(DbSyntax.QuoteIdentifier(w.Column)).Append(" = @").Append(w.Param);
                    parms.Add(DbHelper.CreateParameter("@" + w.Param, w.Value));
                }
            }

            return (sb.ToString(), parms.ToArray());
        }

        /// <summary>
        /// Builds a DELETE FROM table WHERE ... statement.
        /// </summary>
        public (string Sql, DbParameter[] Parameters) BuildDelete()
        {
            var sb = new StringBuilder();
            sb.Append("DELETE FROM ").Append(_table);
            var parms = new List<DbParameter>();

            if (_where.Count > 0)
            {
                sb.Append(" WHERE ");
                for (int i = 0; i < _where.Count; i++)
                {
                    if (i > 0) sb.Append(" AND ");
                    var w = _where[i];
                    sb.Append(DbSyntax.QuoteIdentifier(w.Column)).Append(" = @").Append(w.Param);
                    parms.Add(DbHelper.CreateParameter("@" + w.Param, w.Value));
                }
            }

            return (sb.ToString(), parms.ToArray());
        }

        /// <summary>
        /// Builds an INSERT statement with a RETURNING/OUTPUT clause for the identity column.
        /// Returns the new ID from the database.
        /// </summary>
        public (string Sql, DbParameter[] Parameters) BuildInsert(string idColumn = null)
        {
            var sb = new StringBuilder();
            var parms = new List<DbParameter>();

            sb.Append("INSERT INTO ").Append(_table).Append(" (");
            sb.Append(string.Join(", ", _columns.Select(c => DbSyntax.QuoteIdentifier(c.Column))));
            sb.Append(") VALUES (");
            sb.Append(string.Join(", ", _columns.Select(c => "@" + c.Param)));
            sb.Append(")");

            // Add identity return clause if idColumn specified
            if (!string.IsNullOrEmpty(idColumn))
            {
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sb.Append(" RETURNING ").Append(DbSyntax.QuoteIdentifier(idColumn));
                else
                    sb.Append("; SELECT SCOPE_IDENTITY()");
            }

            foreach (var c in _columns)
                parms.Add(DbHelper.CreateParameter("@" + c.Param, c.Value));

            return (sb.ToString(), parms.ToArray());
        }

        /// <summary>
        /// Builds an UPDATE table SET ... WHERE ... statement.
        /// </summary>
        public (string Sql, DbParameter[] Parameters) BuildUpdate()
        {
            var sb = new StringBuilder();
            var parms = new List<DbParameter>();

            sb.Append("UPDATE ").Append(_table).Append(" SET ");
            sb.Append(string.Join(", ", _columns.Select(c =>
                DbSyntax.QuoteIdentifier(c.Column) + " = @" + c.Param)));

            if (_where.Count > 0)
            {
                sb.Append(" WHERE ");
                for (int i = 0; i < _where.Count; i++)
                {
                    if (i > 0) sb.Append(" AND ");
                    var w = _where[i];
                    sb.Append(DbSyntax.QuoteIdentifier(w.Column)).Append(" = @").Append(w.Param);
                    parms.Add(DbHelper.CreateParameter("@" + w.Param, w.Value));
                }
            }

            foreach (var c in _columns)
                parms.Add(DbHelper.CreateParameter("@" + c.Param, c.Value));

            return (sb.ToString(), parms.ToArray());
        }

        /// <summary>
        /// Builds an INSERT or UPDATE based on whether the id value is positive.
        /// For INSERT, appends "; SELECT SCOPE_IDENTITY()" (SQL Server) or "RETURNING id" (PostgreSQL).
        /// Returns (sql, parameters, isInsert).
        /// </summary>
        public (string Sql, DbParameter[] Parameters, bool IsInsert) BuildUpsert(string idColumn, object idValue)
        {
            int idInt = idValue is int i ? i : 0;
            bool isInsert = idInt <= 0;

            if (isInsert)
            {
                var (sql, parms) = BuildInsert();
                string fullSql;
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    fullSql = sql + " RETURNING " + DbSyntax.QuoteIdentifier(idColumn);
                else
                    fullSql = sql + "; SELECT SCOPE_IDENTITY()";
                return (fullSql, parms, true);
            }
            else
            {
                Where(idColumn, idColumn, idValue);
                var (sql, parms) = BuildUpdate();
                return (sql, parms, false);
            }
        }

        // --- Static convenience methods ---

        /// <summary>
        /// Create a new SqlBuilder for the given table.
        /// </summary>
        public static SqlBuilder For(string table) => new SqlBuilder(table);
    }
}
