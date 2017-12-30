using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Core.SqlProvider.Smo;

namespace WCMS.WebSystem.WebParts.Central
{
    public partial class DataStoreEditor : System.Web.UI.UserControl
    {
        private Dictionary<string, string> _cleanUpQueries = new Dictionary<string, string>();

        public DataStoreEditor()
        {
            _cleanUpQueries.Add("WebUserGroup", "DELETE FROM WebUserGroup WHERE UserId NOT IN (SELECT UserId FROM WebUser)");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        public DataSet Select()
        {
            var query = new QueryParser(this.Context);
            query.BasePath = CentralPages.WebDataRows;

            try
            {
                var dataSet = DataHelper.ToDataSet(
                    from i in WebObject.GetList()
                    let manager = i.DataManager
                    select new
                    {
                        i.Id,
                        i.Name,
                        i.IdentityColumn,
                        i.LastRecordId,
                        i.DateModified,
                        NameUrl = query.Set(WebColumns.Id, i.Id).BuildQuery(),
                        CacheType = CacheTypes.GetText(i.CacheTypeId),
                        ManagerName = DataHelper.GetStringPreview(i.ManagerName, 30),
                        CacheStatus = i.CacheStatus.ToString("G"),
                        CachedCount = i.GetCachedItemCount(),
                        Count = manager == null ? -1 : manager.GetCount()
                    }
                );

                return dataSet;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return DataUtil.GetEmptyDataSet();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataUtil.GetId(e.CommandArgument);
            var query = new WContext(this);

            switch (e.CommandName)
            {
                case "Custom_Edit":
                    query.Set(WebColumns.Id, id);
                    query.Open("WebObjectEdit");
                    break;
            }
        }

        protected void cmdUpdateLastRecords_Click(object sender, EventArgs e)
        {
            UpdateLastRecords();
            this.WriteStatus("<br/>Last record update COMPLETED!");
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            SystemReset();
            QueryParser.StaticRedirect();
        }

        #region Private Methods

        private void UpdateLastRecords()
        {
            var items = WebObject.UpdateLastRecords();

            foreach (var item in items)
                this.WriteStatus("<br/>Last record update: " + item.Name);
        }

        private void SystemReset()
        {
            HttpRuntime.UnloadAppDomain();

            WriteStatus("{0}{0}System Reset COMPLETED.{0}{0}", "<br />");
        }

        #endregion

        protected void cmdCleanUp_Click(object sender, EventArgs e)
        {
            foreach (var sqlQuery in _cleanUpQueries)
                SqlHelper.ExecuteNonQuery(CommandType.Text, sqlQuery.Value);
        }

        protected void cmdSynch_Click(object sender, EventArgs e)
        {
            var items = WebObject.GetList();

            SqlScriptGenerator.SyncWebObjects(items, (string objName) => WriteStatus("WebObject ADDED: {0}", objName));
            GridView1.DataBind();

            this.WriteStatus("<br/>Object Registration Sync COMPLETED!");
        }

        private void WriteStatus(string msgStatus, params object[] parms)
        {
            string msg = string.Format(msgStatus, parms);
            divMsgs.InnerHtml += string.Format("{0}<br />", msg);
        }

        protected void cmdLoadCache_Click(object sender, EventArgs e)
        {
            var checkedIds = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(checkedIds))
            {
                var ids = DataUtil.ParseCommaSeparatedIdList(checkedIds);
                foreach (var id in ids)
                {
                    var manager = WebObject.Get(id).DataManager;
                    if (manager != null)
                        manager.LoadCache();
                }

                GridView1.DataBind();
            }
        }

        protected void cmdUnloadCache_Click(object sender, EventArgs e)
        {
            var checkedIds = Request.Form["chkChecked"];
            if (!string.IsNullOrEmpty(checkedIds))
            {
                var ids = DataUtil.ParseCommaSeparatedIdList(checkedIds);
                foreach (var id in ids)
                {
                    var manager = WebObject.Get(id).DataManager;
                    if (manager != null)
                        manager.UnloadCache();
                }

                GridView1.DataBind();
            }
        }
    }
}