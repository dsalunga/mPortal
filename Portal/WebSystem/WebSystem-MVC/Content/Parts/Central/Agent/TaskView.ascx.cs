using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Diagnostics;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.Agent;

namespace WCMS.WebSystem.WebParts.Central.Agent
{
    public partial class TaskView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var id = DataHelper.GetId(Request, "Id");
                if (id > 0)
                {
                    WebJob job = WebJob.Provider.Get(id);
                    if (job != null)
                    {
                        lblName.InnerHtml = job.Name;
                        lblDescription.InnerHtml = job.Description;
                        lblStartDate.InnerHtml = job.StartDate.ToString();
                        lblRecurrence.InnerHtml = RecurrenceType.GetName(job.RecurrenceId);
                        chkEnabled.Checked = job.IsEnabled;
                        lblTypeName.InnerHtml = job.TypeName;
                        lblOccursEvery.InnerHtml = job.OccursEvery.ToString();

                        // Weekdays
                        StringBuilder sbWeekdays = new StringBuilder();
                        foreach (var kv in WeekdaysEnum.KeyPairs)
                        {
                            if (kv.Key > 0 && (job.Weekdays & kv.Key) > 0)
                            {
                                if (sbWeekdays.Length > 0)
                                    sbWeekdays.Append(",&nbsp;");

                                sbWeekdays.Append(kv.Value);
                            }
                        }

                        lblWeekdays.InnerHtml = sbWeekdays.ToString();

                        //foreach (ListItem item in cblWeekdays.Items)
                        //{
                        //    var value = DataHelper.GetInt32(item.Value);
                        //    if (value > 0)
                        //        item.Selected = (job.Weekdays & value) > 0;
                        //}

                        lblExecutionStartDate.InnerHtml = job.ExecutionStartDate.ToString();
                        lblExecutionEndDate.InnerHtml = job.ExecutionEndDate.ToString();
                        lblExecutionStatus.InnerHtml = ExecutionStatus.ToString(job.ExecutionStatus);
                        lblExecutionMessage.InnerHtml = job.ExecutionMessage;

                        //QueryParser qs = new QueryParser(this);

                        QueryParser q = new QueryParser(this);
                        q.SetEncode(ObjectKey.KeySource, q.BuildQuery());
                        q.Set(ObjectKey.KeyString, (new ObjectKey(WebObjects.WebJob, id)).ToString());

                        linkParameters.HRef = q.BuildQuery(CentralPages.WebParameters);
                    }
                }
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            this.ReturnPage();
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            var query = new WQuery(this);
            query.LoadAndRedirect("Agent/TaskEditor");
        }

        private void ReturnPage()
        {
            var query = new WQuery(this);
            query.Remove("Id");
            query.Remove(WConstants.Load);
            query.Redirect();
            //qs.LoadAndRedirect("TaskManager.ascx");
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            QueryParser query = new QueryParser(this);
            var id = query.GetId("Id");
            if (id > 0)
            {
                WebJob.Provider.Delete(id);

                ReturnPage();
            }
            else
            {
                lblStatus.InnerHtml = "Invalid Job ID.";
            }
        }

        protected void cmdForceExecute_Click(object sender, EventArgs e)
        {
            ExecuteTask(true);
        }

        private void ExecuteTask(bool forceExecute)
        {
            QueryParser query = new QueryParser(this);
            var id = query.GetId("Id");
            if (id > 0)
            {
                var job = WebJob.Provider.Get(id);
                if (forceExecute || job.ExecutionStatus != ExecutionStatus.Running)
                {
                    AgentHelper.ExecuteTask(job, forceExecute);

                    query.Redirect();
                }
                else
                {
                    lblStatus.InnerHtml = "Job is already running.";
                }
            }
            else
            {
                lblStatus.InnerHtml = "Invalid Job ID.";
            }
        }

        protected void cmdExecute_Click(object sender, EventArgs e)
        {
            ExecuteTask(false);
        }
    }
}