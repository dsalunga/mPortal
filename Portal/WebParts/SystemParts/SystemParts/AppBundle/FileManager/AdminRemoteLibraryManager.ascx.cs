using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.WebSystem.WebParts.RemoteIndexer;
using WCMS.Framework.Core;
using System.Diagnostics;
using WCMS.WebSystem.Agent;

namespace WCMS.WebSystem.WebParts.FileManager
{
    public partial class AdminRemoteLibraryManager : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) { }
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            var context = new WContext(this);
            context.SetLoadAndRedirect("AdminRemoteLibraryEdit.ascx");
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataHelper.GetId(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Action_Edit":
                    WContext context = new WContext(this);
                    context.Set("LibraryId", id);
                    context.SetLoadAndRedirect("AdminRemoteLibraryEdit.ascx");
                    break;

                case "Action_Delete":
                    RemoteLibrary.Provider.Delete(id);
                    GridView1.DataBind();
                    break;
            }
        }

        public DataSet Select()
        {
            return DataHelper.ToDataSet(
                from i in RemoteLibrary.Provider.GetList()
                select new
                {
                    i.Id,
                    i.Name,
                    i.SourceTypeId,
                    BaseAddress = DataHelper.GetStringPreview(i.BaseAddress, 30),
                    i.UserName,
                    i.Password,
                    i.LastIndexDate,
                    i.Active,
                    i.Size,
                    DisplayBaseAddress = DataHelper.GetStringPreview(i.DisplayBaseAddress, 30),
                    SourceType = RemoteSourceTypes.ToString(i.SourceTypeId)
                }
            );
        }

        protected void cmdForceExecute_Click(object sender, EventArgs e)
        {
            WContext context = new WContext(this);

            var name = context.PartAdmin.GetParameterValue("IndexerJobName");
            if (!string.IsNullOrEmpty(name))
            {
                var job = WebJob.Provider.Get(name);
                if (job.ExecutionStatus != ExecutionStatus.Running)
                {
                    AgentHelper.ExecuteTask(job, true);

                    lblStatus.InnerHtml = "Indexer Job executed.";
                }
                else
                {
                    lblStatus.InnerHtml = "Indexer Job is already running.";
                }
            }
            else
            {
                lblStatus.InnerHtml = "Unable to find Indexer Job.";
            }
        }
    }
}