namespace WCMS.Common.Utilities
{
    /// <summary>
    /// Provides database-agnostic SQL syntax helpers.
    /// Handles differences in identifier quoting, parameter prefixes, etc.
    /// between SQL Server and PostgreSQL.
    /// </summary>
    public static class DbSyntax
    {
        /// <summary>
        /// Quote a column or table identifier for the active database provider.
        /// SQL Server uses [name], PostgreSQL uses "name".
        /// </summary>
        public static string QuoteIdentifier(string name)
        {
            return QuoteIdentifier(name, DbHelper.Provider);
        }

        /// <summary>
        /// Quote a column or table identifier for the specified database provider.
        /// SQL Server uses [name], PostgreSQL uses "name".
        /// </summary>
        public static string QuoteIdentifier(string name, DatabaseProvider provider)
        {
            return provider switch
            {
                DatabaseProvider.SqlServer => $"[{name}]",
                DatabaseProvider.PostgreSql => $"\"{name}\"",
                _ => $"[{name}]"
            };
        }

        /// <summary>
        /// Returns the parameter prefix for the active database provider.
        /// Both SQL Server and PostgreSQL support @, but this is here for extensibility.
        /// </summary>
        public static string ParameterPrefix => "@";
    }
}
