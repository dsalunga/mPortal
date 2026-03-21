using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using System.IO;

using System.Text;
using System.Xml;
using System.Xml.Linq;

using WCMS.Common.Utilities;

namespace WCMS.BibleReader
{
    public partial class Setup : Page
    {
        private const string OBJECTS_XPATH = "//WebObject";
        private const string XML_FILE = "WebObject.xml";
        private const string OBJECT_NAME_NODE = "Name";
        private const string OBJECT_IDENTITY_NODE = "IdentityColumn";
        private const string SELECT_MAX = "SELECT MAX({0}) FROM {1}";

        private const string TAB_GENERAL = "tabGeneral";
        private const string TAB_BACKUP = "tabBackup";
        private const string TAB_REGISTER = "tabRegister";

        private readonly string XML_PATH = ConfigUtil.Get("XmlProvider.Path");


        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void cmdBackup_Click(object sender, EventArgs e)
        {
            /*
            string dbPath = XML_PATH;

            // Map to exact path
            if (dbPath.StartsWith("~"))
            {
                dbPath = MapPath(dbPath);
            }

            string backupPath = string.Format(@"{0}\Database", dbPath);
            string backupTablesPath = string.Format(@"{0}\Database\Tables", dbPath);
            string backupProceduresPath = string.Format(@"{0}\Database\Procedures", dbPath);
            string dbXml = string.Format(@"{0}\{1}", dbPath, XML_FILE);

            CreateFolderOrDeleteAllFiles(backupPath);
            CreateFolderOrDeleteAllFiles(backupTablesPath);
            CreateFolderOrDeleteAllFiles(backupProceduresPath);

            this.WriteStatus("{0}{0}Backup process STARTED...{0}", "<br />");

            StringBuilder sbRoutinesDrop = new StringBuilder();
            StringBuilder sbRoutinesBody = new StringBuilder();

            var items = SqlScriptGenerator.GetTableList();

            // Export all data into xml files
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];

                string targetXmlFile = string.Format(@"{0}\{1}.xml", backupPath, item);
                // Backup the data
                DataSet ds = SqlHelper.ExecuteDataSetSchema(CommandType.Text,
                    string.Format("SELECT * FROM {0}", item));

                ds.DataSetName = "WCMS";
                ds.Tables[0].TableName = item;
                ds.WriteXml(targetXmlFile, XmlWriteMode.WriteSchema);

                if (item == "WebObject")
                    File.Copy(targetXmlFile, dbXml, true);

                this.WriteStatus("{0} {1} COMPLETED.", i, item);
            }

            this.WriteStatus("");

            // Generate drop scripts
            SqlScriptGenerator.GenerateScript(backupTablesPath, true, (string objName) =>
            { this.WriteStatus("Generate Drop Table Script: {0}", objName); }
            );

            this.WriteStatus("");

            // Generate object scripts
            SqlScriptGenerator.GenerateScript(backupTablesPath, false, (string objName) =>
            { this.WriteStatus("Generate Restore Table Script: {0}", objName); }
            );

            this.WriteStatus("");

            // Generate procedures script
            using (DbDataReader r = SqlHelper.ExecuteReader(CommandType.Text,
                string.Format("SELECT ROUTINE_NAME, ROUTINE_DEFINITION from INFORMATION_SCHEMA.ROUTINES" +
                              " WHERE ROUTINE_TYPE='PROCEDURE'")))
            {
                if (r.HasRows)
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

                        this.WriteStatus("Procedure {0} WRITTEN.", routineName);
                    }
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
             * 
             */

            this.WriteStatus("{0}{0}Backup process COMPLETED.{0}{0}", "<br />");
        }

        protected void cmdRestore_Click(object sender, EventArgs e)
        {
            // Drop all database objects before restoring the backup
            DropObjects();

            RestoreObjects();
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            HttpRuntime.UnloadAppDomain();
            QueryParser.StaticRedirect();
        }

        protected void cmdDrop_Click(object sender, EventArgs e)
        {
            DropObjects();
        }

        private static void CreateFolderOrDeleteAllFiles(string backupPath)
        {
            if (!Directory.Exists(backupPath))
            {
                Directory.CreateDirectory(backupPath);
            }
            else
            {
                // Delete all files inside "Database" backup directory.
                string[] deleteFiles = Directory.GetFiles(backupPath);
                foreach (string deleteFile in deleteFiles)
                {
                    File.Delete(deleteFile);
                }
            }
        }

        private void RestoreObjects()
        {
            string dbPath = XML_PATH;

            // Map to exact path
            if (dbPath.StartsWith("~"))
                dbPath = MapPath(dbPath);

            string createFilter = "*.create.sql";
            string procedureSqlPath = Path.Combine(dbPath, @"Database\Procedures\");
            string tableSqlPath = Path.Combine(dbPath, @"Database\Tables\");
            string backupPath = string.Format(@"{0}\Database", dbPath);

            StringBuilder sbErrors = new StringBuilder();

            // Execute schema scripts

            // Restore table schema
            string[] tableFiles = Directory.GetFiles(tableSqlPath, createFilter);
            foreach (string tableFile in tableFiles)
            {
                ExecuteSqlScriptFile(tableFile, sbErrors, true);
            }

            // Restore procedure schema
            string[] procedureFiles = Directory.GetFiles(procedureSqlPath, createFilter);
            foreach (string procedureFile in procedureFiles)
            {
                ExecuteSqlScriptFile(procedureFile, sbErrors, true);
            }

            // Start restoring data
            // Insert data
            if (Directory.Exists(backupPath))
            {
                this.WriteStatus("Database restore process STARTED.<br/>");

                // Execute stored procedures
                try
                {
                    RestoreObjectData(backupPath);

                    this.WriteStatus("<br/>Database restore process COMPLETED.");
                }
                catch (Exception ex)
                {
                    this.WriteStatus(ex.ToString());
                }
            }

            // Check if there are errors
            string errorMsgs = sbErrors.ToString();
            if (errorMsgs.Length > 0)
            {
                divMsgs.InnerHtml = sbErrors.ToString();
            }
        }

        private void RestoreObjectData(string backupPath)
        {
            /*
            var items = SqlScriptGenerator.GetTableList();

            foreach (var item in items)
            {
                string tableXml = string.Format(@"{0}\{1}.xml", backupPath, item);
                if (!File.Exists(tableXml))
                {
                    this.WriteStatus(string.Format("{0}.xml does not exist.", item));
                    continue;
                }

                // Clear table
                SqlHelper.ExecuteNonQuery(CommandType.Text, string.Format("TRUNCATE TABLE {0}", item));

                DataSet ds = new DataSet();
                ds.ReadXml(tableXml, XmlReadMode.ReadSchema);

                SqlHelper.ExecuteUpdateDataSet(string.Format("SELECT * FROM {0}", item), ds.Tables[0]);

                this.WriteStatus(string.Format("{0}.xml RESTORED.", item));
            }
             */
        }

        private void WriteStatus(string msgStatus, params object[] parms)
        {
            string msg = string.Format(msgStatus, parms);

            divMsgs.InnerHtml += string.Format("{0}<br />", msg);
        }

        
        private void DropObjects()
        {
            string dbPath = XML_PATH;

            // Map to exact path
            if (dbPath.StartsWith("~"))
                dbPath = MapPath(dbPath);

            string dropFilter = "*.drop.sql";
            string procedureSqlPath = Path.Combine(dbPath, @"Database\Procedures\");
            string tableSqlPath = Path.Combine(dbPath, @"Database\Tables\");


            this.WriteStatus("Object drop process STARTED...<br/>");

            StringBuilder sbErrors = new StringBuilder();

            // Get all procedure drop scripts and execute them
            string[] procedureFiles = Directory.GetFiles(procedureSqlPath, dropFilter);
            foreach (string procedureFile in procedureFiles)
            {
                this.WriteStatus("Drop Procedure: {0}", Path.GetFileName(procedureFile));
                ExecuteSqlScriptFile(procedureFile, sbErrors, true);
            }

            this.WriteStatus("");

            // Get all table drop scripts and execute them
            string[] tableFiles = Directory.GetFiles(tableSqlPath, dropFilter);
            foreach (string tableFile in tableFiles)
            {
                this.WriteStatus("Drop Table: {0}", Path.GetFileName(tableFile));
                ExecuteSqlScriptFile(tableFile, sbErrors, true);
            }

            this.WriteStatus("<br/>Object Drop process COMPLETED...<br/>");

            // Check if there are errors
            string errorMsgs = sbErrors.ToString();
            if (errorMsgs.Length > 0)
            {
                divMsgs.InnerHtml = sbErrors.ToString();
            }
        }

        private void DropTable()
        {
            /*
            string selected = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(selected))
            {
                string dbPath = XML_PATH;

                // Map to exact path
                if (dbPath.StartsWith("~"))
                    dbPath = MapPath(dbPath);

                //string dropFilter = "*.drop.sql";
                string tableSqlPath = Path.Combine(dbPath, @"Database\Tables\");

                try
                {
                    var items = SqlScriptGenerator.GetTableList();

                    foreach (var item in items)
                    {
                        string dropScript = Path.Combine(tableSqlPath, string.Format(@"{1}.drop.sql", item));
                        if (File.Exists(dropScript))
                        {
                            this.WriteStatus("Drop Table: {0}", Path.GetFileName(item));
                            ExecuteSqlScriptFile(dropScript, null, true);
                        }
                    }

                    this.WriteStatus("<br/>Data restore process COMPLETED.");
                }
                catch (Exception ex)
                {
                    this.WriteStatus(ex.Message);
                }

            }
            */
        }

        private static void ExecuteSqlScriptFile(string sqlFile, StringBuilder sbErrors, bool continueOnError)
        {
            char cDelim = '\u00b6';

            // Drop all procedures
            if (File.Exists(sqlFile))
            {
                string sQueryBatch = FileHelper.ReadFile(sqlFile);
                string[] sQueries = sQueryBatch.Replace("\r\nGO", cDelim.ToString()).Split(cDelim);

                if (sQueries.Length > 1)
                {
                    // multiple queries
                    try
                    {
                        // execute each query
                        for (int i = 0; i < sQueries.Length - 1; i++)
                        {
                            string sQuery = sQueries[i].Trim();
                            if (!string.IsNullOrEmpty(sQuery))
                            {
                                try
                                {
                                    SqlHelper.ExecuteNonQuery(CommandType.Text, sQuery);
                                }
                                catch (Exception ex)
                                {
                                    LogHelper.WriteLog(ex);

                                    if (sbErrors != null)
                                        sbErrors.Append(ex.ToString());

                                    if (!continueOnError)
                                        break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(ex);

                        if (sbErrors != null)
                            sbErrors.Append(ex.ToString());
                    }
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }


        protected void cmdRestoreData_Click(object sender, EventArgs e)
        {
            // Start restoring data
            string selected = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(selected))
            {
                string dbPath = XML_PATH;

                // Map to exact path
                if (dbPath.StartsWith("~"))
                    dbPath = MapPath(dbPath);

                string backupPath = string.Format(@"{0}\Database", dbPath);
                if (Directory.Exists(backupPath))
                {
                    this.WriteStatus("Data restore process STARTED.<br/>");

                    try
                    {
                        RestoreObjectData(backupPath);

                        this.WriteStatus("<br/>Data restore process COMPLETED.");
                    }
                    catch (Exception ex)
                    {
                        this.WriteStatus(ex.Message);
                    }
                }
            }
        }
    }
}