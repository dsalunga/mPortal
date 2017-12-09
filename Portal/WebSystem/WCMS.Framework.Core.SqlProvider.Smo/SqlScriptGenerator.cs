using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Sdk.Sfc;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.Framework.Core.SqlProvider.Smo
{
    public class SqlScriptGenerator
    {
        public static void SyncWebObjects(IEnumerable<WebObject> items, Action<string> notify)
        {
            //Connect to the local, default instance of SQL Server. 
            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            var server = new Server(new ServerConnection(new SqlConnection(connString)));

            //Reference the AdventureWorks database. 
            var db = server.Databases[ConfigHelper.Get("WebObject.SqlDbName")];

            foreach (Table tb in db.Tables)
            {
                var smoObjects = new Urn[1];
                smoObjects[0] = tb.Urn;

                if (!tb.IsSystemObject && items.FirstOrDefault(i => i.Name == tb.Name) == null)
                {
                    var item = new WebObject();
                    if (tb.Indexes.Count > 0)
                        item.IdentityColumn = tb.Indexes[0].IndexedColumns[0].Name;
                    else
                        item.IdentityColumn = tb.Columns[0].Name;

                    item.Name = tb.Name;
                    item.Owner = tb.Name.StartsWith("Web") ? "System" : "Custom";
                    item.Update();

                    notify(tb.Name);
                }
            }
        }

        public static bool CheckCreateDatabase()
        {
            //Connect to the local, default instance of SQL Server. 
            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string dbName = ConfigHelper.Get("WebObject.SqlDbName");

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connString);
            builder.InitialCatalog = "master";

            var server = new Server(new ServerConnection(new SqlConnection(builder.ConnectionString)));

            var db = server.Databases[dbName];
            if (db == null)
            {
                db = new Database(server, dbName);
                db.Create();

                return true;
                //server.Databases.Add(db);
            }

            return false;
        }

        public static void GenerateScript(string targetRootPath, bool forDrop, Action<string> notify)
        {
            //Connect to the local, default instance of SQL Server. 
            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            var server = new Server(new ServerConnection(new SqlConnection(connString)));

            var dbName = ConfigHelper.Get("WebObject.SqlDbName");

            //Reference the database. 
            var db = server.Databases[dbName];

            //Define a Scripter object and set the required scripting options. 
            var scripter = new Scripter(server);
            scripter.Options.ScriptDrops = false;
            scripter.Options.WithDependencies = false;
            scripter.Options.IncludeHeaders = false;
            scripter.Options.DriPrimaryKey = true;
            scripter.Options.DriDefaults = true;
            scripter.Options.IncludeIfNotExists = true;
            scripter.Options.ScriptDrops = forDrop;

            //scrp.Options.ScriptData = true;
            scripter.Options.TargetServerVersion = SqlServerVersion.Version100;


            //string tablesSql = forDrop ? "Tables.drop.sql" : "Tables.sql";

            //Iterate through the tables in database and script each one. Display the script. 
            //Note that the StringCollection type needs the System.Collections.Specialized namespace to be included. 


            foreach (Table tb in db.Tables)
            {
                var smoObjects = new Urn[1];
                smoObjects[0] = tb.Urn;

                if (!tb.IsSystemObject)
                {
                    string tablesSql = tb.Name;
                    tablesSql += forDrop ? ".drop.sql" : ".create.sql";

                    using (TextWriter r = new StreamWriter(Path.Combine(targetRootPath, tablesSql), false))
                    {
                        var sc = scripter.Script(smoObjects);
                        foreach (string st in sc)
                        {
                            r.WriteLine(st);
                            r.WriteLine("GO");
                        }
                    }

                    notify(tb.Name);
                }
            }

            //using (TextWriter r = new StreamWriter(Path.Combine(targetRootPath, proceduresSql), false))
            //{
            //    foreach (StoredProcedure sp in db.StoredProcedures)
            //    {
            //        Urn[] smoObjects = new Urn[1];
            //        smoObjects[0] = sp.Urn;

            //        if (!sp.IsSystemObject)
            //        {
            //            StringCollection sc = scrp.Script(smoObjects);
            //            foreach (string st in sc)
            //            {
            //                r.WriteLine(st.Trim());
            //                r.WriteLine("GO");
            //            }
            //        }
            //    }
            //}
        }
    }
}
