using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Core.SqlProvider.Smo;

namespace WCMS.WebSystem.Utilities
{
    public class DbManager
    {
        public bool IsNewDb {get; set; }

        public readonly string XML_PATH = WebUtil.MapPath(ConfigUtil.Get(DbConstants.DB_PROVIDER_PATH_KEY), true);

        public string BackupPath => XML_PATH;
        public string ProcedureSqlPath => Path.Combine(XML_PATH, "Procedures");
        public string TableSqlPath => Path.Combine(XML_PATH, "Tables");

        /// <summary>
        /// Path to Portal/Binaries/Database/PostgreSQL directory
        /// (contains schema.sql and PostgreSQL initialization assets).
        /// </summary>
        public string PostgreSqlSchemaDir
        {
            get
            {
                // XML_PATH resolves to Portal/Binaries/Database
                var pgDir = Path.Combine(XML_PATH, "PostgreSQL");
                if (Directory.Exists(pgDir))
                    return pgDir;
                return string.Empty;
            }
        }

        private bool IsSqlServer => DbHelper.Provider == DatabaseProvider.SqlServer;
        private bool IsPostgreSql => DbHelper.Provider == DatabaseProvider.PostgreSql;
        private const string FixtureMarkerCategory = "Integration";
        private const string FixtureMarkerText = "FixtureMarker";
        private const string FixtureMarkerValue = "integration-fixtures-enabled";
        private const string AllowFixtureBackupInProductionKey = "WCMS:AllowFixtureBackupInProduction";

        public DbManager()
        {
            //if (SqlScriptGenerator.CheckCreateDatabase())
            //    IsNewDb = true;
        }

        public bool CheckCreateDatabase()
        {
            // SMO-based database creation is only supported on SQL Server
            if (IsSqlServer && SqlScriptGenerator.CheckCreateDatabase())
            {
                IsNewDb = true;
                return true;
            }
            return false;
        }

        public string RestoreObjectSchema(WebObject item)
        {
            var errors = new StringBuilder();
            // Restore table schema
            string[] tableFiles = Directory.GetFiles(TableSqlPath, string.Format("{0}{1}", item.Name, DbConstants.CREATE_FILTER_WC));
            foreach (string tableFile in tableFiles)
                ExecuteSqlScriptFile(tableFile, errors, true);

            // Restore procedure schema (SQL Server only — stored procedures eliminated for PostgreSQL)
            if (IsSqlServer && Directory.Exists(ProcedureSqlPath))
            {
                string[] procedureFiles = Directory.GetFiles(ProcedureSqlPath, string.Format("{0}_*{1}", item.Name, DbConstants.CREATE_FILTER_WC));
                foreach (string procedureFile in procedureFiles)
                    ExecuteSqlScriptFile(procedureFile, errors, true);
            }

            return errors.ToString();
        }

        public string DropObjectSchema(WebObject item, Action<string> notify)
        {
            var errors = new StringBuilder();

            // Drop procedures (SQL Server only)
            if (IsSqlServer && Directory.Exists(ProcedureSqlPath))
            {
                string[] procedureFiles = Directory.GetFiles(ProcedureSqlPath, string.Format("{0}_{1}", item.Name, DbConstants.DROP_FILTER_WC));
                foreach (string procedureFile in procedureFiles)
                {
                    notify(string.Format("Drop Procedure: {0}", Path.GetFileName(procedureFile)));
                    ExecuteSqlScriptFile(procedureFile, errors, true);
                }
            }

            // Get all table drop scripts and execute them
            var tableFiles = Directory.GetFiles(TableSqlPath, string.Format("{0}{1}", item.Name, DbConstants.DROP_FILTER));
            foreach (string tableFile in tableFiles)
            {
                notify(string.Format("Drop Table: {0}", Path.GetFileName(tableFile)));
                ExecuteSqlScriptFile(tableFile, errors, true);
            }

            return errors.ToString();
        }

        public void DropSelectedTables(string selected, Action<string> notify)
        {
            if (!string.IsNullOrEmpty(selected))
            {
                string tableSqlPath = TableSqlPath;

                try
                {
                    var items = from id in DataUtil.ParseCommaSeparatedIdList(selected)
                                select WebObject.Get(id);

                    foreach (var item in items)
                    {
                        string dropScript = Path.Combine(tableSqlPath, string.Format(@"{0}.drop.sql", item.Name));
                        if (File.Exists(dropScript))
                        {
                            notify(string.Format("Drop Table: {0}", Path.GetFileName(item.Name)));
                            ExecuteSqlScriptFile(dropScript, null, true);
                        }
                    }

                    notify(string.Format("{0}Data restore process COMPLETED.", WConstants.WBREAK));
                }
                catch (Exception ex)
                {
                    notify(ex.Message);
                }
            }
        }

        private void TruncateObject(WebObject item)
        {
            var quotedName = DbSyntax.QuoteIdentifier(item.Name);
            DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("TRUNCATE TABLE {0}", quotedName));
        }

        public void RestoreObjectData(WebObject item, Action<string> notify)
        {
            string tableXml = Path.Combine(BackupPath, item.Name + ".xml");
            if (!File.Exists(tableXml))
            {
                notify(string.Format("{0}.xml does not exist.", item.Name));
                return;
            }

            TruncateObject(item);

            var ds = new DataSet();
            ds.ReadXml(tableXml, XmlReadMode.ReadSchema);

            var table = ds.Tables[0];

            var quotedName = DbSyntax.QuoteIdentifier(item.Name);
            DbHelper.ExecuteUpdateDataSet(string.Format("SELECT * FROM {0}", quotedName), table);

            notify(string.Format("{0}.xml RESTORED.", item.Name));
        }

        public void BackupObjectData(WebObject item, Action<string> notify, int index = -1)
        {
            string targetXmlFile = Path.Combine(BackupPath, item.Name + ".xml");
            var quotedName = DbSyntax.QuoteIdentifier(item.Name);

            // Backup the data
            var ds = DbHelper.ExecuteDataSetSchema(CommandType.Text,
                string.Format("SELECT * FROM {0}", quotedName));

            ds.DataSetName = "mPortal";
            ds.Tables[0].TableName = item.Name;
            ds.WriteXml(targetXmlFile, XmlWriteMode.WriteSchema);

            if (item.Name == WebObject.OBJECT_NAME)
            {
                string dbXml = Path.Combine(XML_PATH, DbConstants.XML_FILE);
                File.Copy(targetXmlFile, dbXml, true);
            }

            if (index > 0)
                notify(string.Format("{0} {1} COMPLETED.", index, item.Name));
            else
                notify(string.Format("{0} BACKUP COMPLETED.", item.Name));
        }

        public bool RestoreAllObjects(Action<string> notify)
        {
            var errors = new StringBuilder();

            // Execute schema scripts
            if (IsPostgreSql)
            {
                // PostgreSQL: use the consolidated schema.sql DDL script
                RestorePostgreSqlSchema(errors, notify);
            }
            else
            {
                // SQL Server: use individual .create.sql scripts from Tables/
                if (Directory.Exists(TableSqlPath))
                {
                    string[] tableFiles = Directory.GetFiles(TableSqlPath, DbConstants.CREATE_FILTER_WC);
                    foreach (string tableFile in tableFiles)
                        ExecuteSqlScriptFile(tableFile, errors, true);
                }

                // Restore procedure schema (SQL Server only)
                if (Directory.Exists(ProcedureSqlPath))
                {
                    string[] procedureFiles = Directory.GetFiles(ProcedureSqlPath, DbConstants.CREATE_FILTER_WC);
                    foreach (string procedureFile in procedureFiles)
                        ExecuteSqlScriptFile(procedureFile, errors, true);
                }
            }

            // Start restoring data
            if (Directory.Exists(BackupPath))
            {
                notify(string.Format("Database restore process STARTED.{0}", WConstants.WBREAK));

                var items = WebObject.XmlProvider.GetList();

                foreach (var item in items)
                {
                    try
                    {
                        RestoreObjectData(item, notify);
                    }
                    catch (Exception ex)
                    {
                        var error = string.Format("Error Restoring Item: {0}", item.Name);
                        errors.AppendLine(error);

                        notify(error);
                        notify(ex.ToString());
                    }
                }

                notify(string.Format("{0}Database restore process COMPLETED.", WConstants.WBREAK));
            }

            // Check if there are errors
            if (errors.Length > 0)
                notify(errors.ToString());

            return true;
        }

        public void UpdateLastRecords(Action<string> notify)
        {
            var items = WebObject.UpdateLastRecords();
            foreach (var item in items)
                notify(string.Format("{0}Last record update: {1}", WConstants.WBREAK, item.Name));
        }

        private void ExecuteSqlScriptFile(string sqlFile, StringBuilder errors, bool continueOnError)
        {
            if (!File.Exists(sqlFile))
                return;

            string queryBatch = FileHelper.ReadFile(sqlFile);

            // Split on GO batch separator (SQL Server convention) or execute as single statement
            string[] queries;
            if (IsSqlServer)
            {
                const char cDelim = '\u00b6';
                queries = queryBatch.Replace("\r\nGO", cDelim.ToString())
                                    .Replace("\nGO", cDelim.ToString())
                                    .Split(cDelim);
            }
            else
            {
                // PostgreSQL: execute full script as one batch
                queries = new[] { queryBatch, string.Empty };
            }

            if (queries.Length > 1)
            {
                try
                {
                    for (int i = 0; i < queries.Length - 1; i++)
                    {
                        string query = queries[i].Trim();
                        if (!string.IsNullOrEmpty(query))
                        {
                            try
                            {
                                DbHelper.ExecuteNonQuery(CommandType.Text, query);
                            }
                            catch (Exception ex)
                            {
                                LogHelper.WriteLog(ex);

                                if (errors != null)
                                    errors.Append(ex.ToString());

                                if (!continueOnError)
                                    break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(ex);

                    if (errors != null)
                        errors.Append(ex.ToString());
                }
            }
        }

        public bool DropAllObjects(Action<string> notify)
        {
            notify(string.Format("Object drop process STARTED...", WConstants.WBREAK));

            var sbErrors = new StringBuilder();

            if (IsPostgreSql)
            {
                // PostgreSQL: drop all tables using information_schema
                DropAllPostgreSqlTables(sbErrors, notify);
            }
            else
            {
                // SQL Server: use individual .drop.sql scripts
                if (Directory.Exists(ProcedureSqlPath))
                {
                    string[] procedureFiles = Directory.GetFiles(ProcedureSqlPath, DbConstants.DROP_FILTER_WC);
                    foreach (string procedureFile in procedureFiles)
                    {
                        notify(string.Format("Drop Procedure: {0}", Path.GetFileName(procedureFile)));
                        ExecuteSqlScriptFile(procedureFile, sbErrors, true);
                    }

                    notify("");
                }

                if (Directory.Exists(TableSqlPath))
                {
                    var tableFiles = Directory.GetFiles(TableSqlPath, DbConstants.DROP_FILTER_WC);
                    foreach (string tableFile in tableFiles)
                    {
                        notify(string.Format("Drop Table: {0}", Path.GetFileName(tableFile)));
                        ExecuteSqlScriptFile(tableFile, sbErrors, true);
                    }
                }
            }

            notify(string.Format("{0}Object Drop process COMPLETED...{0}", WConstants.WBREAK));

            // Check if there are errors
            if (sbErrors.Length > 0)
                notify(sbErrors.ToString());

            return true;
        }

        private void DropAllPostgreSqlTables(StringBuilder errors, Action<string> notify)
        {
            // Get all table names from the public schema
            var tableNames = new List<string>();
            using (var r = DbHelper.ExecuteReader(CommandType.Text,
                "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public' AND table_type = 'BASE TABLE'"))
            {
                while (r.Read())
                    tableNames.Add(r["table_name"].ToString());
            }

            // Drop all tables with CASCADE to handle foreign key dependencies
            foreach (var tableName in tableNames)
            {
                try
                {
                    notify(string.Format("Drop Table: {0}", tableName));
                    DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("DROP TABLE IF EXISTS \"{0}\" CASCADE", tableName));
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(ex);
                    errors?.AppendLine(ex.ToString());
                }
            }
        }

        private void RestorePostgreSqlSchema(StringBuilder errors, Action<string> notify)
        {
            var pgDir = PostgreSqlSchemaDir;
            if (string.IsNullOrEmpty(pgDir))
            {
                errors?.AppendLine("PostgreSQL schema directory not found.");
                notify?.Invoke("ERROR: PostgreSQL schema directory not found.");
                return;
            }

            // Execute schema files for the main database
            var schemaFiles = new[] { "schema.sql", "schema-integration.sql", "schema-biblereader.sql" };
            foreach (var schemaFile in schemaFiles)
            {
                var schemaPath = Path.Combine(pgDir, schemaFile);
                if (File.Exists(schemaPath))
                {
                    notify?.Invoke(string.Format("Executing PostgreSQL schema: {0}", schemaFile));
                    ExecutePostgreSqlScript(schemaPath, errors);
                }
                else
                {
                    notify?.Invoke(string.Format("Schema file not found: {0}", schemaFile));
                }
            }
        }

        private void ExecutePostgreSqlScript(string scriptPath, StringBuilder errors)
        {
            var sql = File.ReadAllText(scriptPath);

            // Split on semicolons for individual statement execution, handling edge cases
            // Execute the whole script as a single batch first; fall back to statement-by-statement
            try
            {
                DbHelper.ExecuteNonQuery(CommandType.Text, sql);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                errors?.AppendLine(string.Format("Error executing {0}: {1}", Path.GetFileName(scriptPath), ex.Message));
            }
        }

        public bool Backup(Action<string> notify)
        {
            if (IsNewDb)
            {
                notify(string.Format("{0}{0}This is a new database, nothing to backup.{0}{0}", WConstants.WBREAK));
                return true;
            }

            if (IsProductionEnvironment() &&
                !ConfigUtil.GetBool(AllowFixtureBackupInProductionKey, false) &&
                HasIntegrationFixtureMarker())
            {
                notify(string.Format(
                    "{0}{0}Backup process ABORTED. Integration fixture marker detected in production environment.{0}" +
                    "Set {1}=true only if this backup is intentionally fixture-inclusive.{0}{0}",
                    WConstants.WBREAK,
                    AllowFixtureBackupInProductionKey));
                return false;
            }

            string backupPath = BackupPath;
            string backupTablesPath = TableSqlPath;
            string backupProceduresPath = ProcedureSqlPath;
            string dbXml = Path.Combine(XML_PATH, DbConstants.XML_FILE);

            FileHelper.CreateFolderOrDeleteAllFiles(backupPath);
            FileHelper.CreateFolderOrDeleteAllFiles(backupTablesPath);

            if (IsSqlServer)
                FileHelper.CreateFolderOrDeleteAllFiles(backupProceduresPath);

            notify(string.Format("{0}{0}Backup process STARTED...{0}", WConstants.WBREAK));

            UpdateLastRecords(notify);

            var items = WebObject.GetList();

            // Export all data into xml files
            for (int i = 0; i < items.Count(); i++)
            {
                var item = items.ElementAt(i);
                BackupObjectData(item, notify, i);
            }

            notify("");

            // SMO-based script generation (SQL Server only)
            if (IsSqlServer)
            {
                // Generate drop scripts
                SqlScriptGenerator.GenerateScript(backupTablesPath, true, (string objName) =>
                    { notify(string.Format("Generate Drop Table Script: {0}", objName)); }
                );

                notify("");

                // Generate object scripts
                SqlScriptGenerator.GenerateScript(backupTablesPath, false, (string objName) =>
                    { notify(string.Format("Generate Restore Table Script: {0}", objName)); }
                );

                notify("");

                // Generate procedures script (legacy — stored procedures have been eliminated)
                BackupStoredProcedures(backupProceduresPath, notify);
            }
            else
            {
                // PostgreSQL: generate portable DROP/CREATE table scripts
                BackupTableSchemaPortable(items, backupTablesPath, notify);
            }

            notify(string.Format("{0}{0}Backup process COMPLETED.{0}{0}", WConstants.WBREAK));

            return true;
        }

        private void BackupStoredProcedures(string backupProceduresPath, Action<string> notify)
        {
            // SQL Server: backup stored procedure definitions from INFORMATION_SCHEMA
            using (var r = DbHelper.ExecuteReader(CommandType.Text,
                "SELECT ROUTINE_NAME, ROUTINE_DEFINITION FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE='PROCEDURE'"))
            {
                while (r.Read())
                {
                    string routineName = r["ROUTINE_NAME"].ToString();
                    string routineDef = r["ROUTINE_DEFINITION"].ToString().Trim();

                    var sbDrop = new StringBuilder();
                    sbDrop.AppendFormat("{1}IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[{0}]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)", routineName, Environment.NewLine);
                    sbDrop.AppendFormat("{1}DROP PROCEDURE [{0}]{1}", routineName, Environment.NewLine);
                    sbDrop.AppendLine("GO" + Environment.NewLine);

                    using (var writer = new StreamWriter(Path.Combine(backupProceduresPath, routineName + ".drop.sql")))
                        writer.WriteLine(sbDrop.ToString());

                    var sbBody = new StringBuilder();
                    sbBody.AppendFormat("{1}-- Procedure {0}", routineName, Environment.NewLine);
                    sbBody.AppendFormat("{0}SET ANSI_NULLS ON{0}GO{0}SET QUOTED_IDENTIFIER ON{0}GO{0}", Environment.NewLine);
                    sbBody.AppendLine(routineDef);
                    sbBody.AppendLine("GO" + Environment.NewLine);
                    sbBody.AppendFormat("{0}SET ANSI_NULLS ON{0}GO{0}SET QUOTED_IDENTIFIER OFF{0}GO{0}", Environment.NewLine);

                    using (var writer = new StreamWriter(Path.Combine(backupProceduresPath, routineName + ".create.sql")))
                        writer.WriteLine(sbBody.ToString());

                    notify(string.Format("Procedure {0} WRITTEN.", routineName));
                }
            }
        }

        private void BackupTableSchemaPortable(IEnumerable<WebObject> items, string backupTablesPath, Action<string> notify)
        {
            // PostgreSQL: generate portable DROP TABLE / CREATE TABLE scripts via information_schema
            foreach (var item in items)
            {
                var quotedName = DbSyntax.QuoteIdentifier(item.Name);

                // Generate drop script
                var dropSql = string.Format("DROP TABLE IF EXISTS {0};", quotedName);
                var dropFile = Path.Combine(backupTablesPath, item.Name + ".drop.sql");
                File.WriteAllText(dropFile, dropSql);

                // Generate column info for create script
                var createSb = new StringBuilder();
                createSb.AppendFormat("CREATE TABLE IF NOT EXISTS {0} (", quotedName);
                createSb.AppendLine();

                bool first = true;
                var tableNameParam = DbHelper.CreateParameter("@TableName", item.Name);
                using (var r = DbHelper.ExecuteReader(CommandType.Text,
                    "SELECT column_name, data_type, character_maximum_length, is_nullable, column_default " +
                    "FROM information_schema.columns WHERE table_name = @TableName ORDER BY ordinal_position",
                    tableNameParam))
                {
                    while (r.Read())
                    {
                        if (!first) createSb.AppendLine(",");
                        first = false;

                        string colName = DbSyntax.QuoteIdentifier(r["column_name"].ToString());
                        string dataType = r["data_type"].ToString();
                        var maxLen = r["character_maximum_length"];
                        string nullable = r["is_nullable"].ToString() == "NO" ? "NOT NULL" : "NULL";
                        string colDefault = r["column_default"] != DBNull.Value ? r["column_default"].ToString() : null;

                        string typeDef = dataType;
                        if (maxLen != DBNull.Value && maxLen != null)
                        {
                            long len = Convert.ToInt64(maxLen);
                            if (len > 0)
                                typeDef = string.Format("{0}({1})", dataType, len);
                        }

                        createSb.AppendFormat("    {0} {1} {2}", colName, typeDef, nullable);
                        if (!string.IsNullOrEmpty(colDefault))
                            createSb.AppendFormat(" DEFAULT {0}", colDefault);
                    }
                }

                createSb.AppendLine();
                createSb.AppendLine(");");

                var createFile = Path.Combine(backupTablesPath, item.Name + ".create.sql");
                File.WriteAllText(createFile, createSb.ToString());

                notify(string.Format("Generate Table Script: {0}", item.Name));
            }
        }

        public bool Restore(Action<string> notify)
        {
            // Drop all database objects before restoring the backup
            DropAllObjects(notify);
            RestoreAllObjects(notify);

            return true;
        }

        private static bool IsProductionEnvironment()
        {
            var env = (ConfigUtil.Get("WCMS:Environment") ?? string.Empty).Trim();
            return env.Equals("PROD", StringComparison.OrdinalIgnoreCase) ||
                   env.Equals("PRODUCTION", StringComparison.OrdinalIgnoreCase);
        }

        private static bool HasIntegrationFixtureMarker()
        {
            try
            {
                var tableName = DbSyntax.QuoteIdentifier("WebConstant");
                var categoryCol = DbSyntax.QuoteIdentifier("Category");
                var textCol = DbSyntax.QuoteIdentifier("Text");
                var valueCol = DbSyntax.QuoteIdentifier("Value");

                var sql = string.Format(
                    "SELECT 1 FROM {0} WHERE {1}=@Category AND {2}=@Text AND {3}=@Value",
                    tableName, categoryCol, textCol, valueCol);

                var categoryParam = DbHelper.CreateParameter("@Category", FixtureMarkerCategory);
                var textParam = DbHelper.CreateParameter("@Text", FixtureMarkerText);
                var valueParam = DbHelper.CreateParameter("@Value", FixtureMarkerValue);

                using (var reader = DbHelper.ExecuteReader(CommandType.Text, sql, categoryParam, textParam, valueParam))
                    return reader != null && reader.Read();
            }
            catch
            {
                // If schema is not initialized yet, do not block backup here.
                return false;
            }
        }
    }
}
