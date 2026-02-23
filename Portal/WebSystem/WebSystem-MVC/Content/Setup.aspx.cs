using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text;
using System.Xml;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Core.SqlProvider.Smo;

using WCMS.WebSystem.Controls;
using WCMS.WebSystem.Utilities;

namespace WCMS.WebSystem
{
    public partial class Setup : Page
    {
        private const string TAB_GENERAL = "tabGeneral";
        private const string TAB_BACKUP = "tabBackup";
        private const string TAB_REGISTER = "tabRegister";

        private DbManager _db = null;

        public Setup()
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _db = new DbManager();

            if (!IsPostBack)
            {
                TabControl1.AddTab(TAB_GENERAL, "General");
                TabControl1.AddTab(TAB_BACKUP, "Backup & Restore");

                this.Inspect();
            }
        }

        protected void TabControl1_SelectedTabChanged(object sender, TabEventArgs e)
        {
            switch (e.TabName)
            {
                case TAB_GENERAL:
                    MultiView1.SetActiveView(viewGeneral);
                    break;

                case TAB_BACKUP:
                    MultiView1.SetActiveView(viewWebObject);
                    break;
            }
        }

        protected void cmdBackup_Click(object sender, EventArgs e)
        {
            // Map to exact path
            _db.Backup((string status) => { this.WriteStatus(status); });
        }

        protected void cmdRestore_Click(object sender, EventArgs e)
        {
            //SqlScriptGenerator.CheckCreateDatabase();
            _db.Restore((string status) => { this.WriteStatus(status); });

            if (chkAutoReset.Checked)
                SystemReset();
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            SystemReset();
            QueryParser.StaticRedirect();
        }

        /*
        private void UpdateLastRecords()
        {
            var items = WebObject.UpdateLastRecords();

            foreach (var item in items)
                this.WriteStatus("<br/>Last record update: " + item.Name);
        }
        */

        private void SystemReset()
        {
            HttpRuntime.UnloadAppDomain();
            WriteStatus("{0}{0}System Reset COMPLETED.{0}{0}", WConstants.WBREAK);
        }

        protected void cmdDrop_Click(object sender, EventArgs e)
        {
            _db.DropAllObjects((string status) => { this.WriteStatus(status); });
        }

        public DataSet Select()
        {
            try
            {
                var selected = SelectedObjects;

                return DataUtil.ToDataSet(
                    from i in WebObject.GetList()
                    select new
                    {
                        i.Id,
                        i.Name,
                        i.IdentityColumn,
                        i.LastRecordId,
                        i.DateModified,
                        Count = WebObject.GetCount(i),
                        IsSelected = selected.Contains(i.Id)
                    }
                );
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return null;
            }
        }

        private void WriteStatus(string msgStatus, params object[] parms)
        {
            string msg = parms.Count() > 0 ? string.Format(msgStatus, parms) : msgStatus;
            divMsgs.InnerHtml += string.Format("{0}{1}", msg, WConstants.WBREAK);
        }

        private void Inspect()
        {
            var sbErrors = new StringBuilder();
            string dbPath = _db.XML_PATH;

            // Test SQL Server and Database existence
            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string dbName = ConfigurationManager.AppSettings["WebObject.SqlDbName"];
            try
            {
                var builder = new SqlConnectionStringBuilder(connString);
                string server = builder.DataSource;
                string user = builder.UserID;
                string password = builder.Password;
                string initialDatabase = builder.InitialCatalog;

                sbErrors.Append("* SQL Server: " + server + WConstants.WBREAK);
                sbErrors.Append("* InitialDB: " + initialDatabase + WConstants.WBREAK);
                sbErrors.Append("* SqlDbName: " + dbName + WConstants.WBREAK + WConstants.WBREAK);

                // Test server connection
                builder.InitialCatalog = "master";
                using (var conn = new SqlConnection(builder.ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        sbErrors.Append("* SQL Server connection: OK" + WConstants.WBREAK);
                    }
                    catch (Exception ex)
                    {
                        sbErrors.Append("* SQL Server connection FAILED: " + ex.Message + WConstants.WBREAK);
                        divErrors.InnerHtml = sbErrors.ToString();
                        return;
                    }

                    // Test database existence
                    using (var cmd = new SqlCommand("SELECT db_id(@initialDatabase)", conn))
                    {
                        cmd.Parameters.AddWithValue("@initialDatabase", initialDatabase);
                        var dbId = cmd.ExecuteScalar();
                        if (dbId == DBNull.Value || dbId == null)
                        {
                            sbErrors.Append("* Database InitialDB '" + initialDatabase + "' does not exist." + WConstants.WBREAK);
                            divErrors.InnerHtml = sbErrors.ToString();
                        }
                        else
                        {
                            sbErrors.Append("* Database InitialDB '" + initialDatabase + "' OK." + WConstants.WBREAK);
                        }
                    }

                    // Test database existence
                    using (var cmd = new SqlCommand("SELECT db_id(@dbName)", conn))
                    {
                        cmd.Parameters.AddWithValue("@dbName", dbName);
                        var dbId = cmd.ExecuteScalar();
                        if (dbId == DBNull.Value || dbId == null)
                        {
                            sbErrors.Append("* Database SqlDbName '" + dbName + "' does not exist." + WConstants.WBREAK);
                            divErrors.InnerHtml = sbErrors.ToString();
                        }
                        else
                        {
                            sbErrors.Append("* Database SqlDbName '" + dbName + "' OK." + WConstants.WBREAK);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sbErrors.Append("* SQL Server/database test error: " + ex.Message + WConstants.WBREAK);
                divErrors.InnerHtml = sbErrors.ToString();
                return;
            }

            if (sbErrors.Length == 0)
            {
                sbErrors.Append("* SQL Server and Database connectivity: OK" + WConstants.WBREAK);
            }

            sbErrors.Append(WConstants.WBREAK);

            // Map to exact path
            string databasePath = string.Format(@"{0}{1}{2}", dbPath, Path.DirectorySeparatorChar, DbConstants.XML_FILE);
            if (File.Exists(databasePath))
            {
                try
                {
                    // Invoke a procedure call
                    string siteName = WConfig.DefaultSite.Name;

                    // Traverse the XML records of Tables
                    var items = WebObject.GetList();
                    foreach (var item in items)
                    {
                        try
                        {
                            SqlHelper.ExecuteScalar(CommandType.Text,
                                string.Format("SELECT TOP 1 * FROM {0}", item.Name));
                        }
                        catch (SqlException ex)
                        {
                            LogHelper.WriteLog(ex);
                            sbErrors.Append(string.Format("* Table {0} is not accessible or doesn't exist.{1}", item.Name, WConstants.WBREAK));
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(ex);
                    sbErrors.AppendFormat("* Database connectivity error:{1}{0}{1}", ex, WConstants.WBREAK);
                }
            }
            else
            {
                sbErrors.AppendFormat("* Database definition XML is missing.{0}", WConstants.WBREAK);
            }

            divErrors.InnerHtml = sbErrors.Length > 0 ? sbErrors.ToString() : "* None";
        }



        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataUtil.GetId(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Custom_Select":
                    var selected = SelectedObjects;
                    ToggleSelect(selected, id);
                    SelectedObjects = selected;

                    GridView1.DataBind();
                    break;
            }

        }

        protected void cmdRestoreDataSelected_Click(object sender, EventArgs e)
        {
            // Start restoring data
            string backupPath = _db.BackupPath;
            if (Directory.Exists(backupPath))
            {
                this.WriteStatus("Data restore process STARTED.{0}", WConstants.WBREAK);

                try
                {
                    var selected = SelectedObjects;
                    var restoreSchema = cboRestoredSelected.SelectedIndex == 1;

                    if (cboRestoredSelected.SelectedIndex == 1)
                    {
                        foreach (var id in selected)
                        {
                            var item = WebObject.Get(id);
                            if (restoreSchema)
                            {
                                _db.DropObjectSchema(item, (string status) => { this.WriteStatus(status); });
                                _db.RestoreObjectSchema(item);
                            }
                            else
                            {
                                //_db.
                            }

                            _db.RestoreObjectData(item, (string status) => { this.WriteStatus(status); });
                        }
                    }

                    //var items = from id in selected
                    //            select WebObject.Get(id);

                    //RestoreSelectedObjectData(items);

                    this.WriteStatus("{0}Data restore process COMPLETED.", WConstants.WBREAK);
                }
                catch (Exception ex)
                {
                    this.WriteStatus(ex.Message);
                }
            }
        }

        protected void cmdSecureMe_Click(object sender, EventArgs e)
        {
            var thisPath = MapPath(WSConstants.SetupPath);
            if (File.Exists(thisPath))
            {
                File.Move(thisPath, thisPath + "_");
                WebUtil.Redirect("/", Context);
            }
        }

        protected void cmdSelect_Click(object sender, EventArgs e)
        {
            string selectedIds = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(selectedIds))
            {
                var ids = DataUtil.ParseCommaSeparatedIdList(selectedIds);
                var selected = SelectedObjects;
                foreach (var id in ids)
                    ToggleSelect(selected, id);

                SelectedObjects = selected;

                GridView1.DataBind();
            }
        }

        private static void ToggleSelect(List<int> selected, int id)
        {
            if (selected.Contains(id))
                selected.Remove(id);
            else
                selected.Add(id);
        }


        private const string SELECTED_KEY = "Setup_Selected";

        private List<int> SelectedObjects
        {
            get
            {
                var selected = Session[SELECTED_KEY] as List<int>;
                if (selected == null)
                    return new List<int>();

                return selected;
            }

            set { Session[SELECTED_KEY] = value; }
        }
    }
}
