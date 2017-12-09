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

        public string BackupPath { get { return string.Format(@"{0}\Database", XML_PATH); } }
        public string ProcedureSqlPath { get { return string.Format(@"{0}\Database\Procedures\", XML_PATH); } }
        public string TableSqlPath { get { return string.Format(@"{0}\Database\Tables\", XML_PATH); } }

        public DbManager()
        {
            if (SqlScriptGenerator.CheckCreateDatabase())
                IsNewDb = true;
        }

        public string RestoreObjectSchema(WebObject item)
        {
            var errors = new StringBuilder();
            // Restore table schema
            string[] tableFiles = Directory.GetFiles(TableSqlPath, string.Format("{0}{1}", item.Name, DbConstants.CREATE_FILTER_WC));
            foreach (string tableFile in tableFiles)
                ExecuteSqlScriptFile(tableFile, errors, true);

            // Restore procedure schema
            string[] procedureFiles = Directory.GetFiles(ProcedureSqlPath, string.Format("{0}_*{1}", item.Name, DbConstants.CREATE_FILTER_WC));
            foreach (string procedureFile in procedureFiles)
                ExecuteSqlScriptFile(procedureFile, errors, true);

            return errors.ToString();
        }

        public string DropObjectSchema(WebObject item, Action<string> notify)
        {
            var errors = new StringBuilder();
            string[] procedureFiles = Directory.GetFiles(ProcedureSqlPath, string.Format("{0}_{1}", item.Name, DbConstants.DROP_FILTER_WC));
            foreach (string procedureFile in procedureFiles)
            {
                notify(string.Format("Drop Procedure: {0}", Path.GetFileName(procedureFile)));
                ExecuteSqlScriptFile(procedureFile, errors, true);
            }

            //this.WriteStatus("");

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
            //string selected = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(selected))
            {
                // Map to exact path
                string tableSqlPath = TableSqlPath;

                try
                {
                    var items = from id in DataHelper.ParseCommaSeparatedIdList(selected)
                                select WebObject.Get(id);

                    foreach (var item in items)
                    {
                        string dropScript = Path.Combine(tableSqlPath, string.Format(@"{1}.drop.sql", item.Name));
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
            // Clear table
            SqlHelper.ExecuteNonQuery(CommandType.Text, string.Format("TRUNCATE TABLE {0}", item.Name));
        }

        public void RestoreObjectData(WebObject item, Action<string> notify)
        {
            string tableXml = string.Format(@"{0}\{1}.xml", BackupPath, item.Name);
            if (!File.Exists(tableXml))
            {
                notify(string.Format("{0}.xml does not exist.", item.Name));
                return;
            }

            TruncateObject(item);

            var ds = new DataSet();
            ds.ReadXml(tableXml, XmlReadMode.ReadSchema);

            SqlHelper.ExecuteUpdateDataSet(string.Format("SELECT * FROM {0}", item.Name), ds.Tables[0]);

            notify(string.Format("{0}.xml RESTORED.", item.Name));
        }

        public void BackupObjectData(WebObject item, Action<string> notify, int index = -1)
        {
            // TruncateObject(item); // Should be update last record

            string targetXmlFile = string.Format(@"{0}\{1}.xml", BackupPath, item.Name);
            // Backup the data
            var ds = SqlHelper.ExecuteDataSetSchema(CommandType.Text,
                string.Format("SELECT * FROM {0}", item.Name));

            ds.DataSetName = "mPortal";
            ds.Tables[0].TableName = item.Name;
            ds.WriteXml(targetXmlFile, XmlWriteMode.WriteSchema);

            if (item.Name == "WebObject")
            {
                string dbXml = string.Format(@"{0}\{1}", XML_PATH, DbConstants.XML_FILE);
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

            // Restore table schema
            string[] tableFiles = Directory.GetFiles(TableSqlPath, DbConstants.CREATE_FILTER_WC);
            foreach (string tableFile in tableFiles)
                ExecuteSqlScriptFile(tableFile, errors, true);

            // Restore procedure schema
            string[] procedureFiles = Directory.GetFiles(ProcedureSqlPath, DbConstants.CREATE_FILTER_WC);
            foreach (string procedureFile in procedureFiles)
                ExecuteSqlScriptFile(procedureFile, errors, true);

            // Start restoring data
            // Insert data
            if (Directory.Exists(BackupPath))
            {
                notify(string.Format("Database restore process STARTED.{0}", WConstants.WBREAK));

                // Execute stored procedures
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

                //RestoreSelectedObjectData(items);

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
            const char cDelim = '\u00b6';

            // Drop all procedures
            if (File.Exists(sqlFile))
            {
                string queryBatch = FileHelper.ReadFile(sqlFile);
                string[] queries = queryBatch.Replace("\r\nGO", cDelim.ToString()).Split(cDelim);

                if (queries.Length > 1)
                {
                    // multiple queries
                    try
                    {
                        // execute each query
                        for (int i = 0; i < queries.Length - 1; i++)
                        {
                            string query = queries[i].Trim();
                            if (!string.IsNullOrEmpty(query))
                            {
                                try
                                {
                                    SqlHelper.ExecuteNonQuery(CommandType.Text, query);
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
        }

        public bool DropAllObjects(Action<string> notify)
        {
            // Map to exact path

            string procedureSqlPath = ProcedureSqlPath;
            string tableSqlPath = TableSqlPath;

            notify(string.Format("Object drop process STARTED...", WConstants.WBREAK));

            var sbErrors = new StringBuilder();

            // Get all procedure drop scripts and execute them
            string[] procedureFiles = Directory.GetFiles(procedureSqlPath, DbConstants.DROP_FILTER_WC);
            foreach (string procedureFile in procedureFiles)
            {
                notify(string.Format("Drop Procedure: {0}", Path.GetFileName(procedureFile)));
                ExecuteSqlScriptFile(procedureFile, sbErrors, true);
            }

            notify("");

            // Get all table drop scripts and execute them
            var tableFiles = Directory.GetFiles(tableSqlPath, DbConstants.DROP_FILTER_WC);
            foreach (string tableFile in tableFiles)
            {
                notify(string.Format("Drop Table: {0}", Path.GetFileName(tableFile)));
                ExecuteSqlScriptFile(tableFile, sbErrors, true);
            }

            notify(string.Format("{0}Object Drop process COMPLETED...{0}", WConstants.WBREAK));

            // Check if there are errors
            if (sbErrors.Length > 0)
                notify(sbErrors.ToString());

            return true;
        }

        public bool Backup(Action<string> notify)
        {
            // Map to exact path

            if (IsNewDb)
            {
                notify(string.Format("{0}{0}This is a new database, nothing to backup.{0}{0}", WConstants.WBREAK));
                return true;
            }

            string backupPath = BackupPath;
            string backupTablesPath = TableSqlPath;
            string backupProceduresPath = ProcedureSqlPath;
            string dbXml = string.Format(@"{0}\{1}", XML_PATH, DbConstants.XML_FILE);

            FileHelper.CreateFolderOrDeleteAllFiles(backupPath);
            FileHelper.CreateFolderOrDeleteAllFiles(backupTablesPath);
            FileHelper.CreateFolderOrDeleteAllFiles(backupProceduresPath);

            notify(string.Format("{0}{0}Backup process STARTED...{0}", WConstants.WBREAK));

            UpdateLastRecords(notify);

            var items = WebObject.GetList();

            var sbRoutinesDrop = new StringBuilder();
            var sbRoutinesBody = new StringBuilder();

            // Export all data into xml files
            for (int i = 0; i < items.Count(); i++)
            {
                var item = items.ElementAt(i);
                BackupObjectData(item, notify, i);

                /*
                string targetXmlFile = string.Format(@"{0}\{1}.xml", backupPath, item.Name);
                // Backup the data
                var ds = SqlHelper.ExecuteDataSetSchema(CommandType.Text,
                    string.Format("SELECT * FROM {0}", item.Name));

                ds.DataSetName = "mPortal";
                ds.Tables[0].TableName = item.Name;
                ds.WriteXml(targetXmlFile, XmlWriteMode.WriteSchema);

                if (item.Name == "WebObject")
                    File.Copy(targetXmlFile, dbXml, true);

                notify(string.Format("{0} {1} COMPLETED.", i, item.Name));
                */
            }

            notify("");

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

            // Generate procedures script
            using (var r = SqlHelper.ExecuteReader(CommandType.Text,
                string.Format("SELECT ROUTINE_NAME, ROUTINE_DEFINITION from INFORMATION_SCHEMA.ROUTINES" +
                              " WHERE ROUTINE_TYPE='PROCEDURE'")))
            {
                while (r.Read())
                {
                    string routineName = r["ROUTINE_NAME"].ToString();
                    string routineDef = r["ROUTINE_DEFINITION"].ToString().Trim();

                    sbRoutinesDrop = new StringBuilder();
                    sbRoutinesDrop.AppendFormat("{1}if exists (select * from dbo.sysobjects where id = object_id(N'[{0}]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)", routineName, Environment.NewLine);
                    sbRoutinesDrop.AppendFormat("{1}drop procedure [{0}]{1}", routineName, Environment.NewLine);
                    sbRoutinesDrop.AppendLine("GO" + Environment.NewLine);

                    using (StreamWriter writer = new StreamWriter(Path.Combine(backupProceduresPath, routineName + ".drop.sql")))
                    {
                        writer.WriteLine(sbRoutinesDrop.ToString());
                    }

                    // Write the sql codes to file
                    sbRoutinesBody = new StringBuilder();
                    sbRoutinesBody.AppendFormat("{1}-- Procedure {0}", routineName, Environment.NewLine);
                    sbRoutinesBody.AppendFormat("{0}SET ANSI_NULLS ON{0}GO{0}SET QUOTED_IDENTIFIER ON{0}GO{0}", Environment.NewLine);
                    sbRoutinesBody.AppendLine(routineDef);
                    sbRoutinesBody.AppendLine("GO" + Environment.NewLine);
                    sbRoutinesBody.AppendFormat("{0}SET ANSI_NULLS ON{0}GO{0}SET QUOTED_IDENTIFIER OFF{0}GO{0}", Environment.NewLine);

                    using (StreamWriter writer = new StreamWriter(Path.Combine(backupProceduresPath, routineName + ".create.sql")))
                    {
                        writer.WriteLine(sbRoutinesBody.ToString());
                    }

                    notify(string.Format("Procedure {0} WRITTEN.", routineName));
                }
            }

            // Write procedures to file

            //using (StreamWriter writer = new StreamWriter(Path.Combine(dbPath, "Procedures.drop.sql")))
            //{
            //    writer.WriteLine(sbRoutinesDrop.ToString());
            //    this.WriteStatus("Procedures written.");
            //}

            //using (StreamWriter writer = new StreamWriter(Path.Combine(dbPath, "Procedures.sql")))
            //{
            //    writer.WriteLine(sbRoutinesBody.ToString());
            //    this.WriteStatus("Procedures written.");
            //}

            notify(string.Format("{0}{0}Backup process COMPLETED.{0}{0}", WConstants.WBREAK));

            return true;
        }

        public bool Restore(Action<string> notify)
        {
            // Drop all database objects before restoring the backup
            DropAllObjects(notify);
            RestoreAllObjects(notify);

            return true;
        }
    }
}
